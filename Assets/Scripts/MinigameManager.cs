using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    public GameObject[] coins;        // Die Münzen-GameObjects
    public Transform[] targetZones;   // Die Zielzonen-Transforms
    public GameObject rewardPrefab;   // Das Belohnungs-Prefab
    public Vector3 rewardSpawnPosition; // Die Position, an der das Belohnungsobjekt gespawnt werden soll
    public GameObject minigameUI; // Referenz auf die Minigame UI

    public void CheckCoins()
    {
        if (AllCoinsCorrectlyPlaced())
        {
            EndMinigame();
        }
    }

    private bool AllCoinsCorrectlyPlaced()
    {
        foreach (GameObject coin in coins)
        {
            bool correctPlacement = false;
            foreach (Transform zone in targetZones)
            {
                if (coin.transform.parent == zone && coin.CompareTag(zone.GetComponent<DropZone>().correctCoinTag))
                {
                    correctPlacement = true;
                    break;
                }
            }
            if (!correctPlacement)
            {
                return false;
            }
        }
        return true;
    }

    private void EndMinigame()
    {
        // Deaktiviere das Minigame UI
        if (minigameUI != null)
        {
            minigameUI.SetActive(false);
        }

        // Aktiviere das rewardPrefab GameObject
        if (rewardPrefab != null)
        {
            Instantiate(rewardPrefab, rewardSpawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("rewardPrefab is not assigned in the inspector.");
        }

        // Weitere Aktionen nach dem Beenden des Minigames...
    }
}
