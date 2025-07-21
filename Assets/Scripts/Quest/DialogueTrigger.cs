using UnityEngine;
using UnityEngine.Events;
using System.Collections; // Coroutine을 사용하기 위해 추가!

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public Dialogue dialogue;
    public UnityEvent onDialogueComplete;

    private bool playerInRange = false;
    private bool isReady = false; // <<-- 1. 트리거 준비 상태 변수 추가

    // Start 함수 추가
    void Start()
    {
        // 게임이 시작되면 바로 트리거가 활성화되지 않도록
        // 짧은 지연을 주는 코루틴을 실행합니다.
        StartCoroutine(InitializeTrigger());
    }

    // 코루틴 함수 추가
    IEnumerator InitializeTrigger()
    {
        // 첫 프레임 렌더링이 끝날 때까지 기다립니다.
        // 이렇게 하면 씬 로딩 시 발생하는 물리 충돌을 무시할 수 있습니다.
        yield return new WaitForEndOfFrame();
        isReady = true; // <<-- 2. 이제 트리거가 작동할 준비가 됨
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
        // isReady가 false이면(아직 준비 안됐으면) 아무것도 안 함
        if (!isReady) return; // <<-- 3. 준비가 됐을 때만 아래 코드가 실행되도록 함

        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log($"[{gameObject.name}] 플레이어 진입 → playerInRange = true");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 퇴장 로직은 isReady와 상관없이 작동해도 괜찮습니다.
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log($"[{gameObject.name}] 플레이어 이탈 → playerInRange = false");
        }
    }

    void OnComplete()
    {
        onDialogueComplete?.Invoke();
    }

    // OnTriggerStay2D는 현재 사용되지 않으므로 삭제하거나 주석 처리해도 좋습니다.
    // void OnTriggerStay2D(Collider2D other)
    // {
    //     ...
    // }
}