using System.Collections;
using UnityEngine;

public class UI_Manager_CountDown : UI_AbstractComponent_Manager
{
    [SerializeField] protected float _timeIdle = 0.25f;
    [SerializeField] private UI_AbstractComponent_Animation _animation1;
    [SerializeField] private UI_AbstractComponent_Animation _animation2;


    public override IEnumerator StartAnimation(){
        yield return StartCoroutine(_animation1.StartAnimation());
        yield return new WaitForSeconds(_timeIdle);
        StartCoroutine(_animation1.EndAnimation());

        yield return StartCoroutine(_animation2.StartAnimation());
        yield return new WaitForSeconds(_timeIdle);
        yield return StartCoroutine(_animation2.EndAnimation());
    }
    
    public override IEnumerator EndAnimation()
    {
        throw new System.NotImplementedException();
    }
}
