using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [Tooltip("Get Buttons in Order")]
    [SerializeField] private List<UI_Button_LevelSelection> _buttonsLevel;
    private SO_LevelsUnlock _levelsUnlock;

    private void Start(){
        _levelsUnlock = Manager_DATA.Instance.GetLevelsUnlockDATA();
    }

    [ButtonMethod]
    public void EnableUnlockLevels(){
        for(int i = 0; i < _buttonsLevel.Count; i++)
            _buttonsLevel[i].SetButtonInteractable(_levelsUnlock.LevelsUnlock[i].unlock);
    }

    [ButtonMethod]
    public void DisableAllButtons(){
        foreach (UI_Button_LevelSelection button in _buttonsLevel)
            button.SetButtonInteractable(false);
    }

}