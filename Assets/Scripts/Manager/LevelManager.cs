using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private MatchManager _matchManager;
    [SerializeField] private DialogueManager _dialogueManager;

    #region Unity Setup
    private void OnEnable() {
        EventManager.GameManager.OnLoadedScene.Get().AddListener(OnLoadedScene);
        EventManager.MatchManger.OnEndMatch.Get().AddListener(OnEndMatch);
        EventManager.DialogueManager.OnEndDialogue.Get().AddListener(OnEndDialogue);

    }

    private void OnDisable(){
        EventManager.GameManager.OnLoadedScene.Get().RemoveListener(OnLoadedScene);
        EventManager.MatchManger.OnEndMatch.Get().RemoveListener(OnEndMatch);
        EventManager.DialogueManager.OnEndDialogue.Get().RemoveListener(OnEndDialogue);

    }

    #endregion

    #region Dialogue Events
    private void OnLoadedScene() {
        Invoke(nameof(StartDialogue), 0.5f);
    }

    [ContextMenu("StartDialogue")]
    private void StartDialogue(){
        _dialogueManager.StartDialogue();
    }

    private void OnEndDialogue(){
        StartMatch();
    }

    private void StartMatch(){
        _matchManager.StartMatch();
    }

    #endregion

    #region Match Events

    private void OnEndMatch(ArenaSide side){
        StartCoroutine(_matchManager.EnableGameOverUI());
    }

    #endregion

    #region UI Events

    public void Rematch(){
        StartCoroutine(_matchManager.DisableGameOverUI(() => StartMatch()));
    }

    public void BackToMenu(){
        DOTween.KillAll();
        GameManager.Instance.LoadScene(SceneIndex.Menu);
    }

    public void NextLevel(){
        Debug.Log("Next Level");
    }

    #endregion
}
