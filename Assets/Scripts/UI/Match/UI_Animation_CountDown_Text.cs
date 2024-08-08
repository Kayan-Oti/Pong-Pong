using System.Collections;
using UnityEngine;
using DG.Tweening;


public class UI_Animation_CountDown_Text : UI_AbstractComponent_Animation
{
    //Propriets Inspector
    [SerializeField] private Vector2 _distanceToAnimate = new Vector2(300, 300);
    [SerializeField] private AnimationCurve _customEaseStart, _customEaseEnd;

    //Components
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    //Values
    private Vector2 _defaultPos;
    private float _endPos;

    public override void Setup()
    {
        //Get Components
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        //Set Values
        _defaultPos = _rectTransform.anchoredPosition;
        _endPos = _defaultPos.y - _distanceToAnimate.y;

        //Set Components
        SetComponents();
    }

    public override void SetComponents(){
        _rectTransform.transform.localPosition = _defaultPos + new Vector2(0f,_distanceToAnimate.y);
        _canvasGroup.alpha = 0f;
    }

    public override IEnumerator StartAnimation()
    {
        Sequence startAnimationSequence = DOTween.Sequence();
        startAnimationSequence
            .Insert(1,_rectTransform.DOAnchorPosY(_defaultPos.y, _animationDuration).SetEase(_customEaseStart))
            .Insert(1,_canvasGroup.DOFade(1f, _animationDuration));
        yield return startAnimationSequence.WaitForCompletion();
    }

    public override IEnumerator EndAnimation(){
        Sequence startAnimationSequence = DOTween.Sequence();
        startAnimationSequence
            .Insert(1,_rectTransform.DOAnchorPosY(_endPos, _animationDuration).SetEase(_customEaseEnd))
            .Insert(1,_canvasGroup.DOFade(0f, _animationDuration));
        yield return startAnimationSequence.WaitForCompletion();
    }
}
