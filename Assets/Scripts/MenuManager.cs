using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Animations Objects")]
    [SerializeField] private UI_Animation_Box_Menu _animationBox;
    [SerializeField] private UI_Animation_LevelSelector _animationLevelSelector;
    [SerializeField] private UI_Manager_Buttons _managerAnimationButtons;

    [SerializeField] private ButtonsManager _buttonManager_Main, _buttonManager_LevelSelector;

    [Header("Values")]
    [SerializeField] private float _delayToStart = 1.0f;

    void Start()
    {
        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation(){
        yield return new WaitForSeconds(_delayToStart);
        yield return StartCoroutine(_animationBox.StartAnimation());
        yield return StartCoroutine(_managerAnimationButtons.StartAnimation());
        _buttonManager_Main.SetInteractable(true);
    }

    #region Onclick
    
    public void OnClick_Play(){
        _buttonManager_Main.SetInteractable(false);
        StartCoroutine(AnimationPlay());
    }

    private IEnumerator AnimationPlay(){
        //Animation
        StartCoroutine(_animationBox.EndAnimation());
        yield return StartCoroutine(_animationLevelSelector.StartAnimation());

        //Pos Animation
        _buttonManager_LevelSelector.SetInteractable(true);
    }

    public void OnClickBack(){
        _buttonManager_LevelSelector.SetInteractable(false);
        StartCoroutine(_animationLevelSelector.EndAnimation());
        StartCoroutine(AnimationBack());
    }

    private IEnumerator AnimationBack(){
        //Animation
        yield return StartCoroutine(_animationBox.BackAnimation());

        //Pos Animation
        _buttonManager_Main.SetInteractable(true);
    }

    #endregion

    #region LevelSelector

    public void EnterLevel1(){
        GameManager.Instance.LoadScene(SceneIndex.Level1);
    }
    public void EnterLevel2(){
        GameManager.Instance.LoadScene(SceneIndex.Level1);
    }
    public void EnterLevel3(){
        GameManager.Instance.LoadScene(SceneIndex.Level1);
    }
    public void EnterLevel4(){
        GameManager.Instance.LoadScene(SceneIndex.Level1);
    }
    
    #endregion

}