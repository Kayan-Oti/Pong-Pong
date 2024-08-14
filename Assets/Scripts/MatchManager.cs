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
    [SerializeField] private UI_Manager_GameOver _gameOverManager;
    [SerializeField] private ButtonsManager _gameOverButtonsManager;
    [SerializeField] private float _scoreToWin = 7;
    private Dictionary<ArenaSide, int> _scoreSides = new Dictionary<ArenaSide, int>();

    private void OnEnable() {
        EventManager.MatchManger.OnScore.Get().AddListener(EndRound);
    }

    private void OnDisable() {
        EventManager.MatchManger.OnScore.Get().RemoveListener(EndRound);
    }

    private void Start() {
        //Solução temporária
        //Invoke criado porque alguns objetos não conseguem iniciar a tempo
        //Criar um evento que triga o começo da partida
        Invoke(nameof(StartMatch), 0.25f);
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
    private void StartMatch(){
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
            StartCoroutine(EndMatch(side));
        }
        else{
            StartCoroutine(StartRound());
        }
    }

    private IEnumerator EndMatch(ArenaSide side){
        EventManager.MatchManger.OnEndMatch.Get().Invoke();

        yield return StartCoroutine(_gameOverManager.StartAnimation());
        _gameOverButtonsManager.SetInteractable(true);        
    }

    #endregion

    #region UI Events

    public void Rematch(){
        Debug.Log("Rematch");

        _gameOverButtonsManager.SetInteractable(false);
        StartCoroutine(_gameOverManager.EndAnimation());

        Invoke(nameof(StartMatch), 1.0f);
    }

    public void BackToMenu(){
        GameManager.Instance.LoadScene(SceneIndex.Menu);
    }

    public void NextLevel(){
        Debug.Log("Next Level");
    }

    #endregion

}
