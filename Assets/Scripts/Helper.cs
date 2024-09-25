using UnityEngine.EventSystems;

public static class Helper
{
    //Method to easy Add a new EventTrigger
    public static void AddTriggerListener (this EventTrigger trigger, EventTriggerType eventType, System.Action<PointerEventData> listener)
	{
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = eventType;
		entry.callback.AddListener(data => listener.Invoke((PointerEventData)data));
		trigger.triggers.Add(entry);
	}
}