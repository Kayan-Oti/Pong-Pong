using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour
{
    private EventTrigger _eventTrigger;
    // Start is called before the first frame update
    void Start()
    {
        _eventTrigger = gameObject.AddComponent<EventTrigger>();
        _eventTrigger.AddListener(EventTriggerType.PointerClick, OnClickEvent);
        _eventTrigger.AddListener(EventTriggerType.PointerEnter, OnEnterEvent);

    }

    private void OnClickEvent(PointerEventData eventData){
        SoundManager.PlaySound(SoundType.UI_ButtonClick);
    }

    private void OnEnterEvent(PointerEventData eventData){
        SoundManager.PlaySound(SoundType.UI_ButtonHover);
    }
}
