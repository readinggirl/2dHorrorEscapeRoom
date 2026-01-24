using System;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip clip, Transform spawnTransform, float volume)
    {
        //instantiate gameobject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        //assign audio clip
        audioSource.clip = clip;
        //assign volume
        audioSource.volume = volume;
        //play sound
        audioSource.Play();
        //get length of sound
        float clipLength = audioSource.clip.length;
        //destroy after clip length
        Destroy(audioSource.gameObject, clipLength + 0.2f);
    }
}