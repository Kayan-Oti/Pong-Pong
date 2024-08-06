using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Character Settings")]
    [SerializeField] protected PlayerSide _playerSide = PlayerSide.Left;
    [SerializeField] protected float _maxForce = 3.0f;
    [SerializeField] protected float _minForce = 1.0f;
    [SerializeField] protected float _speed = 10;
    protected Rigidbody2D _rigidbody;
    protected Paddle _paddle;
    protected Vector2 _direction;
    private void Start(){
        _rigidbody = GetComponent<Rigidbody2D>();
        _paddle = GetComponentInChildren<Paddle>();
        _paddle.Setup(_playerSide,_maxForce, _minForce);
    }
}