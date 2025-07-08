// DialogueTrigger.cs
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public Dialogue dialogue;
    public UnityEvent onDialogueComplete;

    private bool playerInRange = false;
    private bool hasStarted = false;  // 대화 시작 가드

    void Update()
    {
        // 플레이어 범위 인식 및 상호작용 키
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && !hasStarted)
        {
            hasStarted = true;
            DialogueManager.Instance.StartDialogue(dialogue, OnComplete);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($" TriggerEnter2D with: {other.name}");
        if (other.CompareTag("Player"))
        {
            Debug.Log("    Player IN");
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($" TriggerExit2D with: {other.name}");
        if (other.CompareTag("Player"))
        {
            Debug.Log("    Player OUT");
            playerInRange = false;
        }
    }

    void OnComplete()
    {
        // 대화가 끝난 뒤 콜백 호출
        onDialogueComplete?.Invoke();
    }
}