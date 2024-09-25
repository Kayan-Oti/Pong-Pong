#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;


[CustomEditor(typeof(SO_SoundsUI))]
public class SOEditor_SoundsUI : Editor
{
    private void OnEnable()
    {
        ref Sfx[] soundList = ref ((SO_SoundsUI)target).sounds;

        if (soundList == null)
            return;

        string[] names = Enum.GetNames(typeof(SoundUIType));
        bool differentSize = names.Length != soundList.Length;

        Dictionary<string, Sfx> sounds = new();

        if (differentSize)
        {
            for (int i = 0; i < soundList.Length; ++i)
            {
               sounds.Add(soundList[i].name, soundList[i]);
            }
        }

        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
        {
            string currentName = names[i];
            soundList[i].name = currentName;
            if (soundList[i].volume == 0) soundList[i].volume = 1;

            if (differentSize)
            {
                if (sounds.ContainsKey(currentName))
                {
                    Sfx current = sounds[currentName];
                    UpdateElement(ref soundList[i], current.volume, current.clip);
                }
                else
                    UpdateElement(ref soundList[i], 1);

                static void UpdateElement(ref Sfx element, float volume, AudioClip sound = null)
                {
                    element.volume = volume;
                    element.clip = sound;
                }
            }
        }
    }
}
#endif