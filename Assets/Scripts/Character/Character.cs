using UnityEngine;
using DG.Tweening;

public abstract class Character : MonoBehaviour
{
    [Header("Character Settings")]
    [SerializeField] protected ArenaSide _playerSide = ArenaSide.Left;
    
    [SerializeField] protected SO_CharacterData _characterData;
    protected Vector2 _direction;
    protected Vector2 _defaultPosition;
    protected bool _canMove;

    //Components
    protected Rigidbody2D _rigidbody;
    protected Animator _animatorController;
    protected Paddle _paddle;

    #region Unity Setup
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
        _animatorController = GetComponentInChildren<Animator>();

        _paddle.Setup(_playerSide,_characterData.maxForce, _characterData.minForce);
        _defaultPosition = transform.localPosition;
    }

    #endregion

    #region Setup

    public void StartRoundSetup()
    {
        _paddle.Reset();
        transform.DOLocalMove(_defaultPosition, 0.5f).SetEase(_characterData.easeResetPosition);
        _canMove = true;
    }

    public void EndRoundSetup()
    {
        _canMove = false;
        _direction = Vector2.zero;
    }

    #endregion

    #region Update
    private void Update(){
        if (!_canMove)
            return;

        HandleInput();
    }

    private void FixedUpdate() {
        if(_direction.sqrMagnitude != 0){
            _rigidbody.AddForce(_direction * _characterData.speed);
            _animatorController.SetBool("isMoving", true);
        }else{
            _animatorController.SetBool("isMoving", false);
        }
    }

    #endregion

    //Abstract
    abstract public void HandleInput();
}