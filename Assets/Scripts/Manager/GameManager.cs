using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;
    [SerializeField] private LoadingScreen _loadingScreen;
    private List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();
    private int _currentSceneIndex;
    private const float MIN_WAITSECONDS_LOADSCREEN = 1f;

    #region Initial Setup
    private void Awake() {
        if(Instance == null)
            Instance = this;

        StartCoroutine(FirstScene(SceneIndex.Menu));
    }

    //Method similar to LoadScene, but without animation
    public IEnumerator FirstScene(SceneIndex scene){
        //Scene to load
        _scenesLoading.Add(SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Additive));
        _currentSceneIndex = (int)scene;

        //Wait loading
        yield return WaitLoading();

        //On Scene Loaded
        InvokeOnLoadedScene();
    }

    #endregion

    public void LoadScene(SceneIndex scene){
        StartCoroutine(GetSceneLoadProgress(scene));
    }

    private IEnumerator GetSceneLoadProgress(SceneIndex scene){
        //Ativa a animação de Loading
        yield return _loadingScreen.OnStartLoadScene();

        //Scene to load
        _scenesLoading.Add(SceneManager.UnloadSceneAsync(_currentSceneIndex));
        _currentSceneIndex = (int)scene;
        _scenesLoading.Add(SceneManager.LoadSceneAsync(_currentSceneIndex, LoadSceneMode.Additive));

        //Tempo minimo de espera
        yield return new WaitForSeconds(MIN_WAITSECONDS_LOADSCREEN);

        //Wait loading
        yield return WaitLoading();

        //On Scene Loaded
        OnSceneLoaded();
    }

    private IEnumerator WaitLoading(){
        for(int i = 0; i<_scenesLoading.Count; i++){
            while(!_scenesLoading[i].isDone){
                yield return null;
            }
        }
    }

    private void OnSceneLoaded(){
        //Animação ao terminar de Carrega
        _loadingScreen.OnEndLoadScene(InvokeOnLoadedScene);
    }

    private void InvokeOnLoadedScene(){
        Manager_Event.GameManager.OnLoadedScene.Get().Invoke();
    }
}
