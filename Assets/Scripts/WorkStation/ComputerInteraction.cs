// ComputerInteraction.cs
using UnityEngine;

public class ComputerInteraction : MonoBehaviour
{
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("컴퓨터 상호작용! '메일 확인하기' 퀘스트 완료를 시도합니다."); // 확인용 로그
            QuestManager.Instance.CompleteQuest("checkMail");
            this.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
    }
}