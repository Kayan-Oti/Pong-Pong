using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UI_Manager_GameOver : UI_AbstractComponent_Manager
{
    [SerializeField] protected List<UI_AbstractComponent_Animation> _listAnimations = new List<UI_AbstractComponent_Animation>();

    private void Start() {
        GetListAnimationsInChildren(_listAnimations);
    }
    public override IEnumerator StartAnimation()
    {
        foreach(UI_AbstractComponent_Animation animation in _listAnimations)
        {
            StartCoroutine(animation.StartAnimation());
        }
        yield return null;
    }

    public override IEnumerator EndAnimation()
    {
        int count = 0;
        
        //Inicia todas as animações
        foreach(UI_AbstractComponent_Animation animation in _listAnimations)
        {
            StartCoroutine(CountCoroutine(animation, count));
        }

        //Espera todas terminarem
        while (count > 0)
            yield return null;     
    }
}
