using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
public abstract class UI_Abstract_Animation : MonoBehaviour
{
    protected AnimationStruct _animation;
    protected float _animationDuration;
    protected AnimationCurve _ease;
    protected CanvasGroup _canvasGroup;
    protected RectTransform _rectTransform;


    #region --- --- --- Abstract Methods
    public abstract void SetValues();
    public abstract void SetComponents();
    public abstract Tween GetTween();

    #endregion

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

    public IEnumerator StartAnimation(SO_Animation animationSO, bool enableInteractable, Action DoLast = null){
        ConvertSO(animationSO);

        //Before animation
        SetComponents();
        SetCanvasGroupState(false);

        //Animation
        Tween animation = GetTween();
        yield return animation.WaitForCompletion();

        //After Animation
        SetCanvasGroupState(enableInteractable);
        DoLast?.Invoke();
    }

    public void ConvertSO(SO_Animation soAnimation){
        _animationDuration = soAnimation._animationDuration;
        _ease = soAnimation._ease;
        _animation = soAnimation.animation;
    }
}
