using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
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
