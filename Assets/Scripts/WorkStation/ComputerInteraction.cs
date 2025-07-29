// ComputerInteraction.cs
using UnityEngine;

public class ComputerInteraction : MonoBehaviour
{
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("��ǻ�� ��ȣ�ۿ�! '���� Ȯ���ϱ�' ����Ʈ �ϷḦ �õ��մϴ�."); // Ȯ�ο� �α�
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