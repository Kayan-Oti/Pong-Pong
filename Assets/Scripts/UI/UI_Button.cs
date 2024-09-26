using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour
{
    private EventTrigger _eventTrigger;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _eventTrigger = gameObject.AddComponent<EventTrigger>();
        _eventTrigger.AddTriggerListener(EventTriggerType.PointerClick, OnClickEvent);
        _eventTrigger.AddTriggerListener(EventTriggerType.PointerEnter, OnEnterEvent);

    }

    protected virtual void OnClickEvent(PointerEventData eventData){
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.ButtonClick, transform.position);
    }

    protected virtual void OnEnterEvent(PointerEventData eventData){
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.ButtonHover, transform.position);
    }
}