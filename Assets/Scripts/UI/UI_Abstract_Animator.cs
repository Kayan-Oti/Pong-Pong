using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
public abstract class UI_Abstract_Animator : MonoBehaviour
{
    [SerializeField] protected bool _startInteractable;
    [SerializeField] protected bool _startVisibility;

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
        SetInteractable(_startInteractable);
        SetVisibility(_startVisibility);
    }

    public void SetInteractable(bool state){
        _canvasGroup.interactable = state;
        _canvasGroup.blocksRaycasts = state;
    }

    public void SetVisibility(bool state){
        _canvasGroup.alpha = state ? 1f : 0f;
    }

    public IEnumerator StartAnimation(SO_Animation animationSO, bool enableInteractable, bool enableVisibility, Action DoLast = null){
        ConvertSO(animationSO);

        //Before animation
        SetInteractable(false);
        SetVisibility(false);
        SetComponents();

        //Animation
        Tween animation = GetTween();
        yield return animation.WaitForCompletion();

        //After Animation
        SetInteractable(enableInteractable);
        SetVisibility(enableVisibility);
        
        DoLast?.Invoke();
    }

    public void ConvertSO(SO_Animation soAnimation){
        _animationDuration = soAnimation._animationDuration;
        _ease = soAnimation._ease;
        _animation = soAnimation.animation;
    }
}
