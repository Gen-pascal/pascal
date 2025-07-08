using UnityEngine;
using UnityEngine.UI;

public class DoorPortal : MonoBehaviour
{
    public string targetSceneName;
    public GameObject interactionUI; // 문 위에 표시될 UI 텍스트
    public string spawnPointID;      // player 스폰 위치 ID

    private bool isPlayerNear = false;

    void Start()
    {
        if (interactionUI != null)
            interactionUI.SetActive(false); // 처음엔 안 보이게
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            if (!string.IsNullOrEmpty(targetSceneName))
            {
                GameManager.Instance.lastEnteredDoorID = spawnPointID;
                SceneTransitionManager.Instance.RequestSceneChange(targetSceneName);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (interactionUI != null)
                interactionUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (interactionUI != null)
                interactionUI.SetActive(false);
        }
    }
}
