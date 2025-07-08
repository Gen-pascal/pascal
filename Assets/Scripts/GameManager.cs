using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string lastEnteredDoorID = ""; // 어느 문을 통해 들어왔는지

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // 중복 제거
        }
    }
}
