using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private UI_ManagerAnimation _animation;

    public Coroutine OnStartLoadScene(){
        return StartCoroutine(AnimationOnStartLoading());
    }

    public void OnEndLoadScene(Action DoLast){
        StartCoroutine(_animation.PlayAnimation("End", DoLast));
    }

    private IEnumerator AnimationOnStartLoading(){
        yield return StartCoroutine(_animation.PlayAnimation("Start"));
    }
}
