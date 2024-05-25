using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ChronoArkMod.Helper;

public static class EventTriggerMerge
{
    public static void AddOrMergeTrigger(this EventTrigger eventTrigger, EventTriggerType type,
        UnityAction<BaseEventData> action)
    {
        foreach (var trigger in eventTrigger.triggers) {
            if (type == trigger.eventID) {
                trigger.callback.AddListener(action);
                return;
            }
        }

        EventTrigger.Entry entry = new() { eventID = type };
        entry.callback.AddListener(action);
        eventTrigger.triggers.Add(entry);
    }
}