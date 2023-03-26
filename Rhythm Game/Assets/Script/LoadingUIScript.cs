using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingUIScript : MonoBehaviour
{
    private static LoadingUIScript instance;
    public static LoadingUIScript Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<LoadingUIScript>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                { 
                    instance = Create();
                }
            }
            return instance;
        }
    }
    
    private static LoadingUIScript Create()
    {
        return Instantiate(Resources.Load<LoadingUIScript>("LoadingUI"));
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }




    [SerializeField]
    private CanvasGroup canvasGroup;

    private string loadSceneName;

    //씬 로딩 메서드
    public void LoadScene(string name)
    {
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += OnSceneLoaded;
        loadSceneName = name;
        StartCoroutine(LoadSceneProcess());
    }

    //콜백 제거
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name == loadSceneName) 
        {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    //비동기적 로딩
    private IEnumerator LoadSceneProcess()
    {
        yield return StartCoroutine(Fade(true));

        AsyncOperation operation = SceneManager.LoadSceneAsync(loadSceneName);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while (!operation.isDone)
        {
            yield return null;

            if (operation.progress >= 0.90f)
            {
                timer += Time.unscaledDeltaTime;
                float textnum = Mathf.Lerp(0.90f, 1f, timer);

                if (textnum >= 0.99f)
                {
                    operation.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    //페이드 인/아웃
    private IEnumerator Fade(bool IsFadein)
    {
        float timer = 0f;
        while (timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;

            //true = fadein false = fadeout
            canvasGroup.alpha = IsFadein ? Mathf.Lerp(0f, 1f, timer) : Mathf.Lerp(1f, 0f, timer);
        }

        if (!IsFadein)
        {
            gameObject.SetActive(false);
        }
    }


}
