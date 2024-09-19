using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class Manager_Sound : MonoBehaviour
{
    [SerializeField] private SO_Sounds SO;
    private static Manager_Sound instance = null;
    private AudioSource audioSource;

    private void Awake()
    {
        if(!instance)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
        }
   }

    public static void PlaySound(SoundType sound, AudioSource source = null, float volume = 1)
    {
        SoundList soundList = instance.SO.sounds[(int)sound];
        AudioClip[] clips = soundList.sounds;
        AudioClip randomClip = clips[Random.Range(0, clips.Length)];

        if(source)
        {
            source.outputAudioMixerGroup = soundList.mixer;
            source.clip = randomClip;
            source.volume = volume * soundList.volume;
            source.Play();
        }
        else
        {
            instance.audioSource.outputAudioMixerGroup = soundList.mixer;
            instance.audioSource.PlayOneShot(randomClip, volume * soundList.volume);
        }
    }
}

[System.Serializable]
public struct SoundList
{
    [HideInInspector] public string name;
    [Range(0, 1)] public float volume;
    public AudioMixerGroup mixer;
    public AudioClip[] sounds;
}
