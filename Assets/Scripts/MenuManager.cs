using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Animations Objects")]
    [SerializeField] private UI_Animation_Box_Menu _animationBox;
    [SerializeField] private UI_Manager_Buttons _managerAnimationButtons;

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
    }

    public void OnClick_Play(){
        StartCoroutine(AnimationPlay());
    }

    public IEnumerator AnimationPlay(){
        yield return StartCoroutine(_animationBox.EndAnimation());
        GameManager.Instance.LoadScene(SceneIndex.Level1);
    }
}