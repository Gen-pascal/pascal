using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/// <summary>
/// �÷��̾ ���� ���� ���� ��ȣ�ۿ��ϸ� ������ ��ȭ�� ���۽�Ű�� ��ũ��Ʈ�Դϴ�.
/// ��ȭ�� ������ Ư�� �̺�Ʈ�� ȣ���� �� �ֽ��ϴ�.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))] // �� ������Ʈ�� BoxCollider2D�� �ʼ����� ����մϴ�.
public class DialogueTrigger : MonoBehaviour
{
    [Header("��ȭ ����")]
    [Tooltip("�� Ʈ���Ű� ������ ��ȭ �������Դϴ�.")]
    public Dialogue dialogue;

    [Header("��ȭ �Ϸ� �� �̺�Ʈ")]
    [Tooltip("��ȭ�� ���������� �Ϸ�Ǿ��� �� ȣ��� ����Ƽ �̺�Ʈ�Դϴ�. �ν����� â���� ������ �� �ֽ��ϴ�.")]
    public UnityEvent onDialogueComplete;

    // �� Ʈ���Ű� 'ȭ��� ����' ����Ʈ�� �ִ� Ʈ�������� üũ�ϴ� �ɼ��Դϴ�.
    [SerializeField] private bool givesBathroomQuest = false;

    private bool isPlayerInRange = false;
    private bool canInteract = false;

    private void Awake()
    {
        // �ݶ��̴��� Ʈ���� ������� Ȯ���ϰ� �ƴ϶�� �ڵ����� �����մϴ�.
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void Start()
    {
        // ���� ���۵��ڸ��� �ٷ� ��ȣ�ۿ��ϴ� ���� �����ϱ� ���� ª�� ������ �ݴϴ�.
        StartCoroutine(InteractionCooldown());
    }

    private IEnumerator InteractionCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        canInteract = true;
    }

    private void Update()
    {
        // �÷��̾ ���� ���� �ְ�, ��ȣ�ۿ��� �����ϸ�, �ٸ� ��ȭ�� ���� ���� �ƴ� �� 'F' Ű�� ������ ��ȭ�� �����մϴ�.
        if (isPlayerInRange && canInteract && !DialogueManager.isTalking && Input.GetKeyDown(KeyCode.F))
        {
            DialogueManager.Instance.StartDialogue(dialogue, OnComplete);
        }
    }

    /// <summary>
    /// DialogueManager�� ���� ��ȭ�� ��� ������ �� ȣ��Ǵ� �ݹ� �Լ��Դϴ�.
    /// </summary>
    private void OnComplete()
    {
        // �� Ʈ���Ű� 'ȭ��� ����' ����Ʈ�� �ֵ��� �����Ǿ��ٸ�, ����Ʈ�� �߰��մϴ�.
        if (givesBathroomQuest)
        {
            GiveBathroomQuest();
        }

        // �ν����Ϳ��� ������ UnityEvent�� ȣ���մϴ�.
        onDialogueComplete?.Invoke();
    }

    /// <summary>
    /// 'ȭ��� ����' ����Ʈ�� QuestManager�� �߰��մϴ�.
    /// </summary>
    private void GiveBathroomQuest()
    {
        // ����Ʈ ID, ����, ���� ��ǥ, ����Ʈ�� �� ������Ʈ ������ �����մϴ�.
        QuestManager.Instance.AddQuest("goToBathroom", "ȭ��� ����", "ȭ��� ���� ��ȣ�ۿ�", this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}