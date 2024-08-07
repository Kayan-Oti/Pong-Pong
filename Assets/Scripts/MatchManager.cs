using UnityEngine;

public class MatchManager : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private ScoreManager _scoreManager;
    private int _scorePlayerLeft = 0, _scorePlayerRight = 0;

    private void OnEnable() {
        EventManager.MatchManger.OnScoreTrigger.Get().AddListener(OnScoreTrigger);
    }

    private void OnDisable() {
        EventManager.MatchManger.OnScoreTrigger.Get().RemoveListener(OnScoreTrigger);
    }

    public void OnScoreTrigger(Component component, ArenaSide side){
        if(side.Equals(ArenaSide.Left)){
            _scorePlayerLeft++;
            _scoreManager.ChangeScore(side, _scorePlayerLeft);
        }
        else{
            _scorePlayerRight++;
            _scoreManager.ChangeScore(side, _scorePlayerRight);
        }

        _ball.ResetBall();
    }
}
