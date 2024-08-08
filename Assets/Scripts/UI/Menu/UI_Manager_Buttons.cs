using System.Collections;
using UnityEngine;

public class UI_Manager_Buttons : UI_AbstractComponent_Manager
{

    public override IEnumerator StartAnimation(){
        foreach(UI_AbstractComponent_Animation button in _listAnimations)
        {
            yield return new WaitForSeconds(_delayTimeBetween);
            StartCoroutine(button.StartAnimation());
        }
    }
}