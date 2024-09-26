using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private UI_ManagerAnimation _animation;


    public IEnumerator OnStartLoadScene(){
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.LoadingScreenStart);
        yield return StartCoroutine(_animation.PlayAnimation("Start"));
    }

    public void OnEndLoadScene(Action DoLast){
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.LoadingScreenEnd);
        StartCoroutine(_animation.PlayAnimation("End", DoLast));
    }
}
