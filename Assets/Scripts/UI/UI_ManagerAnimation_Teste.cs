using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class UI_ManagerAnimation_Teste : UI_ManagerAnimation
{
    [ButtonMethod]
    public void PlayAnimationFilterStartButton(){
        StartCoroutine(PlayAnimation("Start"));
    }

    [ButtonMethod]
    public void PlayAnimationFilterEndButton(){
        StartCoroutine(PlayAnimation("End", () => Debug.Log("DoLast")));
    }

    [ButtonMethod]
    public void SkipAnimationStart(){
        SkipAnimation("Start");
    }
}
