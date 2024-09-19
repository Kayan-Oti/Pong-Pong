using System.Collections.Generic;
using UnityEngine;

public class Manager_DATA : MonoBehaviour
{
    public static readonly Dictionary<Levels, SceneIndex> DictionaryLevelsToSceneIndex = new Dictionary<Levels, SceneIndex>{
        {Levels.Level1, SceneIndex.Level1},
        {Levels.Level2, SceneIndex.Level2},
        {Levels.Level3, SceneIndex.Level3},
        {Levels.Level4, SceneIndex.Level4},
    };

    public static Manager_DATA Instance;
    [SerializeField] private SO_LevelsUnlock LevelsUnlock;

    private void Awake(){
        Instance = this;
    }

    #region Levels Unlock
    public SO_LevelsUnlock GetLevelsUnlockDATA(){
        return LevelsUnlock;
    }

    public void UnlockLevel(Levels level){
        LevelsUnlock.LevelsUnlock[(int)level].unlock = true;
    }

    #endregion
}
