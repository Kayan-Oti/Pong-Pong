using UnityEngine;

public enum AimSide{
    Top,
    Down
}

public class Paddle : MonoBehaviour
{
    [SerializeField] private bool _isLeftSide = true;
    [SerializeField] private float _maxForce = 3.0f;
    [SerializeField] private float _minForce = 1.5f;
    private float _xForceMultiply = 1.0f;
    private float _yForceAngle = -1.0f;
    private AimSide aimSide = AimSide.Down;

    private Collider2D _collider2D;

    private void Start(){
        _collider2D = GetComponent<Collider2D>();
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            ChangeSide();
        }
    }

    private void ChangeSide(){
        aimSide = aimSide == AimSide.Top ? AimSide.Down : AimSide.Top;
        _yForceAngle *= -1;
        Debug.Log("ChangeSide");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Ball"))
        {
            Ball ball = other.GetComponent<Ball>();
            float ballPosY = ball.gameObject.transform.position.y;
            Bounds paddleBound = _collider2D.bounds;
            float yMaxGlobalPosition = gameObject.transform.parent.position.y + paddleBound.extents.y;
            float yMinGlobalPosition = gameObject.transform.parent.position.y - paddleBound.extents.y;

            float gapDistance = aimSide == AimSide.Top ? ballPosY - yMinGlobalPosition : yMaxGlobalPosition - ballPosY;
            float precisionPercent = gapDistance / (yMaxGlobalPosition - yMinGlobalPosition);
            if (precisionPercent > 1) //Distante do ponto max
                precisionPercent = 1;
            else if (precisionPercent < 0.1) //Próximo o suficiente do ponto max
                precisionPercent = 0;

            _xForceMultiply = _minForce + ((1 - precisionPercent) * (_maxForce-_minForce));

            Debug.Log("perCent: "+(1 - precisionPercent));

            ball.AddForce(new Vector2(_xForceMultiply, _yForceAngle));
        }
    }
}
