using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public string spawnID; // ¿¹: "FromForest", "FromTown"

    void Start()
    {
        if (GameManager.Instance != null && GameManager.Instance.lastEnteredDoorID == spawnID)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                player.transform.position = transform.position;
        }
    }
}