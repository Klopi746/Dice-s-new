using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider_SCRIPT : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] string playerPrefsKey;
    [SerializeField] string mixerExposedParamName;

    public float startVolume = 40;

    void Start()
    {
        startVolume = PlayerPrefs.GetFloat(playerPrefsKey, 40f);
        SetVolume(startVolume);
    }

    public void SetVolume(float volume)
    {
        if (volume < 1f) volume = 0.001f;

        RefreshSlider(volume);

        PlayerPrefs.SetFloat(playerPrefsKey, volume);

        audioMixer.SetFloat(mixerExposedParamName, Mathf.Log10(volume / 100) * 20f);
    }

    public void SetVolumeFromSlider()
    {
        SetVolume(volumeSlider.value);
    }

    public void RefreshSlider(float value)
    {
        volumeSlider.value = value;
    }
}
