using DG.Tweening;
using UnityEngine;

public class UI_Animator : UI_Abstract_Animation
{
    //Translation
    private Vector2 _defaultPos;
    private Vector2 _targetPosition;

    //Rotation
    private float _defaultRotationZ;
    private float _targetRotationZ;

    //Scale  
    private Vector2 _targetScale;

    //Fade
    private float _targetFade;


    public override void SetValues()
    {
        //Default Values
        _defaultPos = _rectTransform.anchoredPosition;
        _defaultRotationZ = _rectTransform.localEulerAngles.z;
    }

    public override void SetComponents(){
        //Start Position
        if(_animation.DoTranslation){
            switch(_animation.StylePosition){
                case AnimationStyle.Appearing:
                    _targetPosition = _defaultPos;
                    _rectTransform.localPosition = (Vector3)(_defaultPos + _animation.Distance);
                break;
                case AnimationStyle.Leaving:
                    _targetPosition = _defaultPos + _animation.Distance;
                    _rectTransform.localPosition = (Vector3)_defaultPos;
                break;
            }
        }
        
        //Start Rotation
        if(_animation.DoRotation){
            switch(_animation.StyleRotation){
                case AnimationStyle.Appearing:
                    _targetRotationZ = _defaultRotationZ;
                    _rectTransform.rotation = Quaternion.Euler(0,0, _defaultRotationZ + _animation.RotationZ);

                break;
                case AnimationStyle.Leaving:
                    _targetRotationZ = _defaultRotationZ + _animation.RotationZ;
                    _rectTransform.rotation = Quaternion.Euler(0,0, _defaultRotationZ);
                break;
            }
        }

        //Start Fade
        if(_animation.DoFade){
            switch(_animation.StyleFade){
                case AnimationStyle.Appearing:
                    _targetFade = 1f;
                    _canvasGroup.alpha = 0f;
                break;
                case AnimationStyle.Leaving:
                    _targetFade = 0f;
                    _canvasGroup.alpha = 1f;
                break;
            }
        }

        //Start Scale
        if(_animation.DoFade){
            switch(_animation.StyleFade){
                case AnimationStyle.Appearing:
                    _targetScale = Vector2.one;
                    _rectTransform.localScale = Vector2.zero;
                break;
                case AnimationStyle.Leaving:
                    _targetScale = Vector2.zero;
                    _rectTransform.localScale = Vector2.one;
                break;
            }
        }
    }

    public override Tween GetTween()
    {
        Sequence animation = DOTween.Sequence();

        if(_animation.DoTranslation)
            animation.Insert(0,_rectTransform.DOAnchorPos(_targetPosition, _animationDuration).SetEase(_ease));

        if(_animation.DoRotation)
            animation.Insert(0,_rectTransform.DORotate(new Vector3(0,0,_targetRotationZ), _animationDuration).SetEase(_ease));

        if(_animation.DoFade)
            animation.Insert(0,_canvasGroup.DOFade(_targetFade, _animationDuration).SetEase(_ease));

        if(_animation.DoScale)
            animation.Insert(0,_rectTransform.DOScale(_targetScale, _animationDuration).SetEase(_ease));

        return animation;
    }
}