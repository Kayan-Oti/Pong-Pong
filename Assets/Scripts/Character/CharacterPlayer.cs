using UnityEngine;

public class CharacterPlayer : Character
{
    private void Update() {
        if (!_canMove)
            return;
        
        //Movimentação
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            _direction = Vector2.up;
        } else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            _direction = Vector2.down;
        }else {
            _direction = Vector2.zero;
        }

        //Paddle Aim
        if(Input.GetKeyDown(KeyCode.Space)){
            _paddle.ChangeAimSide();
        }
    }

    private void FixedUpdate() {
        if(_direction.sqrMagnitude != 0)
            _rigidbody.AddForce(_direction * _characterData.speed);
    }

}