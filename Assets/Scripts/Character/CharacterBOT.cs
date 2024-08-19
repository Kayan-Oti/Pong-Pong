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
    
    public abstract void IAControl();
}
