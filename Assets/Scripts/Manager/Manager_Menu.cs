using System.Collections;
using UnityEngine;

public class Manager_Menu : MonoBehaviour
{
    [Header("Animations Objects")]
    [SerializeField] private UI_Animation_BoxMenu _animationBox;
    [SerializeField] private UI_AbstractComponent_Animation _animationLevelSelector;
    [SerializeField] private UI_Manager_Buttons _managerAnimationButtons;
    [SerializeField] private LevelSelector _levelSelector;

    [SerializeField] private GameObject _backButton;

    [Header("Values")]
    [SerializeField] private const float DELAY_TO_START = 1.0f;

    private void OnEnable() {
        Manager_Event.GameManager.OnLoadedScene.Get().AddListener(OnLoadScene);
    }

    private void OnDisable() {
        Manager_Event.GameManager.OnLoadedScene.Get().AddListener(OnLoadScene);
    }

    private void Start(){
        SetBackButtonActive(false);
    }

    private void SetBackButtonActive(bool state){
        _backButton.SetActive(state);
    }

    private void OnLoadScene(){
        _levelSelector.EnableUnlockLevels();

        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation(){
        yield return new WaitForSeconds(DELAY_TO_START);
        yield return StartCoroutine(_animationBox.StartAnimation());
        yield return StartCoroutine(_managerAnimationButtons.StartAnimation());
    }

    #region Onclick
    
    //--Menu Play
    public void OnClick_Play(){
        StartCoroutine(_animationBox.EndAnimation());
        StartCoroutine(_animationLevelSelector.StartAnimation(()=> SetBackButtonActive(true)));
    }

    //--Menu Back
    public void OnClick_Back(){
        SetBackButtonActive(false);

        StartCoroutine(_animationLevelSelector.EndAnimation());
        StartCoroutine(_animationBox.BackAnimation());
    }

    //--Menu Exit
    public void OnClick_Exit(){
        Application.Quit();
    }

    #endregion

    #region LevelSelector

    private void DisableAllButtons(){
        _levelSelector.DisableAllButtons();
    }

    public void EnterLevel1(){
        DisableAllButtons();
        GameManager.Instance.LoadScene(SceneIndex.Level1);
    }
    public void EnterLevel2(){
        DisableAllButtons();
        GameManager.Instance.LoadScene(SceneIndex.Level2);
    }
    public void EnterLevel3(){
        DisableAllButtons();
        GameManager.Instance.LoadScene(SceneIndex.Level3);
    }
    public void EnterLevel4(){
        DisableAllButtons();
        GameManager.Instance.LoadScene(SceneIndex.Level4);
    }
    
    #endregion

}