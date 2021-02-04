using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadAsyncScene : MonoBehaviour
{
    [SerializeField] private Image _Image_Progress;
    private float _ProgressValue;
    private const string _NextSceneName = "SampleScene";
    private AsyncOperation _AsyncOperation;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        _Image_Progress.fillAmount = _ProgressValue;
        yield return new WaitForSeconds(1.0f);
        _AsyncOperation = SceneManager.LoadSceneAsync(_NextSceneName, LoadSceneMode.Single);
        _AsyncOperation.allowSceneActivation = false;
        while (!_AsyncOperation.isDone)
        {
            yield return new WaitForEndOfFrame();
            if (_AsyncOperation.progress < 0.9f)
            {
                _ProgressValue = _AsyncOperation.progress;
                _Image_Progress.fillAmount = _ProgressValue;
            }
            else
            {
                _ProgressValue = 1.0f;
                _Image_Progress.fillAmount = _ProgressValue;
                _AsyncOperation.allowSceneActivation = true;
                yield break;
            }
        }
    }
}
