using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_AbstractComponent_Manager : MonoBehaviour
{
    protected int _countCoroutines;

    #region Common Methods
    public void GetListAnimationsInChildren(List<UI_AbstractComponent_Animation> list)
    {
        list.Clear();
        GetComponentsInChildren(list);
    }

    public void RunCoroutine(IEnumerator animation)
    {
        StartCoroutine(CountCoroutine(animation));
    }

    private IEnumerator CountCoroutine(IEnumerator animation){
        _countCoroutines++;
        yield return StartCoroutine(animation);
        _countCoroutines--;
    }

    #endregion

    #region Abstracts

    public abstract IEnumerator StartAnimation();
    public abstract IEnumerator EndAnimation();

    #endregion
}
