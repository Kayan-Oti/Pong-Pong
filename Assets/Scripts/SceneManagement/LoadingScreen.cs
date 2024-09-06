using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private UI_AbstractComponent_Animation _animation;

    public Coroutine OnStartLoadScene(){
        return StartCoroutine(AnimationOnStartLoading());
    }

    public void OnEndLoadScene(Action DoLast){
        StartCoroutine(AnimationOnEndLoading(DoLast));
    }

    private IEnumerator AnimationOnStartLoading(){
        yield return StartCoroutine(_animation.StartAnimation());
    }

    private IEnumerator AnimationOnEndLoading(Action DoLast){
        yield return StartCoroutine(_animation.EndAnimation());
        DoLast();
    }
}
