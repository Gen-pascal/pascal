using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위해 꼭 추가해야 합니다!

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // 어느 문(스폰 지점)을 통해 들어왔는지 ID를 저장하는 변수
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

    // --- [ 추가된 부분 시작 ] ---

    // 이 오브젝트가 활성화될 때마다 SceneManager의 sceneLoaded 이벤트에 OnSceneLoaded 함수를 등록
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 오브젝트가 비활성화될 때 등록했던 함수를 해제 (메모리 누수 방지)
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬 로딩이 완료될 때마다 자동으로 호출되는 함수
    // GameManager.cs
    // GameManager.cs
    // GameManager.cs
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (string.IsNullOrEmpty(lastEnteredDoorID))
        {
            return;
        }

        // --- [ 이 부분을 수정합니다 ] ---
        // 더 빠르고 새로운 방식으로 SpawnPoint를 찾습니다. (정렬 안 함)
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