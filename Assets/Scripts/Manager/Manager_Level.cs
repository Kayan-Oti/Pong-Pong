using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MyBox;
using UnityEngine;

public class Manager_Level : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private Manager_Match _matchManager;
    [SerializeField] private Manager_Dialogue _dialogueManager;
    [SerializeField] private GameObject _buttonGameOver_PlayAgain;
    [SerializeField] private GameObject _buttonGameOver_NextLevel;

    [Header("Dialogue")]
    [SerializeField] private SO_Dialogue _dialogueStart;
    [Tooltip("On PLayer Win")]
    [SerializeField] private SO_Dialogue _dialogueWin;
    [Tooltip("On PLayer Lose")]
    [SerializeField] private SO_Dialogue _dialogueLose;

    [Header("Values")]
    [SerializeField] private bool _hasNextLevel = false;
    [ConditionalField(nameof(_hasNextLevel))][SerializeField] private Levels _nextLevelIndex;
    private ArenaSide _sideWinner;
    private bool _startDialogueDone;
    private const float DELAY_TO_START = 0.5f;

    #region Unity Setup
    private void OnEnable() {
        Manager_Event.GameManager.OnLoadedScene.Get().AddListener(OnLoadedScene);
        Manager_Event.MatchManger.OnEndMatch.Get().AddListener(OnEndMatch);
        Manager_Event.DialogueManager.OnEndDialogue.Get().AddListener(OnEndDialogue);
    }

    private void OnDisable(){
        Manager_Event.GameManager.OnLoadedScene.Get().RemoveListener(OnLoadedScene);
        Manager_Event.MatchManger.OnEndMatch.Get().RemoveListener(OnEndMatch);
        Manager_Event.DialogueManager.OnEndDialogue.Get().RemoveListener(OnEndDialogue);
    }

    #endregion

    #region Dialogue Events
    private void OnLoadedScene() {
        Invoke(nameof(StartDialogue), DELAY_TO_START);
    }

    [ContextMenu("StartDialogue")]
    private void StartDialogue(){
        _dialogueManager.StartDialogue(_dialogueStart);
    }

    private void EndMatchDialogue(){
        _dialogueManager.StartDialogue(_sideWinner.Equals(ArenaSide.Left)?_dialogueWin : _dialogueLose);
    }

    private void OnEndDialogue(){
        if(!_startDialogueDone){
            _startDialogueDone = true;
            StartMatch();
        }else{
            EnableGameOverUI();
        }
    }

    #endregion

    #region Match Events
    private void StartMatch(){
        _matchManager.StartMatch();
    }

    private void OnEndMatch(ArenaSide side){
        _sideWinner = side;

        //If Player Win
        if(side == ArenaSide.Left){
            if(_hasNextLevel)
                UnlockNextLevel();
        }

        EndMatchDialogue();
    }

    private void UnlockNextLevel(){
        Manager_DATA.Instance.UnlockLevel(_nextLevelIndex);
    }

    #endregion

    #region UI Events

    private void EnableGameOverUI(){
        SetStateGameOverButtons();
        StartCoroutine(_matchManager.EnableGameOverUI());
    }

    private void SetStateGameOverButtons(){
        if(_hasNextLevel && _sideWinner.Equals(ArenaSide.Left)){
            _buttonGameOver_PlayAgain.SetActive(false);
            _buttonGameOver_NextLevel.SetActive(true);
        }else{
            _buttonGameOver_PlayAgain.SetActive(true);
            _buttonGameOver_NextLevel.SetActive(false);
        }
    }

    public void Rematch(){
        StartCoroutine(_matchManager.DisableGameOverUI(() => StartMatch()));
    }

    public void BackToMenu(){
        DOTween.KillAll();
        GameManager.Instance.LoadScene(SceneIndex.Menu);
    }

    public void NextLevel(){
        SceneIndex sceneIndex = Manager_DATA.DictionaryLevelsToSceneIndex[_nextLevelIndex];
        GameManager.Instance.LoadScene(sceneIndex);
    }

    #endregion
}
