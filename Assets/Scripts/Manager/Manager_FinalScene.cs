using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_FinalScene : MonoBehaviour
{
    public void BackToMenu(){
        GameManager.Instance.LoadScene(SceneIndex.Menu);
    }
}
