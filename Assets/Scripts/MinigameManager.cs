using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public GameObject[] coins;            
    public Transform[] coinTargetZones;   
    public GameObject rewardCoin;         
    public GameObject coinMinigameUI;     

    public GameObject[] pieces;            // Dies muss im Inspector zugewiesen werden
    public Transform[] pieceTargetZones;   // Dies muss im Inspector zugewiesen werden
    public GameObject rewardPuzzle;       
    public GameObject puzzleMinigameUI;   

    public GameObject specialObject;       // Dies muss im Inspector zugewiesen werden
    public GameObject anotherSpecialObject;// Dies muss im Inspector zugewiesen werden

    private PlayerController playerController; // Referenz auf den PlayerController
    private bool isGamePaused = false; // Variable zum Speichern des Pausenstatus

    void Start()
    {
        // PlayerController Komponente finden
        playerController = FindObjectOfType<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController not found in the scene!");
        }

        // Sicherstellen, dass das spezielle Objekt deaktiviert ist
        if (specialObject != null)
        {
            specialObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Special object not assigned!");
        }

        // Sicherstellen, dass das andere spezielle Objekt deaktiviert ist
        if (anotherSpecialObject != null)
        {
            anotherSpecialObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Another special object not assigned!");
        }
    }

    public void CheckCoins()
    {
        if (AllCoinsCorrectlyPlaced())
        {
            EndCoinMinigame();
            UnpauseGame();
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
        Debug.Log("Checking if all pieces are correctly placed...");
        if (AllPiecesCorrectlyPlaced())
        {
            Debug.Log("All puzzle pieces correctly placed.");
            //EndPuzzleMinigame();
            //UnpauseGame();

            if (specialObject != null)
            {
                specialObject.SetActive(true);
                Debug.Log("Special object activated.");
            }
            else
            {
                Debug.LogWarning("Special object is not assigned.");
            }

            if (anotherSpecialObject != null)
            {
                anotherSpecialObject.SetActive(true);
                Debug.Log("Another special object activated.");
            }
            else
            {
                Debug.LogWarning("Another special object is not assigned.");
            }
        }
        else
        {
            Debug.Log("Not all puzzle pieces correctly placed.");
        }
    }

    public void UnpauseGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        Debug.Log("Game unpaused");

        if (playerController != null)
        {
            playerController.UnlockMovement();
        }
    }

    private bool AllPiecesCorrectlyPlaced()
    {
        Debug.Log($"Checking {pieces.Length} pieces against {pieceTargetZones.Length} target zones.");
        foreach (GameObject piece in pieces)
        {
            bool foundTargetZone = false;
            foreach (Transform zone in pieceTargetZones)
            {
                Debug.Log($"Checking piece {piece.name} against zone {zone.name}");
                if (piece.transform.parent == zone)
                {
                    Debug.Log($"Piece {piece.name} is in the correct zone {zone.name}");
                    foundTargetZone = true;
                    break;
                }
            }
            if (!foundTargetZone)
            {
                Debug.Log($"Piece {piece.name} is not in the correct zone");
                return false;
            }
        }
        return true;
    }

    private void EndCoinMinigame()
    {
        if (coinMinigameUI != null)
        {
            coinMinigameUI.SetActive(false);
        }

        if (rewardCoin != null)
        {
            rewardCoin.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Reward coin object is not assigned in the inspector.");
        }
    }

    private void EndPuzzleMinigame()
    {
        if (puzzleMinigameUI != null)
        {
            puzzleMinigameUI.SetActive(false);
        }

        if (rewardPuzzle != null)
        {
            rewardPuzzle.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Reward puzzle object is not assigned in the inspector.");
        }
    }
}
