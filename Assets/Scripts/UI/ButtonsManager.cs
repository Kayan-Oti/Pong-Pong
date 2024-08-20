using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
    [SerializeField] private List<Button> _listButtons = new List<Button>();

    private void Start(){
        GetListButtons();
        SetInteractable(false);
    }

    [ContextMenu("Get List Buttons")]
    private void GetListButtons()
    {
        _listButtons.Clear();
        GetComponentsInChildren(_listButtons);
    }

    public void SetInteractable(bool state){
        foreach (Button button in _listButtons){
            button.interactable = state;
        }
    }
}
