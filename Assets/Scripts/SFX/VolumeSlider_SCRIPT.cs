using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider_SCRIPT : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] string playerPrefsKey;
    [SerializeField] string mixerExposedParamName;

    void Start()
    {
        float volume = PlayerPrefs.GetFloat(playerPrefsKey, 100);
        SetVolume(volume);
    }

    public void SetVolume(float volume)
    {
        if (volume < 0.1f) volume = 0.001f;

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