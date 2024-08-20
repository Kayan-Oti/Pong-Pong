using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject _loadingScreen;
    private List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();
    private int _currentSceneIndex;

    #region Initial Setup
    private void Awake() {
        if(Instance == null)
            Instance = this;
        else{
            Debug.Log("A second GameManager was Created & Destroyed");
            Destroy(gameObject);
        }

        StartCoroutine(LoadMenu());
    }

    public IEnumerator LoadMenu(){
        AsyncOperation loadMenu = SceneManager.LoadSceneAsync((int)SceneIndex.Menu, LoadSceneMode.Additive);
        _currentSceneIndex = (int)SceneIndex.Menu;

        while(!loadMenu.isDone)
            yield return null;

        OnSceneLoaded();
    }

    #endregion

    public void LoadScene(SceneIndex scene){
        StartCoroutine(GetSceneLoadProgress(scene));
    }

    private IEnumerator GetSceneLoadProgress(SceneIndex scene){
        //Ativa a animação de Loading
        _loadingScreen.SetActive(true);

        //Scene to load
        _scenesLoading.Add(SceneManager.UnloadSceneAsync(_currentSceneIndex));
        _currentSceneIndex = (int)scene;
        _scenesLoading.Add(SceneManager.LoadSceneAsync(_currentSceneIndex, LoadSceneMode.Additive));

        //Espera a animação terminar
        yield return new WaitForSeconds(1f); //Substituir

        //Espera carregar tudo
        for(int i = 0; i<_scenesLoading.Count; i++){
            while(!_scenesLoading[i].isDone){
                yield return null;
            }
        }

        OnSceneLoaded();
    }

    private void OnSceneLoaded(){
        //Animação ao terminar de Carrega
        Debug.Log("Animação terminar de Carregar");

        //Invoca o evento
        EventManager.GameManager.OnLoadedScene.Get().Invoke();
    }
}
