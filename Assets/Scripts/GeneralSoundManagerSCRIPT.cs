using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSoundManagerSCRIPT : MonoBehaviour
{
    public static GeneralSoundManagerSCRIPT Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    private void Start()
    {

    }


    public List<AudioClip> MainMenuSoundList;


    public void PlayMusicWithDelay(float timeDelay)
    {
        StartCoroutine(PlayMusicRoutine(timeDelay));
    }
    private IEnumerator PlayMusicRoutine(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        AudioManager_SCRIPT.Instance.StopAllLoopingSounds();
        AudioManager_SCRIPT.Instance.PlayRandomSound(MainMenuSoundList, loop: true, audioGroup: AudioManager_SCRIPT.Instance.musicAudioGroup);
    }


    public AudioClip ButtSound;
    public void PlayButtSound()
    {
        AudioManager_SCRIPT.Instance.PlaySound(ButtSound);
    }
    public AudioClip DefeatSound;
    public void PlayDefeatSound()
    {
        AudioManager_SCRIPT.Instance.PlaySound(DefeatSound);
    }
    public AudioClip VictorySound;
    public void PlayVictorySound()
    {
        AudioManager_SCRIPT.Instance.PlaySound(VictorySound);
    }
    public AudioClip ShopWindClickSound;
    public void PlayShopWindClickSound()
    {
        AudioManager_SCRIPT.Instance.PlaySound(ShopWindClickSound);
    }
    public AudioClip ShopClickSound;
    public void PlayShopClickSound()
    {
        AudioManager_SCRIPT.Instance.PlaySound(ShopClickSound);
    }
    public AudioClip BuySound;
    public void PlayBuySound()
    {
        AudioManager_SCRIPT.Instance.PlaySound(BuySound);
    }
}