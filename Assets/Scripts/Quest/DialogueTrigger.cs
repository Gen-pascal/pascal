using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/// <summary>
/// 플레이어가 범위 내에 들어와 상호작용하면 지정된 대화를 시작시키는 스크립트입니다.
/// 대화가 끝나면 특정 이벤트를 호출할 수 있습니다.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))] // 이 컴포넌트는 BoxCollider2D가 필수임을 명시합니다.
public class DialogueTrigger : MonoBehaviour
{
    [Header("대화 내용")]
    [Tooltip("이 트리거가 실행할 대화 데이터입니다.")]
    public Dialogue dialogue;

    [Header("대화 완료 후 이벤트")]
    [Tooltip("대화가 성공적으로 완료되었을 때 호출될 유니티 이벤트입니다. 인스펙터 창에서 설정할 수 있습니다.")]
    public UnityEvent onDialogueComplete;

    // 이 트리거가 '화장실 가기' 퀘스트를 주는 트리거인지 체크하는 옵션입니다.
    [SerializeField] private bool givesBathroomQuest = false;

    private bool isPlayerInRange = false;
    private bool canInteract = false;

    private void Awake()
    {
        // 콜라이더가 트리거 모드인지 확인하고 아니라면 자동으로 설정합니다.
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void Start()
    {
        // 씬이 시작되자마자 바로 상호작용하는 것을 방지하기 위해 짧은 지연을 줍니다.
        StartCoroutine(InteractionCooldown());
    }

    private IEnumerator InteractionCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        canInteract = true;
    }

    private void Update()
    {
        // 플레이어가 범위 내에 있고, 상호작용이 가능하며, 다른 대화가 진행 중이 아닐 때 'F' 키를 누르면 대화를 시작합니다.
        if (isPlayerInRange && canInteract && !DialogueManager.isTalking && Input.GetKeyDown(KeyCode.F))
        {
            DialogueManager.Instance.StartDialogue(dialogue, OnComplete);
        }
    }

    /// <summary>
    /// DialogueManager에 의해 대화가 모두 끝났을 때 호출되는 콜백 함수입니다.
    /// </summary>
    private void OnComplete()
    {
        // 이 트리거가 '화장실 가기' 퀘스트를 주도록 설정되었다면, 퀘스트를 추가합니다.
        if (givesBathroomQuest)
        {
            GiveBathroomQuest();
        }

        // 인스펙터에서 설정한 UnityEvent를 호출합니다.
        onDialogueComplete?.Invoke();
    }

    /// <summary>
    /// '화장실 가기' 퀘스트를 QuestManager에 추가합니다.
    /// </summary>
    private void GiveBathroomQuest()
    {
        // 퀘스트 ID, 제목, 세부 목표, 퀘스트를 준 오브젝트 정보를 전달합니다.
        QuestManager.Instance.AddQuest("goToBathroom", "화장실 가기", "화장실 문과 상호작용", this.gameObject);
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