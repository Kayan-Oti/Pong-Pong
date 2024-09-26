using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    // [field: Header("Temp")]
    // [field: SerializeField] public EventReference temp { get; private set;}

    [field: Header("Music")]
    [field: SerializeField] public EventReference MenuMusic { get; private set;}
    [field: SerializeField] public EventReference LevelMusic { get; private set;}

    [field: Header("UI")]
    [field: SerializeField] public EventReference ButtonHover { get; private set;}
    [field: SerializeField] public EventReference ButtonClick { get; private set;}

    [field: Header("LoadingScreen")]
    [field: SerializeField] public EventReference LoadingScreenStart { get; private set;}
    [field: SerializeField] public EventReference LoadingScreenEnd { get; private set;}

    [field: Header("Match")]
    [field: SerializeField] public EventReference OnMatchWin { get; private set;}
    [field: SerializeField] public EventReference OnMatchLose { get; private set;}
    [field: SerializeField] public EventReference OnRoundWin { get; private set;}
    [field: SerializeField] public EventReference OnRoundLose { get; private set;}
    
    [field: Header("Ball")]
    [field: SerializeField] public EventReference BallImpact { get; private set;}

    [field: Header("Character")]
    [field: SerializeField] public EventReference CharacterHitBall { get; private set;}


    public static FMODEvents Instance { get; private set;}

    private void Awake(){
        if(Instance == null)
            Instance = this;
    }
}
