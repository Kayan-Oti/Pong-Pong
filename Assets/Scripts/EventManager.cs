using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static readonly MatchEvents MatchManger = new MatchEvents();

    public class GenericEvent<T> where T: class, new()
    {
        private Dictionary<string, T> map = new Dictionary<string, T>();

        public T Get(string channel = ""){
            map.TryAdd(channel, new T());
            return map[channel];
        }
    }

    public class MatchEvents{
        public class ScoreEvent: UnityEvent<Component, ArenaSide> {}
        public GenericEvent<ScoreEvent> OnScoreTrigger = new GenericEvent<ScoreEvent>();
    }
}
