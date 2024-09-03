using UnityEngine;
using System.Collections;

public class UI_Manager_GameOver : UI_AbstractComponent_Manager
{
    public override IEnumerator StartAnimation()
    {
        foreach(UI_AbstractComponent_Animation animation in _listAnimations)
        {
            animation.SetComponents();
            StartCoroutine(animation.StartAnimation());
        }
        yield return null;
    }

    public IEnumerator EndAnimation()
    {
        int tally = 0;
        
        foreach(UI_AbstractComponent_Animation animation in _listAnimations)
        {
            StartCoroutine(RunCoroutine(animation));
        }

        while (tally > 0)
        {
            yield return null;
        }

        IEnumerator RunCoroutine(UI_AbstractComponent_Animation animation)
        {
            tally++;
            yield return StartCoroutine(animation.EndAnimation());
            tally--;
        }
    }
}
