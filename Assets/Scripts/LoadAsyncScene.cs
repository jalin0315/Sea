using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadAsyncScene : MonoBehaviour
{
    [SerializeField] private Slider _Slider_Progress;
    private float _ProgressValue;
    private const string _NextSceneName = "SampleScene";
    private AsyncOperation _AsyncOperation;

    private void Start()
    {
        StartCoroutine(LoadScene(1.0f));
    }

    private IEnumerator LoadScene(float _time)
    {
        yield return new WaitForEndOfFrame();
        _Slider_Progress.value = _ProgressValue;
        yield return new WaitForSeconds(_time);
        _AsyncOperation = SceneManager.LoadSceneAsync(_NextSceneName, LoadSceneMode.Single);
        _AsyncOperation.allowSceneActivation = false;
        while (!_AsyncOperation.isDone)
        {
            yield return new WaitForEndOfFrame();
            _Slider_Progress.value = _ProgressValue;
            if (_AsyncOperation.progress < 0.9f) _ProgressValue = _AsyncOperation.progress;
            else
            {
                _ProgressValue = 1.0f;
                _AsyncOperation.allowSceneActivation = true;
                yield break;
            }
        }
    }
}
