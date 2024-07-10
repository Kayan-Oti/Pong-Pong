using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed = 500.0f;
    private void Start(){
        _rigidbody = GetComponent<Rigidbody2D>();
        AddStartingForce();
    }

    private void AddStartingForce(){
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);

        _rigidbody.AddForce(new Vector2(x,y) * _speed);
    }
    private void ResetVelocity(){
        _rigidbody.velocity = Vector2.zero;
    }

    public void AddForce(Vector2 force){
        ResetVelocity();
        _rigidbody.AddForce(force * _speed);
    }

}
