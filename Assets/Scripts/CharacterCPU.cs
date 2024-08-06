using UnityEngine;

public class CharacterCPU : Character
{
    [Header("BOT Settings")]
    [SerializeField] private Rigidbody2D _ballRigidbody;

    private void Update(){
        if(_ballRigidbody.velocity.x > 0.0f){
            if(_ballRigidbody.position.y >  transform.position.y)
                _direction = Vector2.up;
            else if(_ballRigidbody.position.y <  transform.position.y)
                _direction = Vector2.down;
            else
                _direction = Vector2.zero;
        }
        else{
            if(transform.localPosition.y < -1f)
                _direction = Vector2.up;
            else if(transform.localPosition.y > 1f)
                _direction = Vector2.down;
            else
                _direction = Vector2.zero;
        }
    }

    private void FixedUpdate() {
        if(_direction.sqrMagnitude != 0)
            _rigidbody.AddForce(_direction * _speed);
    }
}
