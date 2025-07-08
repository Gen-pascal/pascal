using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
    private List<string> quests = new List<string>();

    void Awake()
    {
        // 싱글톤 설정
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        // (선택) 씬 전환 시 파괴되지 않게 하고 싶으면:
        // DontDestroyOnLoad(gameObject);
    }

    // 외부에서 퀘스트를 추가할 때 부르는 함수
    public void AddQuest(string quest)
    {
        if (quests.Contains(quest))
            return; // 중복 방지

        quests.Add(quest);
        // UI 갱신
        MissionUI.Instance.UpdateMissionList(quests);
    }

    // (선택) 나중에 퀘스트 완료, 보상 등 메서드 추가 가능
}
