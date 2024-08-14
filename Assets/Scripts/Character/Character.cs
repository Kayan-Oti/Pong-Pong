using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Character Settings")]
    [SerializeField] protected ArenaSide _playerSide = ArenaSide.Left;
    
    [SerializeField] protected SO_CharacterData _characterData;
    protected Rigidbody2D _rigidbody;
    protected Paddle _paddle;
    protected Vector2 _direction;
    protected Vector2 _defaultPosition;
    protected bool _canMove;

    private void OnEnable(){
        EventManager.MatchManger.OnStartRound.Get().AddListener(StartRoundSetup);
        EventManager.MatchManger.OnEndRound.Get().AddListener(EndRoundSetup);

    }

    private void OnDisable() {
        EventManager.MatchManger.OnStartRound.Get().RemoveListener(StartRoundSetup); 
        EventManager.MatchManger.OnEndRound.Get().RemoveListener(EndRoundSetup);
    }

    private void Start(){
        _rigidbody = GetComponent<Rigidbody2D>();
        _paddle = GetComponentInChildren<Paddle>();
        _paddle.Setup(_playerSide,_characterData.maxForce, _characterData.minForce);
        _defaultPosition = transform.localPosition;
    }

    public void StartRoundSetup()
    {
        _paddle.Reset();
        transform.localPosition = _defaultPosition;
        _canMove = true;
    }

    public void EndRoundSetup()
    {
        _canMove = false;
        _direction = Vector2.zero;
    }
}