using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SliderVolume : MonoBehaviour
{
    private enum VolumeType {
        Master,
        Music,
        Ambience,
        Sfx
    }

    [Header("Type")]
    [SerializeField] private VolumeType _volumeType = VolumeType.Master;
    private Slider _volumeSlider;

    private void Start(){
        _volumeSlider = GetComponentInChildren<Slider>();
        UpdateSliders();
    }

    private void UpdateSliders(){
        switch(_volumeType){
            case VolumeType.Master:
                _volumeSlider.value = AudioManager.Instance.masterVolume;
                break;
            case VolumeType.Music:
                _volumeSlider.value = AudioManager.Instance.musicVolume;
                break;
            case VolumeType.Ambience:
                _volumeSlider.value = AudioManager.Instance.ambienceVolume;
                break;
            case VolumeType.Sfx:
                _volumeSlider.value = AudioManager.Instance.sfxVolume;
                break;
        }
    }

    public void OnSliderValueChange(){
        switch(_volumeType){
            case VolumeType.Master:
                AudioManager.Instance.masterVolume = _volumeSlider.value;
                break;
            case VolumeType.Music:
                AudioManager.Instance.musicVolume = _volumeSlider.value;
                break;
            case VolumeType.Ambience:
                AudioManager.Instance.ambienceVolume = _volumeSlider.value;
                break;
            case VolumeType.Sfx:
                AudioManager.Instance.sfxVolume = _volumeSlider.value;
                break;
        }
        AudioManager.Instance.UpdateVolume();
    }
}
