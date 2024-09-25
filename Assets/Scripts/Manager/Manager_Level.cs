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
    [SerializeField] private bool _isFinalLevel = false;
    [ConditionalField(nameof(_isFinalLevel), true)][SerializeField] private Levels _nextLevelIndex;

    [Header("Sfxs")]
    [SerializeField] private Sfx _sfxMatchWin;
    [SerializeField] private Sfx _sfxMatchLose;


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
        SimpleAudioManager.Manager.instance.PlaySong((int)MusicIndex.Level);
        Invoke(nameof(StartDialogue), DELAY_TO_START);
    }

    [ContextMenu("StartDialogue")]
    private void StartDialogue(){
        _dialogueManager.StartDialogue(_dialogueStart);
    }

    private void EndMatchDialogue(){
        _dialogueManager.StartDialogue(HasPlayerWin()?_dialogueWin : _dialogueLose);
    }

    private void OnEndDialogue(){
        //Dialogue before match end
        if(!_startDialogueDone){
            _startDialogueDone = true;
            StartMatch();
            return;
        }
        //Final Level
        if(_isFinalLevel && HasPlayerWin()){
            OnWinTournament();
            return;
        }

        EnableGameOverUI();
    }

    #endregion

    #region Match Events
    private void StartMatch(){
        _matchManager.StartMatch();
    }

    private void OnEndMatch(ArenaSide side){
        _sideWinner = side;

        //If Player Win
        if(HasPlayerWin()){
            //If Has another level
            if(!_isFinalLevel)
                UnlockNextLevel();
            Manager_Sound.Instance.PlaySound(_sfxMatchWin);
        }else{
            Manager_Sound.Instance.PlaySound(_sfxMatchLose);
        }

        EndMatchDialogue();
    }

    private void UnlockNextLevel(){
        Manager_DATA.Instance.UnlockLevel(_nextLevelIndex);
    }

    private void OnWinTournament(){
        GameManager.Instance.LoadScene(SceneIndex.FinalScene);
    }

    private bool HasPlayerWin(){
        return _sideWinner.Equals(ArenaSide.Left);
    }

    #endregion

    #region UI Events

    private void EnableGameOverUI(){
        SetStateGameOverButtons();
        _matchManager.EnableGameOverUI();
    }

    private void SetStateGameOverButtons(){
        if(HasPlayerWin()){
            _buttonGameOver_PlayAgain.SetActive(false);
            _buttonGameOver_NextLevel.SetActive(true);
        }else{
            _buttonGameOver_PlayAgain.SetActive(true);
            _buttonGameOver_NextLevel.SetActive(false);
        }
    }

    public void Rematch(){
        _matchManager.DisableGameOverUI(() => StartMatch());
    }

    private void LeavingLevel(){
        DOTween.KillAll();
        SimpleAudioManager.Manager.instance.StopSong(0.5f);
    }

    public void BackToMenu(){
        LeavingLevel();
        GameManager.Instance.LoadScene(SceneIndex.Menu);
    }

    public void NextLevel(){
        LeavingLevel();
        SceneIndex sceneIndex = Manager_DATA.DictionaryLevelsToSceneIndex[_nextLevelIndex];
        GameManager.Instance.LoadScene(sceneIndex);
    }

    #endregion
}
