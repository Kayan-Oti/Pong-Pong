using UnityEngine;

public class Character : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    [SerializeField] protected float _speed = 10;
    private void Start(){
        _rigidbody = GetComponent<Rigidbody2D>();
    }
}