using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private Ball _ball;
    [SerializeField] private ScoreManager _scoreManager;

    [Header("UI Managers")]
    [SerializeField] private UI_Manager_CountDown _countDownManager;
    [SerializeField] private float _scoreToWin = 7;
    private Dictionary<ArenaSide, int> _scoreSides = new Dictionary<ArenaSide, int>();

    private void OnEnable() {
        EventManager.MatchManger.OnScore.Get().AddListener(EndRound);
    }

    private void OnDisable() {
        EventManager.MatchManger.OnScore.Get().RemoveListener(EndRound);
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
        StartMatchSetup();
        StartCoroutine(StartRound());
    }

    private IEnumerator StartRound(){
        EventManager.MatchManger.OnStartRound.Get().Invoke();
        
        yield return new WaitForSeconds(0.25f);
        yield return StartCoroutine(_countDownManager.StartAnimation());

        yield return new WaitForSeconds(0.25f);
        _ball.AddStartingForce();
    }

    public void EndRound(ArenaSide side){
        //Fim da Rodada
        EventManager.MatchManger.OnEndRound.Get().Invoke();

        //Animações e configurações pos rodada
        AddScore(side);
        _ball.ResetBall();

        //Nova rodada ou fim da partida
        if(_scoreSides[side] >= _scoreToWin){
            //Fim da partida
            EndMatch(side);
        }
        else{
            StartCoroutine(StartRound());
        }
    }

    private void EndMatch(ArenaSide winnerSide){
        EventManager.MatchManger.OnEndMatch.Get().Invoke(winnerSide);
    }

    #endregion

}
