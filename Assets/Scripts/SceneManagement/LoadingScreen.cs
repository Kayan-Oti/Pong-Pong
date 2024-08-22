using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    private void OnEnable() {
        Debug.Log("Active LoadingScreen");
        EventManager.GameManager.OnLoadedScene.Get().AddListener(OnLoadedScene);
    }

    private void OnDisable() {
        Debug.Log("Disable LoadginScreen");
        EventManager.GameManager.OnLoadedScene.Get().RemoveListener(OnLoadedScene);
    }

    private void OnLoadedScene(){
        gameObject.SetActive(false);
    }
}
