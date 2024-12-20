using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public GameObject[] coins;
    public Transform[] coinTargetZones;
    public GameObject rewardCoin;
    public GameObject coinMinigameUI;
    public GameObject finishButton;

    public GameObject[] pieces;
    public Transform[] pieceTargetZones;
    public GameObject rewardPuzzle;
    public GameObject puzzleMinigameUI;

    public GameObject specialObject;
    public GameObject anotherSpecialObject;

    private PlayerController playerController;
    private bool isGamePaused = false;
    public Inventory inventory;
    public Resource scorePoints;
    private bool coinGameisFinished = false;
    private bool puzzleGameIsFinished = false;

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

        if (finishButton != null)
        {
            finishButton.SetActive(false);
        }
        else
        {
            Debug.LogError("Finish button not assigned!");
        }
    }

    public void CheckCoins()
    {
        if (AllCoinsCorrectlyPlaced())
        {
            ActivateFinishButton();
        }
    }

    private void ActivateFinishButton()
    {
        if (finishButton != null)
        {
            if (coinGameisFinished == false)
            {
                inventory.AddResources(scorePoints, 100);
                coinGameisFinished = true;
            }
            else
            {
                Debug.Log("Minigame already finished.");
            }
            finishButton.SetActive(true);
            Debug.Log("Finish button activated.");
        }
        else
        {
            Debug.LogWarning("Finish button is not assigned.");
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

            if (puzzleGameIsFinished == false)
            {
                inventory.AddResources(scorePoints, 100);
                puzzleGameIsFinished = true;
            }
            else
            {
                Debug.Log("Minigame already finished.");
            }

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
