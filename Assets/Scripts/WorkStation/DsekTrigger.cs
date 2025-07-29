// DeskTrigger.cs
using UnityEngine;

public class DeskTrigger : MonoBehaviour
{
    private bool playerInRange = false;
    private bool questsGiven = false;

    void Update()
    {
        if (playerInRange && !questsGiven && Input.GetKeyDown(KeyCode.F))
        {
            // AddQuest를 호출할 때, 마지막에 this.gameObject를 추가해서 자기 자신을 알려줍니다.
            QuestManager.Instance.AddQuest("checkMail", "메일 확인하기", "컴퓨터와 상호작용", this.gameObject);
            QuestManager.Instance.AddQuest("attendMeeting", "회의 참석하기", "회의실 문으로 이동", this.gameObject);

            questsGiven = true;
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