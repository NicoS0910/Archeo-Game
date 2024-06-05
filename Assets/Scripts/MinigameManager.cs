using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public GameObject[] coins;            
    public Transform[] coinTargetZones;   
    public GameObject rewardCoin;         
    public GameObject coinMinigameUI;     

    public GameObject[] pieces;           
    public Transform[] pieceTargetZones;  
    public GameObject rewardPuzzle;       
    public GameObject puzzleMinigameUI;   

    public void CheckCoins()
    {
        if (AllCoinsCorrectlyPlaced())
        {
            EndCoinMinigame();
        }
    }

    private bool AllCoinsCorrectlyPlaced()
    {
        foreach (GameObject coin in coins)
        {
            bool correctPlacement = false;
            foreach (Transform zone in coinTargetZones)
            {
                if (coin.transform.parent == zone && coin.CompareTag(zone.GetComponent<DropZone>().correctTag))
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

    public void CheckPieces()
    {
        if (AllPiecesCorrectlyPlaced())
        {
            Debug.Log("All puzzle pieces correctly placed.");
            EndPuzzleMinigame();
        }
        else
        {
            Debug.Log("Not all puzzle pieces correctly placed.");
        }
    }

    private bool AllPiecesCorrectlyPlaced()
    {
        foreach (GameObject piece in pieces)
        {
            bool foundTargetZone = false;
            foreach (Transform zone in pieceTargetZones)
            {
                if (piece.transform.parent == zone)
                {
                    foundTargetZone = true;
                    break;
                }
            }
            if (!foundTargetZone)
            {
                return false;
            }
        }
        return true;
    }

    private void EndCoinMinigame()
    {
        // Deaktiviere das Minigame UI für die Münzen
        if (coinMinigameUI != null)
        {
            coinMinigameUI.SetActive(false);
        }

        // Aktiviere das Belohnungs-GameObject für die Münzen
        if (rewardCoin != null)
        {
            rewardCoin.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Reward coin object is not assigned in the inspector.");
        }

        // Schließe das Coin-Minispiel
        //gameObject.SetActive(false);
    }

    private void EndPuzzleMinigame()
    {
        // Deaktiviere das Minigame UI für das Puzzle
        if (puzzleMinigameUI != null)
        {
            puzzleMinigameUI.SetActive(false);
        }

        // Aktiviere das Belohnungs-GameObject für das Puzzle
        if (rewardPuzzle != null)
        {
            rewardPuzzle.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Reward puzzle object is not assigned in the inspector.");
        }

        // Schließe das Puzzle-Minispiel
        //gameObject.SetActive(false);
    }
}
