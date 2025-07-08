using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionUI : MonoBehaviour
{
    public static MissionUI Instance { get; private set; }

    [Header("UI Elements")]
    public GameObject missionPanel;  // Panel GameObject
    public TMP_Text missionText;     // 텍스트 컴포넌트

    void Awake()
    {
        // 싱글톤 설정
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        // Tab 키 누를 때마다 켜고 끄기
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            missionPanel.SetActive(!missionPanel.activeSelf);
        }
    }

    // QuestManager에서 호출
    public void UpdateMissionList(List<string> quests)
    {
        // 빌드업: 모든 퀘스트 앞에 • 붙이고 줄바꿈
        missionText.text = "";
        foreach (string q in quests)
        {
            missionText.text += "• " + q + "\n";
        }
    }
}
