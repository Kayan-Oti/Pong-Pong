using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Character Settings")]
    [SerializeField] protected ArenaSide _playerSide = ArenaSide.Left;
    
    [SerializeField] protected SO_CharacterData _characterData;
    protected Rigidbody2D _rigidbody;
    protected Paddle _paddle;
    protected Vector2 _direction;
    private void Start(){
        _rigidbody = GetComponent<Rigidbody2D>();
        _paddle = GetComponentInChildren<Paddle>();
        _paddle.Setup(_playerSide,_characterData.maxForce, _characterData.minForce);
    }
}