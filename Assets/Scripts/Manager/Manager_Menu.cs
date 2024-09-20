using System.Collections;
using UnityEngine;

public class Manager_Menu : MonoBehaviour
{
    [Header("Animations Objects")]
    [SerializeField] private UI_Manager _managerMainBox;
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
        StartCoroutine(_managerMainBox.PlayAnimation("Start"));
    }

    #region Onclick
    
    //--Menu Play
    public void OnClick_Play(){
        StartCoroutine(_managerMainBox.PlayAnimation("End", () => SetBackButtonActive(true)));
    }

    //--Menu Back
    public void OnClick_Back(){
        SetBackButtonActive(false);
        StartCoroutine(_managerMainBox.PlayAnimation("Back"));
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