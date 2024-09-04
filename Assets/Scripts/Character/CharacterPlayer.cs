using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterPlayer : Character
{
    public void Awake()
    {
        InputManager.Instance.playerInputActions.Player.ChangeAim.performed += ChangeAim;
    }

    public override void HandleInput()
    {
        //Movimentação
        _direction = InputManager.Instance.playerInputActions.Player.Movement.ReadValue<Vector2>();
    }

    public void ChangeAim(InputAction.CallbackContext context){
        if(!_canMove)
            return;

        _paddle.ChangeAimSide();
    }
}