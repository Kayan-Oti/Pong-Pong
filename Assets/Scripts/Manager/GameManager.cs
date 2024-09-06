using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private LoadingScreen _loadingScreen;
    private List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();
    private int _currentSceneIndex;
    private const float MIN_WAITSECONDS_LOADSCREEN = 1f;

    #region Initial Setup
    private void Awake() {
        if(Instance == null)
            Instance = this;

        StartCoroutine(FistScene(SceneIndex.Menu));
    }

    //Method similar to LoadScene, but without animation
    public IEnumerator FistScene(SceneIndex scene){
        //Scene to load
        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Additive);
        _currentSceneIndex = (int)scene;

        //Wait loading
        while(!sceneLoading.isDone)
            yield return null;

        //On Scene Loaded
        InvokeOnLoadedScene();
    }

    #endregion

    public void LoadScene(SceneIndex scene){
        StartCoroutine(GetSceneLoadProgress(scene));
    }

    private IEnumerator GetSceneLoadProgress(SceneIndex scene){
        //Ativa a animação de Loading
        Debug.Log("Animação Começar a Carregar");
        yield return _loadingScreen.OnStartLoadScene();

        //Scene to load
        _scenesLoading.Add(SceneManager.UnloadSceneAsync(_currentSceneIndex));
        _currentSceneIndex = (int)scene;
        _scenesLoading.Add(SceneManager.LoadSceneAsync(_currentSceneIndex, LoadSceneMode.Additive));

        //Espera a animação terminar
        yield return new WaitForSeconds(MIN_WAITSECONDS_LOADSCREEN);

        //Wait loading
        for(int i = 0; i<_scenesLoading.Count; i++){
            while(!_scenesLoading[i].isDone){
                yield return null;
            }
        }

        //On Scene Loaded
        OnSceneLoaded();
    }

    private void OnSceneLoaded(){
        //Animação ao terminar de Carrega
        Debug.Log("Animação terminar de Carregar");
        _loadingScreen.OnEndLoadScene(InvokeOnLoadedScene);
    }

    private void InvokeOnLoadedScene(){
        Debug.Log("Invoke");
        EventManager.GameManager.OnLoadedScene.Get().Invoke();
    }
}
