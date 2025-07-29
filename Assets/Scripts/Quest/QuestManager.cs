using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
    public Dictionary<string, QuestData> activeQuests = new Dictionary<string, QuestData>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 어떤 오브젝트가 퀘스트를 추가했는지 알 수 있도록 GameObject questGiver를 받습니다.
    // GameObject questGiver를 마지막 인수로 추가합니다.
    public void AddQuest(string questID, string title, string objective, GameObject questGiver)
    {
        // 어떤 오브젝트가 퀘스트 추가를 요청했는지 콘솔에 메시지를 출력합니다.
        Debug.Log(questGiver.name + " 오브젝트가 '" + title + "' 퀘스트 추가를 요청했습니다.");

        if (activeQuests.ContainsKey(questID))
        {
            Debug.LogWarning("하지만 이미 존재하는 퀘스트라서 추가하지 않았습니다.");
            return;
        }

        QuestData newQuest = new QuestData(title, objective);
        activeQuests.Add(questID, newQuest);
        UpdateMissionUI();
    }

    public void CompleteQuest(string questID)
    {
        if (activeQuests.ContainsKey(questID) && !activeQuests[questID].isCompleted)
        {
            activeQuests[questID].isCompleted = true;
            UpdateMissionUI();
            CheckAllQuestsCompleted();
        }
    }

    private void CheckAllQuestsCompleted()
    {
        if (activeQuests.Count > 0 && activeQuests.Values.All(quest => quest.isCompleted))
        {
            Debug.Log("모든 퀘스트 완료! 퇴근합니다.");
            StartCoroutine(GoHomeSequence());
        }
    }

    private void UpdateMissionUI()
    {
        if (MissionUI.Instance != null)
        {
            MissionUI.Instance.UpdateMissionList(activeQuests);
        }
    }

    IEnumerator GoHomeSequence()
    {
        yield return new WaitForSeconds(2f);
    }
}