using System.Collections;
using DG.Tweening;
using UnityEngine;

public class UI_Animation_FadeInOut : UI_AbstractComponent_Animation
{
    //Propriets Inspector
    [SerializeField] protected Vector2 _distanceToAnimate = new Vector2(300, 300);
    [SerializeField] protected AnimationCurve _customEaseStart, _customEaseEnd;

    //Components
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    //Values
    private Vector2 _defaultPos;
    private Vector2 _endPos;

    public override void Setup()
    {
        //Get Components
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        //Set Values
        _defaultPos = _rectTransform.anchoredPosition;
        _endPos = _defaultPos - _distanceToAnimate;

        //Set Components
        SetComponents();
    }

    public override void SetComponents(){
        _rectTransform.transform.localPosition = _defaultPos + _distanceToAnimate;
        _canvasGroup.alpha = 0f;
    }

    public override IEnumerator StartAnimation()
    {
        Sequence animationSequence = DOTween.Sequence()
            .Insert(1,_rectTransform.DOAnchorPos(_defaultPos, _animationDuration).SetEase(_customEaseStart))
            .Insert(1,_canvasGroup.DOFade(1f, _animationDuration));
        yield return animationSequence.WaitForCompletion();
    }

    public override IEnumerator EndAnimation(){
        Sequence animationSequence = DOTween.Sequence()
            .Insert(1,_rectTransform.DOAnchorPos(_endPos, _animationDuration).SetEase(_customEaseEnd))
            .Insert(1,_canvasGroup.DOFade(0f, _animationDuration));
        yield return animationSequence.WaitForCompletion();
    }
}

