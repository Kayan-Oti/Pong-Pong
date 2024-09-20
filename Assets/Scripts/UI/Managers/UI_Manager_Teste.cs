using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class UI_Manager_Teste : UI_Manager
{
    [ButtonMethod]
    public void PlayAnimationFilterStartButton(){
        StartCoroutine(PlayAnimationsByName("Start"));
    }

    [ButtonMethod]
    public void PlayAnimationFilterEndButton(){
        StartCoroutine(PlayAnimationsByName("End", () => Debug.Log("DoLast")));
    }
}
