using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using System.Linq;
using System;

public class UI_Manager : MonoBehaviour
{
    [Serializable]
    public struct Animation{
        [Tooltip("Optional Name")]
        public string name;
        [Space(2)]
        [Tooltip("Wait animation to End Before Start Next")]
        public bool waitAnimationEnd;
        [Space(2)]
        [Tooltip("If Delay After animations")]
        public bool hasDelay;
        [Tooltip("Delay in Seconds After animation")]
        [ConditionalField(nameof(hasDelay))] public float delaySeconds;
        [Space(5)]
        public UI_Animator target;
        [Space(2)]
        public SO_Animation SOAnimation;
    }

    [Serializable]
    public struct AnimationsList{
        [Tooltip("Name used to Filter")]
        public string name;
        [Space(10)]
        public List<Animation> animations;
    }

    [SerializeField] private List<AnimationsList> _listAnimations = new List<AnimationsList>();
    private int _countCoroutines;

    private IEnumerator CountCoroutine(IEnumerator animation){
        _countCoroutines++;
        yield return StartCoroutine(animation);
        _countCoroutines--;
    }

    public IEnumerator PlayAnimationsByName(string nameFilter, Action DoLast = null)
    {
        _countCoroutines = 0;

        AnimationsList animationList = _listAnimations.First(x => x.name == nameFilter);
        foreach(Animation animation in animationList.animations)
        {
            if(animation.waitAnimationEnd)
                yield return animation.target.StartAnimation(animation.SOAnimation, true);
            else
                 StartCoroutine(CountCoroutine(animation.target.StartAnimation(animation.SOAnimation, true)));

            if(animation.hasDelay)
                yield return new WaitForSeconds(animation.delaySeconds);
        }
        
        //Wait all animations to End
        while (_countCoroutines > 0)
            yield return null;

        //Do After Animations End
        DoLast?.Invoke();
    }
}
