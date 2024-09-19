using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Manager_Event
{
    public static readonly MatchEvents MatchManger = new MatchEvents();
    public static readonly GameEvents GameManager = new GameEvents();
    public static readonly DialogueEvents DialogueManager = new DialogueEvents();

    public class GenericEvent<T> where T: class, new()
    {
        private Dictionary<string, T> map = new Dictionary<string, T>();

        public T Get(string channel = ""){
            map.TryAdd(channel, new T());
            return map[channel];
        }
    }

    public class GameEvents{
        public class ChangingScene: UnityEvent {}
        public GenericEvent<ChangingScene> OnChanginScene = new GenericEvent<ChangingScene>();
        public class LoadedScene: UnityEvent {}
        public GenericEvent<LoadedScene> OnLoadedScene = new GenericEvent<LoadedScene>();
    }

    public class MatchEvents{
        public class ScoreEvent: UnityEvent<ArenaSide> {}
        public GenericEvent<ScoreEvent> OnScore = new GenericEvent<ScoreEvent>();

        public class StartRoundEvent: UnityEvent {}
        public GenericEvent<StartRoundEvent> OnStartRound = new GenericEvent<StartRoundEvent>();

        public class EndRoundEvent: UnityEvent {}
        public GenericEvent<EndRoundEvent> OnEndRound = new GenericEvent<EndRoundEvent>();

        public class EndMatchEvent: UnityEvent<ArenaSide> {}
        public GenericEvent<EndMatchEvent> OnEndMatch = new GenericEvent<EndMatchEvent>();

    }

    public class DialogueEvents{
        public class StartDialogueEvent: UnityEvent<SO_Dialogue> {}
        public GenericEvent<StartDialogueEvent> OnStartDialogue = new GenericEvent<StartDialogueEvent>();
        public class EndDialogueEvent: UnityEvent {}
        public GenericEvent<EndDialogueEvent> OnEndDialogue = new GenericEvent<EndDialogueEvent>();
    }
}
