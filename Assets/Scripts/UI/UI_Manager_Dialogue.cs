using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager_Dialogue : MonoBehaviour
{
    [SerializeField] private UI_AbstractComponent_Animation _animationBackground;

    [SerializeField] private UI_AbstractComponent_Animation _animationTextContainer;
    [SerializeField] private UI_AbstractComponent_Animation _animationCharacterContainer;

    public IEnumerator StartAnimation(){
        StartCoroutine(_animationBackground.StartAnimation());
        yield return StartCoroutine(_animationCharacterContainer.StartAnimation());
        yield return StartCoroutine(_animationTextContainer.StartAnimation());
    }

    public IEnumerator EndAnimation(){
        StartCoroutine(_animationBackground.EndAnimation());
        StartCoroutine(_animationTextContainer.EndAnimation());
        yield return StartCoroutine(_animationCharacterContainer.EndAnimation());
    }
}
