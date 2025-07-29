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
            // AddQuest�� ȣ���� ��, �������� this.gameObject�� �߰��ؼ� �ڱ� �ڽ��� �˷��ݴϴ�.
            QuestManager.Instance.AddQuest("checkMail", "���� Ȯ���ϱ�", "��ǻ�Ϳ� ��ȣ�ۿ�", this.gameObject);
            QuestManager.Instance.AddQuest("attendMeeting", "ȸ�� �����ϱ�", "ȸ�ǽ� ������ �̵�", this.gameObject);

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