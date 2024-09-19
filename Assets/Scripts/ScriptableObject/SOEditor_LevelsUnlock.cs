#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SO_LevelsUnlock))]
public class SOEditor_LevelsUnlock : Editor
{
    private void OnEnable()
    {
        //Recebe referencia do SO
        ref LevelUnlock[] levelsUnlockList = ref ((SO_LevelsUnlock)target).LevelsUnlock;

        if (levelsUnlockList == null)
            return;

        //Recebe os nomes dos Levels no Enum
        string[] levelNames = Enum.GetNames(typeof(Levels));

        //Reajusta a lista para ter o mesmo tamanho do Enum
        Array.Resize(ref levelsUnlockList, levelNames.Length);

        //Torna o nome de cada LevelUnlock do SO no name no Enum
        for (int i = 0; i < levelsUnlockList.Length; i++)
            levelsUnlockList[i].name = levelNames[i];
    }
}
#endif