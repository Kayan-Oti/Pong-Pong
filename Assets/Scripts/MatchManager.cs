using UnityEngine;

public class MatchManager : MonoBehaviour
{
    private int _scorePlayerLeft, _scorePlayerRight;

    public void AddPoint(PlayerSide side){
        if(side.Equals(PlayerSide.Left))
            _scorePlayerLeft++;
        else
            _scorePlayerRight++;
    }
}
