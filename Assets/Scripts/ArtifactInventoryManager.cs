using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ArtifactInventoryManager : MonoBehaviour
{
    public GameObject inventoryUI;  // Referenz auf das Inventar UI Panel

    void Update()
    {
        // Öffne/Schließe das Inventar, wenn 'i' gedrückt wird
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
}