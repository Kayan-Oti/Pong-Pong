using System.Collections;
using DG.Tweening;
using UnityEngine;

public class UI_Animation_BoxMenu : UI_AbstractComponent_Animation_Position
{
    #region Setup

    public override void SetValues()
    {
        //Set Values
        _defaultPos = _rectTransform.anchoredPosition;
        _startPos = _defaultPos + _distanceStart;
        _endPos = _defaultPos + _distanceEnd;
    }

    public override void SetComponents(){
        //Set Components
        _rectTransform.transform.localPosition = (Vector3)_startPos;
        _canvasGroup.alpha = 0f;
    }

    #endregion

    #region Animations

    public override Tween GetTweenStart()
    {
        Tween animation = DOTween.Sequence()
            .Insert(0,_rectTransform.DOAnchorPos(_defaultPos, _animationDuration).SetEase(_easeStart))
            .Insert(0,_canvasGroup.DOFade(1f, _animationDuration).SetEase(_easeStart));
        return animation;
    }

    public override Tween GetTweenEnd()
    {
        Tween animation = _rectTransform.DOAnchorPos(_endPos, _animationDuration).SetEase(_easeEnd);
        return animation;
    }

    //Animation voltando do LevelSelector
    public IEnumerator BackAnimation()
    {
        //Before Animation
        _canvasGroup.alpha = 1f;

        //Animation
        //Deslocamento simples, voltando ao ponto inicial
        Tween animation = _rectTransform.DOAnchorPos(_defaultPos, _animationDuration).SetEase(_easeEnd);

        yield return animation.WaitForCompletion();

        //After Animation
        SetCanvasGroupState(true);
    }

    #endregion
}