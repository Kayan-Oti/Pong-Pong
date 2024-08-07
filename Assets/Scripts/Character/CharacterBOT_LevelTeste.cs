using UnityEngine;

public class CharacterBOT_LevelTeste : CharacterBOT
{
    public override void IAControl()
    {
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
}
