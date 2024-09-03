using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Animation_FrontSkyPlants : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationTarget;
    [SerializeField] private float _duration = 1.0f;

    void Start()
    {
        transform.DORotate(_rotationTarget, _duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

}