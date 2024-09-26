using System.Collections;
using UnityEngine;

public class Manager_Menu : MonoBehaviour
{
    [Header("Animations Objects")]
    [SerializeField] private UI_ManagerAnimation _managerMainBox;
    [SerializeField] private LevelSelector _levelSelector;

    [SerializeField] private GameObject _backButton;
    [SerializeField] private GameObject _settings;

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
        //Play Song Menu
        AudioManager.Instance.InitializeMusic(FMODEvents.Instance.MenuMusic, MusicIntensity.Intensity3);
        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation(){
        yield return new WaitForSeconds(DELAY_TO_START);
        yield return _managerMainBox.PlayAnimation("Start");
    }

    #region Onclick
    
    //--Menu Play
    public void OnClick_Play(){
        StartCoroutine(_managerMainBox.PlayAnimation("End", () => SetBackButtonActive(true)));
    }
    //-Back
    public void OnClick_Back(){
        SetBackButtonActive(false);
        StartCoroutine(_managerMainBox.PlayAnimation("Back"));
    }

    //--Menu Settings
    public void OnClick_Settings(){
        _settings.SetActive(true);
    }

    public void OnClick_SettingsClose(){
        _settings.SetActive(false);
    }


    //--Menu Exit
    public void OnClick_Exit(){
        Application.Quit();
    }

    #endregion

    #region LevelSelector

    private void LeavingMenu(){
        //Stop Song
        AudioManager.Instance.StopMusic();
        _levelSelector.DisableAllButtons();
    }

    public void EnterLevel1(){
        LeavingMenu();
        GameManager.Instance.LoadScene(SceneIndex.Level1);
    }
    public void EnterLevel2(){
        LeavingMenu();
        GameManager.Instance.LoadScene(SceneIndex.Level2);
    }
    public void EnterLevel3(){
        LeavingMenu();
        GameManager.Instance.LoadScene(SceneIndex.Level3);
    }
    public void EnterLevel4(){
        LeavingMenu();
        GameManager.Instance.LoadScene(SceneIndex.Level4);
    }
    
    #endregion

}