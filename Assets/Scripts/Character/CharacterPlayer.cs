using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterPlayer : Character
{
    public void Awake()
    {
        Manager_Input.Instance.playerInputActions.Player.ChangeAim.performed += ChangeAim;
    }

    public override void HandleInput()
    {
        //Movimentação
        _direction = Manager_Input.Instance.playerInputActions.Player.Movement.ReadValue<Vector2>();
    }

    public void ChangeAim(InputAction.CallbackContext context){
        if(!_canMove)
            return;

        _paddle.ChangeAimSide();
    }
}