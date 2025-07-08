// DialogueManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

[System.Serializable]


public class Dialogue
{
    public string[] sentences;
}

public class DialogueManager : MonoBehaviour
{
    public static bool isTalking = false;
    public static DialogueManager Instance;

    [Header("UI Elements")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    [Header("Settings")]
    public float typeSpeed = 0.05f;

    private Queue<string> sentencesQueue;
    private UnityAction onDialogueEnd;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        sentencesQueue = new Queue<string>();
        // 시작 시 대화 패널을 비활성화
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
    }

    // 외부에서 대화 시작
    public void StartDialogue(Dialogue dialogue, UnityAction onEnd = null)
    {
        isTalking = true;
        onDialogueEnd = onEnd;
        dialoguePanel.SetActive(true);
        sentencesQueue.Clear();
        foreach (string sentence in dialogue.sentences)
            sentencesQueue.Enqueue(sentence);
        DisplayNextSentence();
    }

    // 다음 문장 표시
    public void DisplayNextSentence()
    {
        if (sentencesQueue.Count == 0)
        {
            EndDialogue();
            return;
        }
        StopAllCoroutines();
        string sentence = sentencesQueue.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = string.Empty;
        foreach (char c in sentence)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    // 대화 종료 시 패널 숨기고 콜백 실행
    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        isTalking = false;  // 대화 종료 시
        onDialogueEnd?.Invoke();
        dialoguePanel.SetActive(false);
        onDialogueEnd?.Invoke();
    }

    void Update()
    {
        // Enter 또는 Space 키로 다음 문장 전환
        if (dialoguePanel.activeSelf && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)))
            DisplayNextSentence();
    }
}