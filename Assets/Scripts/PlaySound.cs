using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] SoundType soundType;

    public void Play(){
        SoundManager.PlaySound(soundType);
    }
 }
