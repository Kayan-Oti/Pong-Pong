using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private UI_Manager_CountDown _countDownManager;
    [SerializeField] private UI_Manager_GameOver _gameOverManager;
    [SerializeField] private ButtonsManager _gameOverButtonsManager;
    [SerializeField] private float _scoreToWin = 7;
    private Dictionary<ArenaSide, int> _scoreSides = new Dictionary<ArenaSide, int>();

    private void OnEnable() {
        EventManager.MatchManger.OnScore.Get().AddListener(OnScoreTrigger);
    }

    private void OnDisable() {
        EventManager.MatchManger.OnScore.Get().RemoveListener(OnScoreTrigger);
    }

    private void Start() {
        //Solução temporária
        //Invoke criado porque alguns objetos não conseguem iniciar a tempo
        //Criar um evento que triga o começo da partida
        Invoke(nameof(StartMatch), 0.25f);
    }

    private void StartMatchSetup(){
        foreach(ArenaSide side in Enum.GetValues(typeof(ArenaSide))) {
            if(!_scoreSides.TryAdd(side, 0))
                _scoreSides[side] = 0;
            _scoreManager.ChangeScore(side, _scoreSides[side]);
        }
    }

    private void StartMatch(){
        StartMatchSetup();
        StartCoroutine(NextRound());
    }

    public void Rematch(){
        Debug.Log("Rematch");

        _gameOverButtonsManager.SetInteractable(false);
        StartCoroutine(_gameOverManager.EndAnimation());

        Invoke(nameof(StartMatch), 1.0f);
    }

    public void OnScoreTrigger(ArenaSide side){
        AddScore(side);

        _ball.ResetBall();

        if(_scoreSides[side] >= _scoreToWin)
            StartCoroutine(EndOfMatch(side));
        else
            StartCoroutine(NextRound());
    }

    private void AddScore(ArenaSide side){
        _scoreSides[side]++;
        _scoreManager.ChangeScore(side, _scoreSides[side]);
    }

    private IEnumerator NextRound(){
        yield return new WaitForSeconds(0.25f);
        yield return StartCoroutine(_countDownManager.StartAnimation());
        yield return new WaitForSeconds(0.25f);
        _ball.AddStartingForce();
    }

    private IEnumerator EndOfMatch(ArenaSide side){
        EventManager.MatchManger.OnEndMatch.Get().Invoke();

        yield return StartCoroutine(_gameOverManager.StartAnimation());
        _gameOverButtonsManager.SetInteractable(true);        
    }

    public void BackToMenu(){
        GameManager.Instance.LoadScene(SceneIndex.Menu);
    }
}
