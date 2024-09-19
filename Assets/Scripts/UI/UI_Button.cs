using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour
{
    private EventTrigger _eventTrigger;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _eventTrigger = gameObject.AddComponent<EventTrigger>();
        _eventTrigger.AddEventListener(EventTriggerType.PointerClick, OnClickEvent);
        _eventTrigger.AddEventListener(EventTriggerType.PointerEnter, OnEnterEvent);

    }

    protected virtual void OnClickEvent(PointerEventData eventData){
        Manager_Sound.PlaySound(SoundType.UI_ButtonClick);
    }

    protected virtual void OnEnterEvent(PointerEventData eventData){
        Manager_Sound.PlaySound(SoundType.UI_ButtonHover);
    }
}
