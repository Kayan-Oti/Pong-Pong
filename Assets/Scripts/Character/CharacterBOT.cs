using UnityEngine;

public abstract class CharacterBOT : Character
{
    [Header("BOT Settings")]
    [SerializeField] protected Rigidbody2D _ballRigidbody;

    private void Update(){
        if (!_canMove)
            return;

        IAControl();
    }

    private void FixedUpdate() {
        if(_direction.sqrMagnitude != 0)
            _rigidbody.AddForce(_direction * _characterData.speed);
    }
    
    public abstract void IAControl();
}
