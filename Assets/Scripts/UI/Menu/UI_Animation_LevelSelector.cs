using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_Animation_LevelSelector : UI_AbstractComponent_Animation
{
    //Propriets Inspector
    [SerializeField] private Vector2 _distanceToAnimateStart = new Vector2(300, 300);

    [SerializeField] private AnimationCurve _easeStart;
    [SerializeField] private AnimationCurve _easeEnd;

    [SerializeField] private GameObject _backButton;


    //Components
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    //Values
    private Vector2 _defaultPos;
    private Vector2 _startPos;

    #region Setup

    public override void Setup()
    {
        //Get Components
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        //Set Values
        _defaultPos = _rectTransform.anchoredPosition;
        _startPos = _defaultPos + _distanceToAnimateStart;

        SetComponents();
    }

    public override void SetComponents(){
        //Set Components
        _rectTransform.transform.localPosition = _startPos;
        _canvasGroup.alpha = 1f;
        SetLayoutActive(false);
    }

    #endregion

    #region Animations

    public override IEnumerator StartAnimation(){
        //Animação de deslocamento simples, movendo para a pos default
        Tween animation = _rectTransform.DOAnchorPos(_defaultPos, _animationDuration).SetEase(_easeStart);
        yield return animation.WaitForCompletion();

        //After Animation
        SetLayoutActive(true);
    }

    public override IEnumerator EndAnimation()
    {
        //Before Animation
        SetLayoutActive(false);
        
        //Animação de deslocamento simples, voltando ao ponto inicial
        Tween animation = _rectTransform.DOAnchorPos(_startPos, _animationDuration).SetEase(_easeEnd);
        yield return animation.WaitForCompletion();
    }

    private void SetLayoutActive(bool state){
        _backButton.SetActive(state);
        _canvasGroup.blocksRaycasts = state;
    }

    #endregion
}