using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public static AudioSystem _Instance;
    [SerializeField] private Audio[] _Audio;

    private void Awake()
    {
        _Instance = this;
        foreach (var _var in _Audio)
        {
            _var._AudioSource = gameObject.AddComponent<AudioSource>();
            _var._AudioSource.enabled = false;
            _var._AudioSource.clip = _var._AudioClip;
            _var._AudioSource.volume = _var._Volume;
            _var._AudioSource.pitch = 1.0f;
            _var._AudioSource.playOnAwake = _var._PlayOnAwake;
            _var._AudioSource.loop = _var._Loop;
            _var._AudioSource.enabled = true;
        }
    }

    public void Play(string _name)
    {
        var _var = Array.Find(_Audio, _Audio => _Audio._Name == _name);
        if (_var == null)
        {
            Debug.LogWarningFormat("Play({0}) not find.", _name);
            return;
        }
        _var._AudioSource.Play();
    }
    public void PlayOneShot(string _name)
    {
        var _var = Array.Find(_Audio, _Audio => _Audio._Name == _name);
        if (_var == null)
        {
            Debug.LogWarningFormat("PlayOneShot({0}) not find.", _name);
            return;
        }
        _var._AudioSource.PlayOneShot(_var._AudioClip);
    }
    public void Pause(string _name)
    {
        var _var = Array.Find(_Audio, _Audio => _Audio._Name == _name);
        if (_var == null)
        {
            Debug.LogWarningFormat("Pause({0}) not find.", _name);
            return;
        }
        _var._AudioSource.Pause();
    }
    public void PauseAll()
    {
        foreach (var _var in _Audio)
        {
            if (_var._AudioClip == null)
            {
                Debug.LogWarningFormat("PauseAll(Audio name: {0} is null.)", _var._Name);
                continue;
            }
            _var._AudioSource.Pause();
        }
    }
    public void UnPause(string _name)
    {
        var _var = Array.Find(_Audio, _Audio => _Audio._Name == _name);
        if (_var == null)
        {
            Debug.LogWarningFormat("UnPause({0}) not find.", _name);
            return;
        }
        _var._AudioSource.UnPause();
    }
    public void UnPauseAll()
    {
        foreach (var _var in _Audio)
        {
            if (_var._AudioClip == null)
            {
                Debug.LogWarningFormat("UnPauseAll(Audio name: {0} is null.)", _var._Name);
                continue;
            }
            _var._AudioSource.UnPause();
        }
    }
    public void Stop(string _name)
    {
        var _var = Array.Find(_Audio, _Audio => _Audio._Name == _name);
        if (_var == null)
        {
            Debug.LogWarningFormat("Stop({0}) not find.", _name);
            return;
        }
        _var._AudioSource.Stop();
    }
    public void StopAll()
    {
        foreach (var _var in _Audio)
        {
            if (_var._AudioClip == null)
            {
                Debug.LogWarningFormat("StopAll(Audio name: {0} is null.)", _var._Name);
                continue;
            }
            _var._AudioSource.Stop();
        }
    }
}

[Serializable]
public class Audio
{
    public string _Name;
    public AudioClip _AudioClip;
    [Range(0.0f, 1.0f)] public float _Volume;
    public bool _PlayOnAwake;
    public bool _Loop;
    [HideInInspector] public AudioSource _AudioSource;
}
