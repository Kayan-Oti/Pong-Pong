using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;


public abstract class UI_AbstractComponent_Animation : MonoBehaviour
{
    [SerializeField] protected float _animationDuration;
    [SerializeField] protected AnimationCurve _easeStart;
    [SerializeField] protected AnimationCurve _easeEnd;
    protected CanvasGroup _canvasGroup;
    protected RectTransform _rectTransform;

    void Start()
    {
        //Get Components
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        SetValues();
        SetComponents();
        SetCanvasGroupState(false);
    }

    public void SetCanvasGroupState(bool state){
        _canvasGroup.interactable = state;
        _canvasGroup.blocksRaycasts = state;
    }

    public IEnumerator StartAnimation(){
        //Before animation
        SetComponents();
        SetCanvasGroupState(false);

        //Animation
        Tween animation = GetTweenStart();
        yield return animation.WaitForCompletion();

        //After Animation
        SetCanvasGroupState(true);
    }

    public IEnumerator StartAnimation(Action DoLast){
        //Before animation
        SetComponents();
        SetCanvasGroupState(false);

        //Animation
        Tween animation = GetTweenStart();
        yield return animation.WaitForCompletion();

        //After Animation
        SetCanvasGroupState(true);
        DoLast();
    }

    public IEnumerator EndAnimation(){
        //Before Animation
        SetCanvasGroupState(false);

        //Animation
        Tween animation = GetTweenEnd();
        yield return animation.WaitForCompletion();

        //After Animation
    }

    public IEnumerator EndAnimation(Action DoLast){
        //Before Animation
        SetCanvasGroupState(false);

        //Animation
        Tween animation = GetTweenEnd();
        yield return animation.WaitForCompletion();

        //After Animation
        DoLast();
    }

    #region Abstract Methods

    public abstract void SetValues();
    public abstract void SetComponents();

    public abstract Tween GetTweenStart();
    public abstract Tween GetTweenEnd();

    #endregion
}

