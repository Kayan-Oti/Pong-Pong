using System.Collections;

public class UI_Manager_GameOver : UI_AbstractComponent_Manager
{
    public override IEnumerator StartAnimation()
    {
        foreach(UI_AbstractComponent_Animation animation in _listAnimations)
        {
            animation.SetComponents();
            yield return StartCoroutine(animation.StartAnimation());
        }
    }

    public IEnumerator EndAnimation()
    {
        foreach(UI_AbstractComponent_Animation animation in _listAnimations)
        {
            StartCoroutine(animation.EndAnimation());
        }
        yield return null;
    }
}
