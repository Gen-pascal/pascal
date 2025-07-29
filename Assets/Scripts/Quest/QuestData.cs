// QuestData.cs
[System.Serializable]
public class QuestData
{
    public string title;
    public string objective;
    public bool isCompleted;

    public QuestData(string title, string objective)
    {
        this.title = title;
        this.objective = objective;
        this.isCompleted = false;
    }
}