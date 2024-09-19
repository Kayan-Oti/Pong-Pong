using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DATA/LevelsUnlockData")]
public class SO_LevelsUnlock : ScriptableObject
{
    public LevelUnlock[] LevelsUnlock;
}

[System.Serializable]
public class LevelUnlock{
    [HideInInspector] public string name;
    public bool unlock;
}