using DG.Tweening;
using UnityEngine;

public class UI_Animation_Position : UI_AbstractComponent_Animation_Position
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
    }

    public override Tween GetTweenStart()
    {
        Tween animation = _rectTransform.DOAnchorPos(_defaultPos, _animationDuration).SetEase(_easeStart);
        return animation;
    }

    public override Tween GetTweenEnd()
    {
        Tween animation = _rectTransform.DOAnchorPos(_endPos, _animationDuration).SetEase(_easeEnd);
        return animation;
    }
}
