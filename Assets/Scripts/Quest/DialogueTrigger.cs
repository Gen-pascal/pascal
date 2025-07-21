using UnityEngine;
using UnityEngine.Events;
using System.Collections; // Coroutine�� ����ϱ� ���� �߰�!

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public Dialogue dialogue;
    public UnityEvent onDialogueComplete;

    private bool playerInRange = false;
    private bool isReady = false; // <<-- 1. Ʈ���� �غ� ���� ���� �߰�

    // Start �Լ� �߰�
    void Start()
    {
        // ������ ���۵Ǹ� �ٷ� Ʈ���Ű� Ȱ��ȭ���� �ʵ���
        // ª�� ������ �ִ� �ڷ�ƾ�� �����մϴ�.
        StartCoroutine(InitializeTrigger());
    }

    // �ڷ�ƾ �Լ� �߰�
    IEnumerator InitializeTrigger()
    {
        // ù ������ �������� ���� ������ ��ٸ��ϴ�.
        // �̷��� �ϸ� �� �ε� �� �߻��ϴ� ���� �浹�� ������ �� �ֽ��ϴ�.
        yield return new WaitForEndOfFrame();
        isReady = true; // <<-- 2. ���� Ʈ���Ű� �۵��� �غ� ��
    }

    void Update()
    {
        if (playerInRange
            && !DialogueManager.isTalking
            && Input.GetKeyDown(KeyCode.F))
        {
            DialogueManager.Instance.StartDialogue(dialogue, OnComplete);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // isReady�� false�̸�(���� �غ� �ȵ�����) �ƹ��͵� �� ��
        if (!isReady) return; // <<-- 3. �غ� ���� ���� �Ʒ� �ڵ尡 ����ǵ��� ��

        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log($"[{gameObject.name}] �÷��̾� ���� �� playerInRange = true");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // ���� ������ isReady�� ������� �۵��ص� �������ϴ�.
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log($"[{gameObject.name}] �÷��̾� ��Ż �� playerInRange = false");
        }
    }

    void OnComplete()
    {
        onDialogueComplete?.Invoke();
    }

    // OnTriggerStay2D�� ���� ������ �����Ƿ� �����ϰų� �ּ� ó���ص� �����ϴ�.
    // void OnTriggerStay2D(Collider2D other)
    // {
    //     ...
    // }
}