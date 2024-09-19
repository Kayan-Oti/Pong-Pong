using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Input : MonoBehaviour
{
    public static Manager_Input Instance;
    public PlayerInputActions playerInputActions {get; private set;}

    private void Awake() {
        if(Instance == null)
            Instance = this;

        playerInputActions = new PlayerInputActions();
    }

    public void Start(){
        playerInputActions.Player.Enable();
    }
}
