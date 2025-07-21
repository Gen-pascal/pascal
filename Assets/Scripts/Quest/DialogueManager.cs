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
    public static DialogueManager Instance;
    public static bool isTalking = false;
    public static bool justEndedDialogue = false;  // ← 추가

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
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue, UnityAction onEnd = null)
    {
        isTalking = true;
        onDialogueEnd = onEnd;
        dialoguePanel.SetActive(true);
        sentencesQueue.Clear();
        foreach (string s in dialogue.sentences) sentencesQueue.Enqueue(s);
        DisplayNextSentence();
    }

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

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);

        isTalking = false;
        justEndedDialogue = true;              // ← 플래그 세트
        StartCoroutine(ResetJustEndedFlag());  // ← 다음 프레임에 클리어

        onDialogueEnd?.Invoke();
    }

    IEnumerator ResetJustEndedFlag()
    {
        yield return null;
        justEndedDialogue = false;             // ← 다음 프레임에 리셋
    }

    void Update()
    {
        if (dialoguePanel.activeSelf &&
            (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)))
        {
            DisplayNextSentence();
        }
    }
}
