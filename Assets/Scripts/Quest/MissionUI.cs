using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionUI : MonoBehaviour
{
    public static MissionUI Instance { get; private set; }

    [Header("UI Elements")]
    public Transform questListContainer;
    public GameObject questItemPrefab;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateMissionList(Dictionary<string, QuestData> quests)
    {
        foreach (Transform child in questListContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var questPair in quests)
        {
            QuestData questData = questPair.Value;
            GameObject questItemGO = Instantiate(questItemPrefab, questListContainer);

            TMP_Text titleText = questItemGO.transform.Find("TextContainer/TitleText").GetComponent<TMP_Text>();
            TMP_Text objectiveText = questItemGO.transform.Find("TextContainer/ObjectiveText").GetComponent<TMP_Text>();

            if (titleText != null && objectiveText != null)
            {
                titleText.text = questData.title;
                objectiveText.text = "• " + questData.objective;

                // 제목의 폰트 스타일은 항상 Normal로 유지합니다.
                titleText.fontStyle = FontStyles.Normal;

                // 퀘스트 완료 여부에 따라 '세부 목표'의 폰트 스타일만 변경합니다.
                if (questData.isCompleted)
                {
                    objectiveText.fontStyle = FontStyles.Strikethrough;
                }
                else
                {
                    objectiveText.fontStyle = FontStyles.Normal;
                }
            }
        }
    }
}