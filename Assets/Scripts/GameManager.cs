using UnityEngine;
using UnityEngine.SceneManagement; // �� ������ ���� �� �߰��ؾ� �մϴ�!

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // ��� ��(���� ����)�� ���� ���Դ��� ID�� �����ϴ� ����
    public string lastEnteredDoorID = "";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // --- [ �߰��� �κ� ���� ] ---

    // �� ������Ʈ�� Ȱ��ȭ�� ������ SceneManager�� sceneLoaded �̺�Ʈ�� OnSceneLoaded �Լ��� ���
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ������Ʈ�� ��Ȱ��ȭ�� �� ����ߴ� �Լ��� ���� (�޸� ���� ����)
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // �� �ε��� �Ϸ�� ������ �ڵ����� ȣ��Ǵ� �Լ�
    // GameManager.cs
    // GameManager.cs
    // GameManager.cs
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (string.IsNullOrEmpty(lastEnteredDoorID))
        {
            return;
        }

        // --- [ �� �κ��� �����մϴ� ] ---
        // �� ������ ���ο� ������� SpawnPoint�� ã���ϴ�. (���� �� ��)
        SpawnPoint[] spawnPoints = FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);
        // --------------------------------

        foreach (var point in spawnPoints)
        {
            if (point.spawnID == lastEnteredDoorID)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    player.transform.position = point.transform.position;
                }
                break;
            }
        }

        lastEnteredDoorID = "";
    }
}