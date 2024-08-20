using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _accelerationStart = 200.0f;
    [Tooltip("Acceleration add every time the ball bouncy in the paddle")]
    [SerializeField] private float _accelerationScale = 8.0f;
    private float _accelerationCurrent = 0.0f;

    //Components
    private Rigidbody2D _rigidbody;
    private Animator _animatorController;

    private void Start(){
        _rigidbody = GetComponent<Rigidbody2D>();
        _animatorController = GetComponent<Animator>();
        ResetBall();
    }

    public void ResetBall(){
        _accelerationCurrent = _accelerationStart;
        ResetVelocity();
        transform.localPosition = Vector2.zero;
        transform.rotation = Quaternion.identity;
        _animatorController.SetBool("isMoving", false);
    }

    private void ResetVelocity(){
        _rigidbody.velocity = Vector2.zero;
    }

    public void AddStartingForce(){
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);
        Vector2 force = new Vector2(x,y) * _accelerationCurrent;

        _rigidbody.AddForce(force);
        ChangeFacingDirection(force);
        
        _animatorController.SetBool("isMoving", true);
    }

    public void AddForce(Vector2 force){
        _accelerationCurrent += _accelerationScale;
        ResetVelocity();

        _rigidbody.AddForce(force * _accelerationCurrent);
        ChangeFacingDirection(force);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        ChangeFacingDirection(_rigidbody.velocity);
    }

    private void ChangeFacingDirection(Vector2 direction){
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction.normalized);
    }
}