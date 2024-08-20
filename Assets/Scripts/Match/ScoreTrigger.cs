using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    [Tooltip("Chose the side that you want to gain a Point")]
    [SerializeField] private ArenaSide _sideGettingPoint;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Ball")){
            EventManager.MatchManger.OnScore.Get().Invoke(_sideGettingPoint);
        }
    }
}
