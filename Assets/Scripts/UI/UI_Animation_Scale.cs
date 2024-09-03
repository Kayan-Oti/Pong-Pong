using System.Collections;
using DG.Tweening;
using UnityEngine;

public class UI_Animation_Scale : UI_AbstractComponent_Animation
{
    //Propriets Inspector
    [SerializeField] private AnimationCurve _customEase;
    private CanvasGroup _canvasGroup;

    //Components
    private RectTransform _rectTransform;

    public override void Setup()
    {
        //Get Components
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        SetComponents();
    }

    public override void SetComponents(){
        _rectTransform.localScale = Vector2.zero;
    }

    [ContextMenu("Start animation")]
    public override IEnumerator StartAnimation(){
        Tween animation = _rectTransform.DOScale(1f, _animationDuration).SetEase(_customEase);
        yield return animation.WaitForCompletion();
        SetCanvasGroup(true);
    }

    public override IEnumerator EndAnimation()
    {
        SetCanvasGroup(false);
        Tween animation = _rectTransform.DOScale(0f, _animationDuration).SetEase(_customEase);
        yield return animation.WaitForCompletion();
    }

    private void SetCanvasGroup(bool state){
        _canvasGroup.interactable = state;
        _canvasGroup.blocksRaycasts = state;
    }
}
