using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager_Buttons : UI_AbstractComponent_Manager
{
    [SerializeField] protected List<UI_AbstractComponent_Animation> _listAnimations = new List<UI_AbstractComponent_Animation>();
    [SerializeField] protected float _delayTimeBetween = 1f;

    private void Start() {
        GetListAnimationsInChildren(_listAnimations);
    }

    public override IEnumerator StartAnimation(){
        foreach(UI_AbstractComponent_Animation button in _listAnimations)
        {
            yield return new WaitForSeconds(_delayTimeBetween);
            StartCoroutine(button.StartAnimation());
        }
    }

    public override IEnumerator EndAnimation()
    {
        throw new System.NotImplementedException();
    }
}