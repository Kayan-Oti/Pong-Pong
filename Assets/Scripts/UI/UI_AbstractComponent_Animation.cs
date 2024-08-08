using System.Collections;
using UnityEngine;

public abstract class UI_AbstractComponent_Animation : MonoBehaviour
{
    [SerializeField] protected float _animationDuration;
    void Start()
    {
        Setup();
    }

    public abstract void Setup();
    public abstract void SetComponents();


    [ContextMenu("Start animation")]
    public abstract IEnumerator StartAnimation();

    [ContextMenu("End animation")]
    public abstract IEnumerator EndAnimation();

}

