using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Audio;
using System.Linq;
using System;

public class AudioManager_SCRIPT : MonoBehaviour
{
    public static AudioManager_SCRIPT Instance;

    public AudioMixer masterMixer;
    public AudioMixerGroup generalAudioGroup;
    public AudioMixerGroup musicAudioGroup;
    public AudioMixerGroup uiAudioGroup;

    [SerializeField] private int generalPoolSize = 16;
    [SerializeField] private int musicPoolSize = 1;
    [SerializeField] private int uiPoolSize = 4;

    private List<AudioSourcePoolItem_CLASS> generalSourcePool;
    private List<AudioSourcePoolItem_CLASS> musicSourcePool;
    private List<AudioSourcePoolItem_CLASS> uiSourcePool;

    // Dictionary to manage looping sounds by AudioClip
    private Dictionary<AudioClip, List<AudioSource>> loopingSources = new Dictionary<AudioClip, List<AudioSource>>();
    private Dictionary<string, List<AudioSource>> taggedLoopingSources = new Dictionary<string, List<AudioSource>>();
    // Dictionary to track active fade coroutines for each AudioSource
    private Dictionary<AudioSource, Coroutine> activeFadeCoroutines = new Dictionary<AudioSource, Coroutine>();

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
        UpdateMixerVolumes();

        InitializeGeneralAudioSourcePool();
        InitializeMusicAudioSourcePool();
        InitializeUISourcePool();
    }

    #region Initialization
    private void InitializeGeneralAudioSourcePool()
    {
        generalSourcePool = new List<AudioSourcePoolItem_CLASS>(generalPoolSize);
        for (int i = 0; i < generalPoolSize; i++)
        {
            GameObject audioSourceGameObject = new GameObject("GeneralSource_" + i);
            audioSourceGameObject.transform.parent = transform;
            AudioSource audioSource = audioSourceGameObject.AddComponent<AudioSource>();
            generalSourcePool.Add(new AudioSourcePoolItem_CLASS(audioSource, audioSourceGameObject));
            audioSource.outputAudioMixerGroup = generalAudioGroup;
        }
    }

    private void InitializeMusicAudioSourcePool()
    {
        musicSourcePool = new List<AudioSourcePoolItem_CLASS>(musicPoolSize);
        for (int i = 0; i < musicPoolSize; i++)
        {
            GameObject audioSourceGameObject = new GameObject("MusicSource_" + i);
            audioSourceGameObject.transform.parent = transform;
            AudioSource audioSource = audioSourceGameObject.AddComponent<AudioSource>();
            musicSourcePool.Add(new AudioSourcePoolItem_CLASS(audioSource, audioSourceGameObject));
            audioSource.outputAudioMixerGroup = musicAudioGroup;
        }
    }

    private void InitializeUISourcePool()
    {
        uiSourcePool = new List<AudioSourcePoolItem_CLASS>(uiPoolSize);
        for (int i = 0; i < uiPoolSize; i++)
        {
            GameObject audioSourceGameObject = new GameObject("UISource_" + i);
            audioSourceGameObject.transform.parent = transform;
            AudioSource audioSource = audioSourceGameObject.AddComponent<AudioSource>();
            uiSourcePool.Add(new AudioSourcePoolItem_CLASS(audioSource, audioSourceGameObject));
            audioSource.outputAudioMixerGroup = uiAudioGroup;
        }
    }
    #endregion

    #region PlaySound
    public void PlaySound(
        AudioClip clip,
        GameObject emitter = null,
        AudioMixerGroup audioGroup = null,
        float volume = 1f,
        float pitch = 1f,
        float spatialBlend = 0f,
        bool loop = false,
        string loopTag = null)
    {
        if (clip == null)
        {
            Debug.LogWarning("Sound was not assigned to PlaySound() function");
            return;
        }

        AudioSourcePoolItem_CLASS poolItem = null;

        if (audioGroup == null || audioGroup == generalAudioGroup)
            poolItem = generalSourcePool.Find(item => !item.AudioSource.isPlaying);
        else if (audioGroup == musicAudioGroup)
            poolItem = musicSourcePool.Find(item => !item.AudioSource.isPlaying);
        else if (audioGroup == uiAudioGroup)
            poolItem = uiSourcePool.Find(item => !item.AudioSource.isPlaying);

        if (poolItem == null)
        {
            Debug.LogWarning("No available audio source to play sound.");
            return;
        }

        AudioSource source = poolItem.AudioSource;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
        source.spatialBlend = spatialBlend;

        if (emitter != null)
        {
            source.gameObject.transform.position = emitter.transform.position;
        }
        else
        {
            source.gameObject.transform.position = Camera.main.transform.position;
        }

        source.clip = clip;
        source.Play();

        if (loop)
        {
            if (!string.IsNullOrEmpty(loopTag))
            {
                // Handle tagged looping sounds
                if (!taggedLoopingSources.ContainsKey(loopTag))
                {
                    taggedLoopingSources[loopTag] = new List<AudioSource>();
                }
                taggedLoopingSources[loopTag].Add(source);
            }
            else
            {
                // Handle untagged looping sounds based on AudioClip
                if (!loopingSources.ContainsKey(clip))
                {
                    loopingSources[clip] = new List<AudioSource>();
                }
                loopingSources[clip].Add(source);
            }
        }
    }


    /// <summary>
    /// Plays random sound from the list
    /// </summary>
    public void PlayRandomSound(List<AudioClip> clips,
        GameObject emitter = null,
        AudioMixerGroup audioGroup = null,
        float volume = 1f,
        float pitch = 1f,
        float spatialBlend = 0f,
        bool loop = false,
        string loopTag = null)
    {
        if (clips == null || clips.Count == 0)
        {
            Debug.LogWarning("Sounds were not assigned to PlayRandomSound() function");
            return;
        }
        
        PlaySound(clips[UnityEngine.Random.Range(0,clips.Count)], emitter, audioGroup, volume, pitch, spatialBlend,loop,loopTag);
    }
    #endregion

    #region GetAudioSource
    public AudioSource GetFirstPlayingAudioSource(AudioClip clip, AudioMixerGroup audioGroup = null)
    {
        if (audioGroup != null)
        {
            List<AudioSourcePoolItem_CLASS> sourcePool = GetSourcePoolByGroup(audioGroup);
            if (sourcePool == null)
                return null;

            foreach (var poolItem in sourcePool)
            {
                if (poolItem.AudioSource.isPlaying && poolItem.AudioSource.clip == clip)
                {
                    return poolItem.AudioSource;
                }
            }
        }
        else
        {
            // Search across all source pools if no AudioMixerGroup is provided
            List<List<AudioSourcePoolItem_CLASS>> allPools = new List<List<AudioSourcePoolItem_CLASS>>
            {
                generalSourcePool,
                musicSourcePool,
                uiSourcePool
            };

            foreach (var pool in allPools)
            {
                foreach (var poolItem in pool)
                {
                    if (poolItem.AudioSource.isPlaying && poolItem.AudioSource.clip == clip)
                    {
                        return poolItem.AudioSource;
                    }
                }
            }
        }

        return null;
    }

    public AudioSource GetFirstPlayingAudioSource(List<AudioClip> clips, AudioMixerGroup audioGroup = null)
    {
        if (clips == null || clips.Count == 0)
        {
            Debug.LogWarning("No AudioClips provided.");
            return null;
        }

        // Convert list to a HashSet for faster lookup
        HashSet<AudioClip> clipSet = new HashSet<AudioClip>(clips);

        List<List<AudioSourcePoolItem_CLASS>> poolsToSearch;

        if (audioGroup != null)
        {
            List<AudioSourcePoolItem_CLASS> specificPool = GetSourcePoolByGroup(audioGroup);
            if (specificPool == null)
                return null;

            poolsToSearch = new List<List<AudioSourcePoolItem_CLASS>> { specificPool };
        }
        else
        {
            poolsToSearch = new List<List<AudioSourcePoolItem_CLASS>>
            {
                generalSourcePool,
                musicSourcePool,
                uiSourcePool
            };
        }

        foreach (var pool in poolsToSearch)
        {
            foreach (var poolItem in pool)
            {
                if (poolItem.AudioSource.isPlaying && clipSet.Contains(poolItem.AudioSource.clip))
                {
                    return poolItem.AudioSource;
                }
            }
        }

        return null;
    }

    private List<AudioSourcePoolItem_CLASS> GetSourcePoolByGroup(AudioMixerGroup audioGroup)
    {
        if (audioGroup == generalAudioGroup)
            return generalSourcePool;
        else if (audioGroup == musicAudioGroup)
            return musicSourcePool;
        else if (audioGroup == uiAudioGroup)
            return uiSourcePool;
        else
        {
            Debug.LogWarning("Invalid AudioMixerGroup provided.");
            return null;
        }
    }
    #endregion

    /// <summary>
    /// Stops first AudioSource with this AudioClip, even if it's a loop
    /// </summary>
    public void StopSound(AudioClip clip, AudioMixerGroup audioGroup = null)
    {
        var audioSource = GetFirstPlayingAudioSource(clip, audioGroup);
        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource found playing the clip");
            return;
        }

        audioSource.Stop();

        if (audioSource.loop)
        {
            if (loopingSources.ContainsKey(clip))
            {
                loopingSources[clip].Remove(audioSource);
                if (loopingSources[clip].Count == 0)
                {
                    loopingSources.Remove(clip);
                }
            }
        }
    }

    #region LoopingSounds
    public void StopLoopingSound(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("Clip is null in StopLoopingSound");
            return;
        }

        if (loopingSources.ContainsKey(clip))
        {
            List<AudioSource> sourcesToStop = new List<AudioSource>(loopingSources[clip]);

            foreach (var source in sourcesToStop)
            {
                if (source.isPlaying)
                {
                    source.Stop();
                    loopingSources[clip].Remove(source);
                }
            }

            if (loopingSources[clip].Count == 0)
            {
                loopingSources.Remove(clip);
            }
        }
        else
        {
            Debug.LogWarning("No looping sound found for the given clip");
        }
    }


    public void StopLoopingSoundsByTag(string tag, bool excludeTagged = false, float fadeDuration = 0f)
    {
        if (string.IsNullOrEmpty(tag))
        {
            Debug.LogWarning("Tag is null or empty in StopLoopingSoundsByTag function");
            return;
        }

        Action<AudioSource> stopAction = source =>
        {
            // If a fade is already in progress, stop it
            if (activeFadeCoroutines.ContainsKey(source))
            {
                StopCoroutine(activeFadeCoroutines[source]);
                activeFadeCoroutines.Remove(source);
            }

            if (fadeDuration > 0f)
            {
                // Start a new fade coroutine and track it
                Coroutine fadeCoroutine = StartCoroutine(FadeOutAndStop(source, fadeDuration));
                activeFadeCoroutines[source] = fadeCoroutine;
            }
            else
            {
                source.Stop();
                RemoveSourceFromTag(tag, source);
            }
        };

        if (excludeTagged)
        {
            foreach (var kvp in new Dictionary<string, List<AudioSource>>(taggedLoopingSources))
            {
                if (kvp.Key.Equals(tag, StringComparison.OrdinalIgnoreCase))
                    continue;

                foreach (var source in new List<AudioSource>(kvp.Value))
                {
                    if (source.isPlaying)
                    {
                        stopAction(source);
                    }
                }

                if (kvp.Value.Count == 0)
                {
                    taggedLoopingSources.Remove(kvp.Key);
                }
            }
        }
        else
        {
            if (taggedLoopingSources.ContainsKey(tag))
            {
                foreach (var source in new List<AudioSource>(taggedLoopingSources[tag]))
                {
                    if (source.isPlaying)
                    {
                        stopAction(source);
                    }
                }

                if (taggedLoopingSources[tag].Count == 0)
                {
                    taggedLoopingSources.Remove(tag);
                }
            }
            else
            {
                Debug.LogWarning($"No looping sounds found with tag: {tag}");
            }
        }
    }

    private void RemoveSourceFromTag(string tag, AudioSource source)
    {
        if (taggedLoopingSources.ContainsKey(tag))
        {
            taggedLoopingSources[tag].Remove(source);
            if (taggedLoopingSources[tag].Count == 0)
            {
                taggedLoopingSources.Remove(tag);
            }
        }
    }

    // Slowly decrease volume of sounds that need to be stopped
    private IEnumerator FadeOutAndStop(AudioSource source, float duration)
    {
        float startVolume = source.volume;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, 0f, elapsed / duration);
            yield return null;
        }

        source.Stop();
        source.volume = startVolume;

        if (activeFadeCoroutines.ContainsKey(source))
        {
            activeFadeCoroutines.Remove(source);
        }
    }

    public void StopAllLoopingSounds()
    {
        // Stop untagged looping sounds
        var allClips = loopingSources.Keys.ToList();

        foreach (var clip in allClips)
        {
            List<AudioSource> sourcesToStop = new List<AudioSource>(loopingSources[clip]);

            foreach (var source in sourcesToStop)
            {
                if (source.isPlaying)
                {
                    source.Stop();
                    loopingSources[clip].Remove(source);
                }
            }

            if (loopingSources[clip].Count == 0)
            {
                loopingSources.Remove(clip);
            }
        }

        // Stop tagged looping sounds
        var allTags = taggedLoopingSources.Keys.ToList();

        foreach (var tag in allTags)
        {
            List<AudioSource> sourcesToStop = new List<AudioSource>(taggedLoopingSources[tag]);

            foreach (var source in sourcesToStop)
            {
                if (source.isPlaying)
                {
                    source.Stop();
                    taggedLoopingSources[tag].Remove(source);
                }
            }

            if (taggedLoopingSources[tag].Count == 0)
            {
                taggedLoopingSources.Remove(tag);
            }
        }
    }
    #endregion

    public void UpdateMixerVolumes()
    {
        masterMixer.SetFloat("Master Volume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume", 1f)) * 20);
        generalAudioGroup.audioMixer.SetFloat("General Volume", Mathf.Log10(PlayerPrefs.GetFloat("GeneralVolume", 1f)) * 20);
        musicAudioGroup.audioMixer.SetFloat("Music Volume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume", 1f)) * 20);
        uiAudioGroup.audioMixer.SetFloat("UI Volume", Mathf.Log10(PlayerPrefs.GetFloat("UIVolume", 1f)) * 20);
    }
}