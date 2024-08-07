using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _accelerationStart = 200.0f;
    [Tooltip("Acceleration add every time the ball bouncy in the paddle")]
    [SerializeField] private float _accelerationScale = 8.0f;
    private float _accelerationCurrent = 0.0f;
    private void Start(){
        _rigidbody = GetComponent<Rigidbody2D>();
        ResetBall();
    }

    public void ResetBall(){
        _accelerationCurrent = _accelerationStart;
        ResetVelocity();
        transform.localPosition = Vector2.zero;
        Invoke(nameof(AddStartingForce),1.5f);
    }

    private void ResetVelocity(){
        _rigidbody.velocity = Vector2.zero;
    }

    private void AddStartingForce(){
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);

        _rigidbody.AddForce(new Vector2(x,y) * _accelerationCurrent);
    }

    public void AddForce(Vector2 force){
        _accelerationCurrent += _accelerationScale;
        ResetVelocity();
        _rigidbody.AddForce(force * _accelerationCurrent);
    }
}