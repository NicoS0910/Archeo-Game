using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateTeleportOnClick : MonoBehaviour
{
    public int sceneBuildIndex; // Der Build-Index der neuen Szene, die geladen werden soll

    // Methode zum Laden der neuen Szene
    public void OnButtonClicked()
    {
        // Lade die neue Szene basierend auf dem Build-Index
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
}
