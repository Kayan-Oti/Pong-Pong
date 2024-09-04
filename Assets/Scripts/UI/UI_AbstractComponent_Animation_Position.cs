using UnityEngine;

public abstract class UI_AbstractComponent_Animation_Position : UI_AbstractComponent_Animation
{
    [Header("Distance")]
    [SerializeField] protected Vector2 _distanceStart = new Vector2(300, 300);
    [SerializeField] protected Vector2 _distanceEnd = new Vector2(300, 300);

    //Values
    protected Vector2 _defaultPos;
    protected Vector2 _startPos, _endPos;
}
