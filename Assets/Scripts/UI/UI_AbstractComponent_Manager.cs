using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_AbstractComponent_Manager : MonoBehaviour
{
    //Propriets Inspector
    [SerializeField] protected float _delayTimeBetween = 0.25f;
    [SerializeField] protected List<UI_AbstractComponent_Animation> _listAnimations = new List<UI_AbstractComponent_Animation>();

    private void Start()
    {
        GetListAnimations();
    }

    [ContextMenu("Get List Animations")]
    private void GetListAnimations()
    {
        _listAnimations.Clear();
        GetComponentsInChildren(_listAnimations);
    }
    public abstract IEnumerator StartAnimation();
}
