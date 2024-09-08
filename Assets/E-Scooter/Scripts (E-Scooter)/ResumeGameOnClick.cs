using UnityEngine;
using UnityEngine.EventSystems;

public class ResumeGameOnClick : MonoBehaviour, IPointerClickHandler
{
    // Methode, die aufgerufen wird, wenn auf den Bereich des UI-Elements geklickt wird
    public void OnPointerClick(PointerEventData eventData)
    {
        // Überprüfen, ob die linke Maustaste gedrückt wurde
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ResumeGame();
        }
    }

    // Methode zum Fortsetzen des Spiels
    private void ResumeGame()
    {
        Time.timeScale = 1f; // Setzt die Spielgeschwindigkeit auf Normal
        Debug.Log("Game Resumed");
    }
}
