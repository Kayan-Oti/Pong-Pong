using DG.Tweening;
using UnityEngine;

public class UI_Animation_Scale : UI_AbstractComponent_Animation
{
    public override void SetValues()
    {
        return;
    }

    public override void SetComponents(){
        _rectTransform.localScale = Vector2.zero;
    }

    public override Tween GetTweenStart()
    {
        Tween animation = _rectTransform.DOScale(1f, _animationDuration).SetEase(_easeStart);
        return animation;
    }

    public override Tween GetTweenEnd()
    {
        Tween animation = _rectTransform.DOScale(0f, _animationDuration).SetEase(_easeEnd);
        return animation;
    }
}
