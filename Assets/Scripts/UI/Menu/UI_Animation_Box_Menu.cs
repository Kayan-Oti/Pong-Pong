using System.Collections;
using DG.Tweening;
using UnityEngine;

public class UI_Animation_Box_Menu : UI_AbstractComponent_Animation
{
    //Propriets Inspector
    [SerializeField] private Vector2 _distanceToAnimate = new Vector2(300, 300);
    [SerializeField] private AnimationCurve _customEase;

    //Components
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    //Values
    private Vector2 _defaultPos;
    private float _endPos;

    #region Setup

    public override void Setup()
    {
        //Get Components
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        //Set Values
        _defaultPos = _rectTransform.anchoredPosition;
        _endPos = _defaultPos.x - _distanceToAnimate.x;

        SetComponents();
    }

    public override void SetComponents(){
        //Set Components
        _rectTransform.transform.localPosition += new Vector3(0f,_distanceToAnimate.y, 0f);
        _canvasGroup.alpha = 0f;
    }

    #endregion

    #region Animations

    [ContextMenu("Start animation")]
    public override IEnumerator StartAnimation(){
        Sequence startAnimationSequence = DOTween.Sequence();
        startAnimationSequence
            .Insert(1,_rectTransform.DOAnchorPosY(_defaultPos.y, _animationDuration).SetEase(_customEase))
            .Insert(1,_canvasGroup.DOFade(1f, _animationDuration));
        yield return startAnimationSequence.WaitForCompletion();
    }

    public override IEnumerator EndAnimation()
    {
        Tween animation = _rectTransform.DOAnchorPosX(_endPos, _animationDuration).SetEase(Ease.InBack);

        yield return animation.WaitForCompletion();
    }

    #endregion
}