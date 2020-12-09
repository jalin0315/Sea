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
        for (int _i = 0; _i < _Audio.Length; _i++)
        {
            _Audio[_i]._AudioSource = gameObject.AddComponent<AudioSource>();
            _Audio[_i]._AudioSource.enabled = false;
            _Audio[_i]._AudioSource.clip = _Audio[_i]._AudioClip;
            _Audio[_i]._AudioSource.volume = _Audio[_i]._Volume;
            _Audio[_i]._AudioSource.pitch = 1.0f;
            _Audio[_i]._AudioSource.playOnAwake = _Audio[_i]._PlayOnAwake;
            _Audio[_i]._AudioSource.loop = _Audio[_i]._Loop;
            _Audio[_i]._AudioSource.enabled = true;
        }
    }

    public void Play(string _name)
    {
        var _var = Array.Find(_Audio, _x => _x._Name == _name);
        if (_var == null)
        {
            Debug.LogWarningFormat("Play({0}) not find.", _name);
            return;
        }
        _var._AudioSource.Play();
    }
    public void PlayOneShot(string _name)
    {
        var _var = Array.Find(_Audio, _x => _x._Name == _name);
        if (_var == null)
        {
            Debug.LogWarningFormat("PlayOneShot({0}) not find.", _name);
            return;
        }
        _var._AudioSource.PlayOneShot(_var._AudioClip);
    }
    public void Pause(string _name)
    {
        var _var = Array.Find(_Audio, _x => _x._Name == _name);
        if (_var == null)
        {
            Debug.LogWarningFormat("Pause({0}) not find.", _name);
            return;
        }
        _var._AudioSource.Pause();
    }
    public void PauseAll()
    {
        for (int _i = 0; _i < _Audio.Length; _i++)
        {
            if (_Audio[_i]._AudioClip == null)
            {
                Debug.LogWarningFormat("PauseAll(Audio name: {0} is null.)", _Audio[_i]._Name);
                continue;
            }
            _Audio[_i]._AudioSource.Pause();
        }
    }
    public void UnPause(string _name)
    {
        var _var = Array.Find(_Audio, _x => _x._Name == _name);
        if (_var == null)
        {
            Debug.LogWarningFormat("UnPause({0}) not find.", _name);
            return;
        }
        _var._AudioSource.UnPause();
    }
    public void UnPauseAll()
    {
        for (int _i = 0; _i < _Audio.Length; _i++)
        {
            if (_Audio[_i]._AudioClip == null)
            {
                Debug.LogWarningFormat("UnPauseAll(Audio name: {0} is null.)", _Audio[_i]._Name);
                continue;
            }
            _Audio[_i]._AudioSource.UnPause();
        }
    }
    public void Stop(string _name)
    {
        var _var = Array.Find(_Audio, _x => _x._Name == _name);
        if (_var == null)
        {
            Debug.LogWarningFormat("Stop({0}) not find.", _name);
            return;
        }
        _var._AudioSource.Stop();
    }
    public void StopAll()
    {
        for (int _i = 0; _i < _Audio.Length; _i++)
        {
            if (_Audio[_i]._AudioClip == null)
            {
                Debug.LogWarningFormat("StopAll(Audio name: {0} is null.)", _Audio[_i]._Name);
                continue;
            }
            _Audio[_i]._AudioSource.Stop();
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
