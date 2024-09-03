using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Animation_CameraShake : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _strength;
    [SerializeField] private int _vibrato;
    [SerializeField] [Range(0f,90f)] private float _randomness;
    [SerializeField] private bool _fadeOut = true;
    [SerializeField] private ShakeRandomnessMode _mode = ShakeRandomnessMode.Harmonic;

    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    [ContextMenu("StartShake")]
    public void StartShakeInspector(){
        StartCoroutine(StartShake());
    }

    public IEnumerator StartShake(){
        Tween animation = _camera.DOShakePosition(_duration, _strength, _vibrato, _randomness, _fadeOut, _mode);
        yield return animation.WaitForCompletion();
    }
}
