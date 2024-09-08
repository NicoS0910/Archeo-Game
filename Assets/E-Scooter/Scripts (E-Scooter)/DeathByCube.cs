using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Überprüfen, ob das Objekt mit dem Player kollidiert
        if (other.CompareTag(playerTag))
        {
            // Rufe die GameOver-Methode vom GameManager auf
            if (GameManager.instance != null)
            {
                GameManager.instance.GameOver();
            }
            else
            {
                Debug.LogError("GameManager instance is missing!");
            }
        }
    }
}
