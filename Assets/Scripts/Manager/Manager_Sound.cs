using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sfx
{
    [HideInInspector] public string name;
    [Range(0, 1)] public float volume = 1f;
    public AudioClip clip;
}

[RequireComponent(typeof(AudioSource))]
public class Manager_Sound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourcePrefab;
    [SerializeField] private SO_SoundsUI _soundsUI;
    public static Manager_Sound Instance;
    private AudioSource _audioSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
        }
   }

   public void PlaySound(Sfx sfx, Transform spawnTransform = null){
        //Default Transform
        if(spawnTransform == null)
            spawnTransform = transform;

        //Spawn AudioSource
        AudioSource audioSource = Instantiate(_audioSourcePrefab, spawnTransform.position, Quaternion.identity);

        audioSource.clip = sfx.clip;
        audioSource.volume = sfx.volume;
        audioSource.Play();

        float cliplength = sfx.clip.length;

        Destroy(audioSource.gameObject, cliplength);
    }

    public void PlaySoundRandom(Sfx[] sfx, Transform spawnTransform){
        int rand = Random.Range(0, sfx.Length);
        PlaySound(sfx[rand], spawnTransform);
    }

    public void PlaySoundUI(SoundUIType sound){
        Sfx soundList = _soundsUI.sounds[(int)sound];
        _audioSource.PlayOneShot(soundList.clip, soundList.volume);
    }
}