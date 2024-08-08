using System.Collections;
using UnityEngine;

public class UI_Manager_CountDown : UI_AbstractComponent_Manager
{
    public override IEnumerator StartAnimation(){
        foreach(UI_AbstractComponent_Animation animation in _listAnimations)
        {
            animation.SetComponents();
            yield return StartCoroutine(animation.StartAnimation());
            yield return new WaitForSeconds(_delayTimeBetween);
            StartCoroutine(animation.EndAnimation());
        }
    }
}
