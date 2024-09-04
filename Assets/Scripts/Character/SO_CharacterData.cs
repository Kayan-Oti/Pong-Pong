using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "DATA/CharacterData")]
public class SO_CharacterData : ScriptableObject
{
    public float speed = 10;
    public float maxForce = 3.0f;
    public float minForce = 1.0f;

    public AnimationCurve easeResetPosition;
}
