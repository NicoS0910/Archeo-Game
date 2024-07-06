using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public GameObject[] coins;
    public Transform[] coinTargetZones;
    public GameObject rewardCoin;
    public GameObject coinMinigameUI;
    public GameObject closeUIButton; // Das UI-Element für den Close-Button

    public GameObject[] pieces;
    public Transform[] pieceTargetZones;
    public GameObject rewardPuzzle;
    public GameObject puzzleMinigameUI;

    public GameObject specialObject;
    public GameObject anotherSpecialObject;

    private PlayerController playerController;
    private bool isGamePaused = false;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController not found in the scene!");
        }

        if (specialObject != null)
        {
            specialObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Special object not assigned!");
        }

        if (anotherSpecialObject != null)
        {
            anotherSpecialObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Another special object not assigned!");
        }

        if (closeUIButton != null)
        {
            closeUIButton.SetActive(false);
        }
        else
        {
            Debug.LogError("Close UI Button not assigned!");
        }
    }

    public void CheckCoins()
    {
        if (AllCoinsCorrectlyPlaced())
        {
            ActivateCloseButton(); // Den Close-Button aktivieren
            //UnpauseGame();
        }
    }

    private void ActivateCloseButton()
    {
        if (closeUIButton != null)
        {
            closeUIButton.SetActive(true);
            Debug.Log("Close button activated.");
        }
        else
        {
            Debug.LogWarning("Close button is not assigned.");
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
