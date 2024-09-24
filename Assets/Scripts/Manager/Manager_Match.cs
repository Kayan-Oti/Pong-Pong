using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Match : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private Ball _ball;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private Animation_CameraShake _cameraShake;

    [Header("UI Managers")]
    [SerializeField] private UI_ManagerAnimation _managerAnimation;
    [Header("Values")]
    [SerializeField] private float _scoreToWin = 7;


    private Dictionary<ArenaSide, int> _scoreSides = new Dictionary<ArenaSide, int>();

    private void OnEnable() {
        Manager_Event.MatchManger.OnScore.Get().AddListener(EndRound);
    }

    private void OnDisable() {
        Manager_Event.MatchManger.OnScore.Get().RemoveListener(EndRound);
    }

    #region Setup

    private void StartMatchSetup(){
        foreach(ArenaSide side in Enum.GetValues(typeof(ArenaSide))) {
            if(!_scoreSides.TryAdd(side, 0))
                _scoreSides[side] = 0;
            _scoreManager.ChangeScore(side, _scoreSides[side]);
        }
    }
    
    private void AddScore(ArenaSide side){
        _scoreSides[side]++;
        _scoreManager.ChangeScore(side, _scoreSides[side]);
    }

    #endregion

    #region MatchEvents
    public void StartMatch(){
        Debug.Log("Start Match");
        StartMatchSetup();
        StartCoroutine(StartRound());
    }

    private IEnumerator StartRound(){
        Manager_Event.MatchManger.OnStartRound.Get().Invoke();
        
        yield return new WaitForSeconds(0.25f);
        yield return StartCoroutine(_managerAnimation.PlayAnimation("CountDown"));

        _ball.AddStartingForce();
    }

    public void EndRound(ArenaSide side){
        //Fim da Rodada
        Manager_Event.MatchManger.OnEndRound.Get().Invoke();
        StartCoroutine(EndRoundActions(side));
    }

    private IEnumerator EndRoundActions(ArenaSide side){
        //Animations

        //CameraShake
        yield return StartCoroutine(_cameraShake.StartShake());
        
        //Score Animation
        //TODO
        AddScore(side);
        
        //Ball start Animation
        //TODO
        _ball.ResetBall();

        //Nova rodada ou fim da partida
        CheckEndOfMatch(side);
    }

    private void CheckEndOfMatch(ArenaSide side){
        if(_scoreSides[side] >= _scoreToWin){
            //Fim da partida
            EndMatch(side);
        }
        else{
            StartCoroutine(StartRound());
        }
    }

    private void EndMatch(ArenaSide winnerSide){
        Manager_Event.MatchManger.OnEndMatch.Get().Invoke(winnerSide);
    }

    #endregion

    public IEnumerator EnableGameOverUI(){
        yield return StartCoroutine(_managerAnimation.PlayAnimation("GameOver_Start"));
    }

    public IEnumerator DisableGameOverUI(Action DoLast){
        yield return StartCoroutine(_managerAnimation.PlayAnimation("GameOver_End"));
        DoLast();
    }
}
