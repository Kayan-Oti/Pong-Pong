using UnityEngine;

public class Paddle : MonoBehaviour
{
    public enum AimSide{
        Top,
        Down
    }

    private float _maxForce;
    private float _minForce;
    private float _xForceMultiply = 1.0f;
    private float _yForceAngle = -1.0f;
    private AimSide _aimSide = AimSide.Down;

    private Collider2D _collider2D;

    public void Setup(ArenaSide side, float max, float min){
        _collider2D = GetComponent<Collider2D>();
        _maxForce = max;
        _minForce = min;
        SetupArenaSide(side);
    }

    public void Reset(){
        if(_aimSide != AimSide.Down)
            ChangeAimSide();
        _aimSide = AimSide.Down;
    }

    private void SetupArenaSide(ArenaSide side){
        //Dont Change if Side is Left
        if(side.Equals(ArenaSide.Left))
            return;
        
        //Chande if Side is Right
        _maxForce *= -1;
        _minForce *= -1;
    }

    public void ChangeAimSide(){
        _aimSide = _aimSide == AimSide.Top ? AimSide.Down : AimSide.Top;
        _yForceAngle *= -1;
        Debug.Log("ChangeAimSide");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Ball"))
        {
            //Setup
            Ball ball = other.GetComponent<Ball>();
            float ballPosY = ball.gameObject.transform.position.y;
            Bounds paddleBound = _collider2D.bounds;
            float yMaxGlobalPosition = gameObject.transform.parent.position.y + paddleBound.extents.y;
            float yMinGlobalPosition = gameObject.transform.parent.position.y - paddleBound.extents.y;

            //Calculation
            float gapDistance = _aimSide == AimSide.Top ? ballPosY - yMinGlobalPosition : yMaxGlobalPosition - ballPosY;
            float precisionPercent = gapDistance / (yMaxGlobalPosition - yMinGlobalPosition);
            if (precisionPercent > 1) //Distante do ponto max
                precisionPercent = 1;
            else if (precisionPercent < 0.2) //Próximo o suficiente do ponto max
                precisionPercent = 0;

            //Applaying
            _xForceMultiply = _minForce + ((1 - precisionPercent) * (_maxForce-_minForce));
            Debug.Log("perCent: "+(1 - precisionPercent));
            ball.AddForce(new Vector2(_xForceMultiply, _yForceAngle));
        }
    }
}
