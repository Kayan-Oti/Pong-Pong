using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    [SerializeField] private ArenaSide _side;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Ball")){
            EventManager.MatchManger.OnScoreTrigger.Get().Invoke(this, _side);
        }
    }
}
