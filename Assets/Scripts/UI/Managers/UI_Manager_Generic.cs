using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class UI_Manager_Generic : UI_AbstractComponent_Manager
{
    [SerializeField] protected List<UI_AbstractComponent_Animation> _listAnimations = new List<UI_AbstractComponent_Animation>();

    private void Start() {
        GetListAnimationsInChildren(_listAnimations);
    }

    public override IEnumerator StartAnimation()
    {
        foreach(UI_AbstractComponent_Animation animation in _listAnimations)
        {
            RunCoroutine(animation.StartAnimation());
        }
        
        //Espera todas terminarem
        while (_countCoroutines > 0)
            yield return null;
    }

    public override IEnumerator EndAnimation()
    {
        //Inicia todas as animações
        foreach(UI_AbstractComponent_Animation animation in _listAnimations)
        {
            RunCoroutine(animation.EndAnimation());
        }
        
        //Espera todas terminarem
        while (_countCoroutines > 0)
            yield return null;
    }

    [ButtonMethod]
    public void StartAnimationButton(){
        StartCoroutine(StartAnimation());
    }

    [ButtonMethod]
    public void EndAnimationButton(){
        StartCoroutine(EndAnimation());
    }
}
