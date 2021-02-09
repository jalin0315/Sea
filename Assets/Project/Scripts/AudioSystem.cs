using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTJ
{
    public class AudioSystem : MonoBehaviour
    {
        public static AudioSystem _Instance;
        [SerializeField] private AudioMusic[] _AudioMusic;
        [SerializeField] private AudioSoundEffect[] _AudioSoundEffect;

        private void Awake()
        {
            _Instance = this;
            for (int _i = 0; _i < _AudioMusic.Length; _i++)
            {
                _AudioMusic[_i]._AudioSource = gameObject.AddComponent<AudioSource>();
                _AudioMusic[_i]._AudioSource.enabled = false;
                _AudioMusic[_i]._AudioSource.clip = _AudioMusic[_i]._AudioClip;
                _AudioMusic[_i]._AudioSource.volume = _AudioMusic[_i]._Volume;
                _AudioMusic[_i]._AudioSource.pitch = 1.0f;
                _AudioMusic[_i]._AudioSource.playOnAwake = _AudioMusic[_i]._PlayOnAwake;
                _AudioMusic[_i]._AudioSource.loop = _AudioMusic[_i]._Loop;
                _AudioMusic[_i]._AudioSource.enabled = true;
            }
            for (int _i = 0; _i < _AudioSoundEffect.Length; _i++)
            {
                _AudioSoundEffect[_i]._AudioSource = gameObject.AddComponent<AudioSource>();
                _AudioSoundEffect[_i]._AudioSource.enabled = false;
                _AudioSoundEffect[_i]._AudioSource.clip = _AudioSoundEffect[_i]._AudioClip;
                _AudioSoundEffect[_i]._AudioSource.volume = _AudioSoundEffect[_i]._Volume;
                _AudioSoundEffect[_i]._AudioSource.pitch = 1.0f;
                _AudioSoundEffect[_i]._AudioSource.playOnAwake = _AudioSoundEffect[_i]._PlayOnAwake;
                _AudioSoundEffect[_i]._AudioSource.loop = _AudioSoundEffect[_i]._Loop;
                _AudioSoundEffect[_i]._AudioSource.enabled = true;
            }
        }

        public void PlayMusic(string _name)
        {
            var _var = Array.Find(_AudioMusic, _x => _x._Name == _name);
            if (_var == null)
            {
                Logger.LogWarningFormat("PlayMusic({0}) not find.", _name);
                return;
            }
            _var._AudioSource.Play();
        }
        public void PlaySoundEffect(string _name)
        {
            var _var = Array.Find(_AudioSoundEffect, _x => _x._Name == _name);
            if (_var == null)
            {
                Logger.LogWarningFormat("PlaySoundEffect({0}) not find.", _name);
                return;
            }
            _var._AudioSource.Play();
        }
        public void PlayOneShotMusic(string _name)
        {
            var _var = Array.Find(_AudioMusic, _x => _x._Name == _name);
            if (_var == null)
            {
                Logger.LogWarningFormat("PlayOneShotMusic({0}) not find.", _name);
                return;
            }
            _var._AudioSource.PlayOneShot(_var._AudioClip);
        }
        public void PlayOneShotSoundEffect(string _name)
        {
            var _var = Array.Find(_AudioSoundEffect, _x => _x._Name == _name);
            if (_var == null)
            {
                Logger.LogWarningFormat("PlayOneShotSoundEffect({0}) not find.", _name);
                return;
            }
            _var._AudioSource.PlayOneShot(_var._AudioClip);
        }
        public void PauseMusic(string _name)
        {
            var _var = Array.Find(_AudioMusic, _x => _x._Name == _name);
            if (_var == null)
            {
                Logger.LogWarningFormat("PauseMusic({0}) not find.", _name);
                return;
            }
            _var._AudioSource.Pause();
        }
        public void PauseSoundEffect(string _name)
        {
            var _var = Array.Find(_AudioSoundEffect, _x => _x._Name == _name);
            if (_var == null)
            {
                Logger.LogWarningFormat("PauseSoundEffect({0}) not find.", _name);
                return;
            }
            _var._AudioSource.Pause();
        }
        public void PauseAll()
        {
            for (int _i = 0; _i < _AudioMusic.Length; _i++)
            {
                if (_AudioMusic[_i]._AudioClip == null)
                {
                    Logger.LogWarningFormat("PauseAll(Audio name: {0} is null.)", _AudioMusic[_i]._Name);
                    continue;
                }
                _AudioMusic[_i]._AudioSource.Pause();
            }
            for (int _i = 0; _i < _AudioSoundEffect.Length; _i++)
            {
                if (_AudioSoundEffect[_i]._AudioClip == null)
                {
                    Logger.LogWarningFormat("PauseAll(Audio name: {0} is null.)", _AudioSoundEffect[_i]._Name);
                    continue;
                }
                _AudioSoundEffect[_i]._AudioSource.Pause();
            }
        }
        public void UnPauseMusic(string _name)
        {
            var _var = Array.Find(_AudioMusic, _x => _x._Name == _name);
            if (_var == null)
            {
                Logger.LogWarningFormat("UnPauseMusic({0}) not find.", _name);
                return;
            }
            _var._AudioSource.UnPause();
        }
        public void UnPauseSoundEffect(string _name)
        {
            var _var = Array.Find(_AudioSoundEffect, _x => _x._Name == _name);
            if (_var == null)
            {
                Logger.LogWarningFormat("UnPauseSoundEffect({0}) not find.", _name);
                return;
            }
            _var._AudioSource.UnPause();
        }
        public void UnPauseAll()
        {
            for (int _i = 0; _i < _AudioMusic.Length; _i++)
            {
                if (_AudioMusic[_i]._AudioClip == null)
                {
                    Logger.LogWarningFormat("UnPauseAll(Audio name: {0} is null.)", _AudioMusic[_i]._Name);
                    continue;
                }
                _AudioMusic[_i]._AudioSource.UnPause();
            }
            for (int _i = 0; _i < _AudioSoundEffect.Length; _i++)
            {
                if (_AudioSoundEffect[_i]._AudioClip == null)
                {
                    Logger.LogWarningFormat("UnPauseAll(Audio name: {0} is null.)", _AudioSoundEffect[_i]._Name);
                    continue;
                }
                _AudioSoundEffect[_i]._AudioSource.UnPause();
            }
        }
        public void StopMusic(string _name)
        {
            var _var = Array.Find(_AudioMusic, _x => _x._Name == _name);
            if (_var == null)
            {
                Logger.LogWarningFormat("StopMusic({0}) not find.", _name);
                return;
            }
            _var._AudioSource.Stop();
        }
        public void StopSoundEffect(string _name)
        {
            var _var = Array.Find(_AudioSoundEffect, _x => _x._Name == _name);
            if (_var == null)
            {
                Logger.LogWarningFormat("StopSoundEffect({0}) not find.", _name);
                return;
            }
            _var._AudioSource.Stop();
        }
        public void StopAll()
        {
            for (int _i = 0; _i < _AudioMusic.Length; _i++)
            {
                if (_AudioMusic[_i]._AudioClip == null)
                {
                    Logger.LogWarningFormat("StopAll(Audio name: {0} is null.)", _AudioMusic[_i]._Name);
                    continue;
                }
                _AudioMusic[_i]._AudioSource.Stop();
            }
            for (int _i = 0; _i < _AudioSoundEffect.Length; _i++)
            {
                if (_AudioSoundEffect[_i]._AudioClip == null)
                {
                    Logger.LogWarningFormat("StopAll(Audio name: {0} is null.)", _AudioSoundEffect[_i]._Name);
                    continue;
                }
                _AudioSoundEffect[_i]._AudioSource.Stop();
            }
        }
        public void VolumeChangeMusic(float _volume)
        {
            for (int _i = 0; _i < _AudioMusic.Length; _i++)
            {
                if (_AudioMusic[_i]._AudioClip == null)
                {
                    Logger.LogWarningFormat("VolumeChange(Audio name: {0} is null.)", _AudioMusic[_i]._Name);
                    continue;
                }
                _AudioMusic[_i]._AudioSource.volume = _volume;
            }
        }
        public void VolumeChangeSoundEffect(float _volume)
        {
            for (int _i = 0; _i < _AudioSoundEffect.Length; _i++)
            {
                if (_AudioSoundEffect[_i]._AudioClip == null)
                {
                    Logger.LogWarningFormat("VolumeChange(Audio name: {0} is null.)", _AudioSoundEffect[_i]._Name);
                    continue;
                }
                _AudioSoundEffect[_i]._AudioSource.volume = _volume;
            }
        }
        public void FadeMusic(string _name, float _target_volume, float _duration)
        {
            StartCoroutine(IEnumeratorFadeMusic(_name, _target_volume, _duration));
        }
        private IEnumerator IEnumeratorFadeMusic(string _name, float _target_volume, float _duration)
        {
            yield return OPT._WaitForEndOfFrame;
            var _var = Array.Find(_AudioMusic, _x => _x._Name == _name);
            if (_var == null)
            {
                Logger.LogWarningFormat("FadeMusic({0}) not find.", _name);
                yield break;
            }
            switch (Database._Settings_Music)
            {
                case 0:
                    if (_target_volume == 0.0f) _var._AudioSource.Stop();
                    yield break;
                case 1:
                    if (_target_volume == 1.0f) _var._AudioSource.volume = 0.0f;
                    float _current_time = 0;
                    float _start = _var._AudioSource.volume;
                    while (_current_time < _duration)
                    {
                        _current_time += TimeSystem._UnscaledDeltaTime();
                        _var._AudioSource.volume = Mathf.Lerp(_start, _target_volume, _current_time / _duration);
                        yield return null;
                    }
                    if (_target_volume == 0.0f) { _var._AudioSource.Stop(); _var._AudioSource.volume = 1.0f; }
                    yield break;
            }
        }
        public void FadeSoundEffect(string _name, float _target_volume, float _duration)
        {
            StartCoroutine(IEnumeratorFadeSoundEffect(_name, _target_volume, _duration));
        }
        private IEnumerator IEnumeratorFadeSoundEffect(string _name, float _target_volume, float _duration)
        {
            yield return OPT._WaitForEndOfFrame;
            var _var = Array.Find(_AudioSoundEffect, _x => _x._Name == _name);
            if (_var == null)
            {
                Logger.LogWarningFormat("FadeSoundEffect({0}) not find.", _name);
                yield break;
            }
            switch (Database._Settings_SoundEffect)
            {
                case 0:
                    if (_target_volume == 0.0f) _var._AudioSource.Stop();
                    yield break;
                case 1:
                    if (_target_volume == 1.0f) _var._AudioSource.volume = 0.0f;
                    float _current_time = 0;
                    float _start = _var._AudioSource.volume;
                    while (_current_time < _duration)
                    {
                        _current_time += TimeSystem._UnscaledDeltaTime();
                        _var._AudioSource.volume = Mathf.Lerp(_start, _target_volume, _current_time / _duration);
                        yield return null;
                    }
                    if (_target_volume == 0.0f) { _var._AudioSource.Stop(); _var._AudioSource.volume = 1.0f; }
                    yield break;
            }
        }
    }

    [Serializable]
    public class AudioMusic
    {
        public string _Name;
        public AudioClip _AudioClip;
        [Range(0.0f, 1.0f)] public float _Volume;
        public bool _PlayOnAwake;
        public bool _Loop;
        [HideInInspector] public AudioSource _AudioSource;
    }

    [Serializable]
    public class AudioSoundEffect
    {
        public string _Name;
        public AudioClip _AudioClip;
        [Range(0.0f, 1.0f)] public float _Volume;
        public bool _PlayOnAwake;
        public bool _Loop;
        [HideInInspector] public AudioSource _AudioSource;
    }
}
