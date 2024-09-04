using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_AbstractComponent_Manager : MonoBehaviour
{
    #region Common Methods
    public void GetListAnimationsInChildren(List<UI_AbstractComponent_Animation> list)
    {
        list.Clear();
        GetComponentsInChildren(list);
    }

    public IEnumerator CountCoroutine(UI_AbstractComponent_Animation animation,int count)
    {
        count++;
        yield return StartCoroutine(animation.EndAnimation());
        count--;
    }

    #endregion

    #region Abstracts

    public abstract IEnumerator StartAnimation();
    public abstract IEnumerator EndAnimation();

    #endregion
}
