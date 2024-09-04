using System.Collections;
using UnityEngine;

public class UI_Manager_Dialogue : UI_AbstractComponent_Manager
{
    [SerializeField] private UI_AbstractComponent_Animation _animationBackground;
    [SerializeField] private UI_AbstractComponent_Animation _animationTextContainer;
    [SerializeField] private UI_AbstractComponent_Animation _animationCharacterContainer;

    public override IEnumerator StartAnimation(){
        StartCoroutine(_animationBackground.StartAnimation());
        yield return StartCoroutine(_animationCharacterContainer.StartAnimation());
        yield return StartCoroutine(_animationTextContainer.StartAnimation());
    }

    public override IEnumerator EndAnimation(){
        StartCoroutine(_animationBackground.EndAnimation());
        StartCoroutine(_animationTextContainer.EndAnimation());
        yield return StartCoroutine(_animationCharacterContainer.EndAnimation());
    }
}
