using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    public float fadeDuration = 1f;
    public Image fadeImage; // 페이드용 UI 이미지

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 이벤트 등록
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 해제
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬 전환 후 페이드 인 시작
        StartCoroutine(Fade(1f, 0f));
    }

    public void RequestSceneChange(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        // 페이드 아웃 (0->1)
        yield return StartCoroutine(Fade(0f, 1f));

        // 씬 로드
        SceneManager.LoadScene(sceneName);

        // 페이드 인은 씬이 완전히 로드된 후 이벤트(OnSceneLoaded)에서 실행하므로 여기서는 안 함
    }

    private IEnumerator Fade(float from, float to)
    {
        if (fadeImage == null)
        {
            
            yield break;
        }

        float timer = 0f;
        Color c = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, timer / fadeDuration);
            c.a = alpha;
            fadeImage.color = c;
            yield return null;
        }

        // 페이드 완료 후 알파 값 확실히 세팅
        c.a = to;
        fadeImage.color = c;
    }
}
