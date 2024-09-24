using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using System.Linq;
using System;

public class UI_ManagerAnimation : MonoBehaviour
{
    [Serializable]
    public struct Animation{
        [Tooltip("Optional Name")]
        public string name;
        [Space(2)]
        [Tooltip("Enable Interactable in the End of The animation")]
        public bool enableInteractable;
        [Space(2)]
        [Tooltip("Enable Visibility in the End of The animation")]
        public bool enableVisibility;
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
    /// <summary>
    /// Dictionay of Currents Animation
    /// </summary>
    private Dictionary<string, bool> _animationActivity = new Dictionary<string, bool>();
    private int _countCoroutines;

    /// <summary>
    /// Used to Count Coroutines that don't need to WaitToEnd
    /// </summary>
    private IEnumerator CountCoroutine(IEnumerator animation){
        _countCoroutines++;
        yield return StartCoroutine(animation);
        _countCoroutines--;
    }

    public IEnumerator PlayAnimation(string nameFilter, Action DoLast = null)
    {
        //Creates or Check is animation can be Active
        if(!ActiveAnimation(nameFilter))
            yield break;

        //Values
        _countCoroutines = 0;
        AnimationsList animationsList = GetAnimationListByName(nameFilter);

        //Loop
        foreach(Animation animation in animationsList.animations)
        {
            //Exception: Skip further animation
            if(!_animationActivity[nameFilter]){
                animation.target.SkipAnimation(animation.SOAnimation, animation.enableInteractable, animation.enableVisibility);
                continue;
            }
            //Play Animation, WaitingEnd or Not
            if(animation.waitAnimationEnd)
                yield return animation.target.StartAnimation(animation.SOAnimation, animation.enableInteractable, animation.enableVisibility);
            else
                 StartCoroutine(CountCoroutine(animation.target.StartAnimation(animation.SOAnimation, animation.enableInteractable, animation.enableVisibility)));
            
            //Delay before next
            //Check if is Active, because it can be skipped while is WaitingEnd Animation
            if(animation.hasDelay){
                yield return DelayAnimation(animation, nameFilter);
            }
        }
        
        //Wait all animations to End
        while (_countCoroutines > 0)
            yield return null;

        //Do After Animations End
        _animationActivity[nameFilter] = false; // Desactive Animation
        DoLast?.Invoke();
    }

    /// <summary>
    /// Alternative version of a WaitForSeconds, that can be break
    /// </summary>
    private IEnumerator DelayAnimation(Animation animation, string nameFilter){
        //Alternative WaitForSeconds
        for(float timer = animation.delaySeconds; timer >= 0; timer -= Time.deltaTime){
            //Skip Delay if !Activity
            if(!_animationActivity[nameFilter])
                yield break;
                    
            yield return null;
        }
    }

    private AnimationsList GetAnimationListByName(string nameFilter){
        return _listAnimations.First(x => x.name == nameFilter);
    }

    /// <summary>
    /// Actives or Add a new Animation to de Dictonary
    /// </summary>
    /// <returns>Return false if is Already Active</returns>
    private bool ActiveAnimation(string nameFilter){
        if(_animationActivity.ContainsKey(nameFilter)){
            if(!_animationActivity[nameFilter])
                _animationActivity[nameFilter] = true;
            else{
                Debug.LogError("Animation is Already Active");
                return false;
            }
        }
        else
            _animationActivity.Add(nameFilter, true);
        return true;
    }

    /// <returns>Return true if Animation is Active</returns>
    private bool CheckAnimationIsActive(string nameFilter){
        //If is in the list
        if(_animationActivity.ContainsKey(nameFilter)){
            //If is Active
            if(_animationActivity[nameFilter])
                return true;
            //If isn't
            Debug.LogError($"AnimationName {nameFilter}, is Already Desactive");
        }
        //If isn't
        else
            Debug.LogError($"AnimationName {nameFilter} Doesn't Exist");

         return false;
    }

    public void SkipAnimation(string nameFilter){
        //Check if does not AnimationName exist
        if(!CheckAnimationIsActive(nameFilter))
            return;
            
        _animationActivity[nameFilter] = false;
        AnimationsList animationsList = GetAnimationListByName(nameFilter);
        foreach(Animation animation in animationsList.animations){
            animation.target.CompleteAnimation();
        }
    }
}