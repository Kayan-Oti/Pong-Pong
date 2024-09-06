using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Animations Objects")]
    [SerializeField] private UI_Animation_BoxMenu _animationBox;
    [SerializeField] private UI_AbstractComponent_Animation _animationLevelSelector;
    [SerializeField] private UI_Manager_Buttons _managerAnimationButtons;
    [SerializeField] private GameObject _backButton;

    [Header("Values")]
    [SerializeField] private const float DELAY_TO_START = 1.0f;

    private void OnEnable() {
        EventManager.GameManager.OnLoadedScene.Get().AddListener(OnLoadScene);
    }

    private void OnDisable() {
        EventManager.GameManager.OnLoadedScene.Get().AddListener(OnLoadScene);
    }

    void Start()
    {
        SetBackButtonActive(false);
    }

    private void SetBackButtonActive(bool state){
        _backButton.SetActive(state);
    }

    private void OnLoadScene(){
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