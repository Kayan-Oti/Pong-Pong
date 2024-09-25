using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private UI_ManagerAnimation _animation;
    [SerializeField] private Sfx _sfxStart;
    [SerializeField] private Sfx _sfxEnd;


    public IEnumerator OnStartLoadScene(){
        Manager_Sound.Instance.PlaySound(_sfxStart);
        yield return StartCoroutine(_animation.PlayAnimation("Start"));
    }

    public void OnEndLoadScene(Action DoLast){
        Manager_Sound.Instance.PlaySound(_sfxEnd);
        StartCoroutine(_animation.PlayAnimation("End", DoLast));
    }
}
