using DG.Tweening;

public class UI_Animation_Fade : UI_AbstractComponent_Animation
{
    public override void SetValues()
    {
        return;
    }

    public override void SetComponents(){
        _canvasGroup.alpha = 0f;
    }

    public override Tween GetTweenStart()
    {
        Tween animation = _canvasGroup.DOFade(1f, _animationDuration);
        return animation;
    }

    public override Tween GetTweenEnd()
    {
        Tween animation = _canvasGroup.DOFade(0f, _animationDuration);
        return animation;
    }
}
