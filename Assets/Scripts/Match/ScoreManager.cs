using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _leftScoreText, _rightScoreText;

    public void ChangeScore(ArenaSide side, int currentPoint){
        if(side.Equals(ArenaSide.Left))
            _leftScoreText.text = currentPoint.ToString();
        else
            _rightScoreText.text = currentPoint.ToString();
    }
}
