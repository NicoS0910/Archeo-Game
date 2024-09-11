using UnityEngine;

public class FinishPopUp : MonoBehaviour
{
    [SerializeField] private GameObject _objectToActivate;
    public Resource scorePoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_objectToActivate != null)
            {
                _objectToActivate.SetActive(true);
            }

            if (Inventory.instance != null)
            {
                Inventory.instance.AddResources(scorePoints, 100);
            }

            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
