using DG.Tweening;
using UnityEngine;

public class UI_Animation_PositionFade : UI_AbstractComponent_Animation_Position
{
    public override void SetValues()
    {
        //Set Values
        _defaultPos = _rectTransform.anchoredPosition;
        _startPos = _defaultPos + _distanceStart;
        _endPos = _defaultPos + _distanceEnd;
    }

    public override void SetComponents(){
        _rectTransform.transform.localPosition = (Vector3)_startPos;
        _canvasGroup.alpha = 0f;
    }

    public override Tween GetTweenStart()
    {
        Tween animation = DOTween.Sequence()
            .Insert(0,_rectTransform.DOAnchorPos(_defaultPos, _animationDuration).SetEase(_easeStart))
            .Insert(0,_canvasGroup.DOFade(1f, _animationDuration));
        return animation;
    }

    public override Tween GetTweenEnd()
    {
        Tween animation = DOTween.Sequence()
            .Insert(0,_rectTransform.DOAnchorPos(_endPos, _animationDuration).SetEase(_easeEnd))
            .Insert(0,_canvasGroup.DOFade(0f, _animationDuration));
        return animation;
    }
}