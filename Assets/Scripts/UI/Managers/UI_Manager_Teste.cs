using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class UI_Manager_Teste : UI_Manager
{
    [ButtonMethod]
    public void PlayAnimationFilterStartButton(){
        StartCoroutine(PlayAnimation("Start"));
    }

    [ButtonMethod]
    public void PlayAnimationFilterEndButton(){
        StartCoroutine(PlayAnimation("End", () => Debug.Log("DoLast")));
    }
}
