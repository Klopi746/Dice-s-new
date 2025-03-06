using UnityEngine;

public class AudioSourcePoolItem_CLASS
{
    public AudioSource AudioSource { get; private set; }
    public GameObject AudioSourceGameObject { get; private set; }

    public AudioSourcePoolItem_CLASS(AudioSource audioSource, GameObject audioSourceGameObject)
    {
        AudioSource = audioSource;
        AudioSourceGameObject = audioSourceGameObject;
    }
}