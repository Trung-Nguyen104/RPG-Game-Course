using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public bool playBGM;
    [SerializeField] private AudioSource[] sfxSources;
    [SerializeField] private AudioSource[] bgmSources;
    [SerializeField] private float audioDistance;

    private Dictionary<int, Coroutine> fadeCoroutines = new();
    private int bgmPlaying;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
        GetAudioSources();
    }

    private void Update()
    {
        if (!playBGM)
        {
            StopAllBGM();
        }
        else
        {
            if (!bgmSources[bgmPlaying].isPlaying)
            {
                PlayBGM(bgmPlaying);
            }
        }
    }

    private void GetAudioSources()
    {
        Transform sfxParent = transform.Find("SFX");
        Transform bgmParent = transform.Find("BGM");

        if (sfxParent != null)
            sfxSources = sfxParent.GetComponentsInChildren<AudioSource>();

        if (bgmParent != null)
            bgmSources = bgmParent.GetComponentsInChildren<AudioSource>();
    }

    public void PlaySFX(int _index, float _time = 0f)
    {
        if (_index < 0 || _index >= sfxSources.Length) return;

        var audio = sfxSources[_index];

        if (fadeCoroutines.TryGetValue(_index, out var existing))
        {
            if (existing != null)
            {
                StopCoroutine(existing);
            }
            fadeCoroutines.Remove(_index);
        }

        audio.pitch = Random.Range(0.9f, 1f);
        audio.volume = 0f;
        audio.Play();

        if (gameObject.activeInHierarchy)
        {
            fadeCoroutines[_index] = StartCoroutine(FadeIn(audio, _time));
        }
    }

    public void StopSFX(int _index, float _time = 0f)
    {
        if (_index < 0 || _index >= sfxSources.Length) return;

        var audio = sfxSources[_index];

        if (fadeCoroutines.TryGetValue(_index, out var existing))
        {
            if (existing != null)
            {
                StopCoroutine(existing);
            }
            fadeCoroutines.Remove(_index);
        }

        if (_time <= 0f)
        {
            audio.Stop();
            return;
        }

        if (gameObject.activeInHierarchy)
        {
            Coroutine routine = StartCoroutine(FadeOutAndStop(audio, _time));
            if (routine != null)
            {
                fadeCoroutines[_index] = routine;
            }
        }
    }

    private IEnumerator FadeIn(AudioSource audio, float duration)
    {
        float t = 0f;
        float startVolume = 0f;
        float targetVolume = 1f;

        while (t < duration)
        {
            t += Time.deltaTime;
            audio.volume = Mathf.Lerp(startVolume, targetVolume, t / duration);
            yield return null;
        }

        audio.volume = targetVolume;
    }

    private IEnumerator FadeOutAndStop(AudioSource source, float duration)
    {
        float startVolume = source.volume;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            yield return null;
        }

        source.volume = 0f;
        source.Stop();
        source.volume = startVolume;
    }

    public void PlayBGM(int _index)
    {
        bgmPlaying = _index;
        StopAllBGM();
        bgmSources[bgmPlaying].Play();
    }

    public void StopAllBGM()
    {
        for (int i = 0; i < bgmSources.Length; i++)
        {
            bgmSources[i].Stop();
        }
    }
}
