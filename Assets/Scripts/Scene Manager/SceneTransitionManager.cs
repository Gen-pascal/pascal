using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    public float fadeDuration = 1f;
    public Image fadeImage; // ���̵�� UI �̹���

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // �� �ε� �̺�Ʈ ���
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
            SceneManager.sceneLoaded -= OnSceneLoaded; // �̺�Ʈ ����
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �� ��ȯ �� ���̵� �� ����
        StartCoroutine(Fade(1f, 0f));
    }

    public void RequestSceneChange(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        // ���̵� �ƿ� (0->1)
        yield return StartCoroutine(Fade(0f, 1f));

        // �� �ε�
        SceneManager.LoadScene(sceneName);

        // ���̵� ���� ���� ������ �ε�� �� �̺�Ʈ(OnSceneLoaded)���� �����ϹǷ� ���⼭�� �� ��
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

        // ���̵� �Ϸ� �� ���� �� Ȯ���� ����
        c.a = to;
        fadeImage.color = c;
    }
}
