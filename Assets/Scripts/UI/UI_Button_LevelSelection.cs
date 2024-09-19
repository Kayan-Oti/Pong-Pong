using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Button))]
public class UI_Button_LevelSelection : UI_Button
{
    private Button _button;
    private bool _isEnable;
    protected new void Start(){
        base.Start();
        _button = GetComponent<Button>();
    }

    public void SetButtonInteractable(bool state){
        _isEnable = state;
        _button.interactable = state;
    }

    protected override void OnClickEvent(PointerEventData eventData){
        if(_isEnable){
            Manager_Sound.PlaySound(SoundType.UI_ButtonClick);
        }
    }

    protected override void OnEnterEvent(PointerEventData eventData){
        if(_isEnable){
            Manager_Sound.PlaySound(SoundType.UI_ButtonHover);
        }
    }
}
