using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ButtonsManager _gameOverButtonsManager;
    [SerializeField] private UI_Manager_GameOver _gameOverManager;

    public IEnumerator EnableGameOverUI(){
        yield return StartCoroutine(_gameOverManager.StartAnimation());
        _gameOverButtonsManager.SetInteractable(true);
    }

    public IEnumerator DisableGameOverUI(){
        _gameOverButtonsManager.SetInteractable(false);
        yield return StartCoroutine(_gameOverManager.EndAnimation());
    }
}
