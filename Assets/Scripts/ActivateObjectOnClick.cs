using UnityEngine;
using UnityEngine.EventSystems;

public class ActivateObjectOnClick : MonoBehaviour, IPointerClickHandler
{
    private bool isGamePaused = false; // Declare isGamePaused here

    public GameObject objectToActivate; // Referenz auf das zu aktivierende Objekt

    void Start()
    {
        // Überprüfe, ob das Objekt zum Aktivieren zugewiesen wurde
        if (objectToActivate == null)
        {
            objectToActivate = GameObject.Find("ObjectToDeactivate");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ActivateObject();
        }
    }

    private void ActivateObject()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true); // Aktiviere das Objekt
            //PauseGame();

        }
        else
        {
            Debug.LogWarning("Object to activate is null!");
        }
    }
    
    void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f; // Spielzeit auf Null setzen, um das Spiel zu pausieren
        Debug.Log("Game paused");
    }
}
