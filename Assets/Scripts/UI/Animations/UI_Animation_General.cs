using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using MyBox;

public class UI_Animation_General : UI_AbstractComponent_Animation
{
    [Separator("Options")]
    [SerializeField] private bool _doTranslation;
    [ConditionalField(nameof(_doTranslation))][SerializeField] private Vector2 _distanceStart;
    [ConditionalField(nameof(_doTranslation))][SerializeField] private Vector2 _distanceEnd;
    [Space(10)]

    [SerializeField] private bool _doRotation;
    [ConditionalField(nameof(_doRotation))][SerializeField] private float _rotationZStart;
    [ConditionalField(nameof(_doRotation))][SerializeField] private float _rotationZEnd;
    [Space(10)]

    [SerializeField] private bool _doScale;
    [Space(10)]

    [SerializeField] private bool _doFade;
    [Space(10)]


    //Values
    private Vector2 _defaultPos;
    private Vector2 _startPos, _endPos;
    private float _defaultRotationZ;
    private float _startRotationZ, _endRotationZ;

    public override void SetValues()
    {
        //Default Values
        _defaultPos = _rectTransform.anchoredPosition;
        _defaultRotationZ = _rectTransform.localEulerAngles.z;
    }

    public override void SetComponents(){
        //Start Position
        if(_doTranslation){
            _startPos = _defaultPos + _distanceStart;
            _endPos = _defaultPos + _distanceEnd;
            _rectTransform.localPosition = (Vector3)_startPos;
        }
        
        //Start Rotation
        if(_doRotation){
            _startRotationZ = _defaultRotationZ + _rotationZStart;
            _endRotationZ = _defaultRotationZ + _rotationZEnd;
            _rectTransform.rotation = Quaternion.Euler(0,0, _startRotationZ);
        }

        //Start Fade
        _canvasGroup.alpha = _doFade ? 0f : 1f;

        //Start Scale
        _rectTransform.localScale = _doScale ? Vector2.zero : Vector2.one;

    }

    public override Tween GetTweenStart()
    {
        Sequence animation = DOTween.Sequence();

        if(_doTranslation)
            animation.Insert(0,_rectTransform.DOAnchorPos(_defaultPos, _animationDuration).SetEase(_easeStart));

        if(_doRotation)
            animation.Insert(0,_rectTransform.DORotate(new Vector3(0,0,_defaultRotationZ), _animationDuration).SetEase(_easeStart));

        if(_doFade)
            animation.Insert(0,_canvasGroup.DOFade(1f, _animationDuration).SetEase(_easeStart));

        if(_doScale)
            animation.Insert(0,_rectTransform.DOScale(1f, _animationDuration).SetEase(_easeStart));

        return animation;
    }

    public override Tween GetTweenEnd()
    {
        Sequence animation = DOTween.Sequence();

        if(_doTranslation)
            animation.Insert(0,_rectTransform.DOAnchorPos(_endPos, _animationDuration).SetEase(_easeEnd));

        if(_doRotation)
            animation.Insert(0,_rectTransform.DORotate(new Vector3(0,0,_endRotationZ), _animationDuration).SetEase(_easeEnd));

        if(_doFade)
            animation.Insert(0,_canvasGroup.DOFade(0f, _animationDuration).SetEase(_easeEnd));

        if(_doScale)
            animation.Insert(0,_rectTransform.DOScale(0f, _animationDuration).SetEase(_easeEnd));

        return animation;
    }
}