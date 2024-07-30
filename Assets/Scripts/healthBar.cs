using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Start()
    {
        // Initialisiere die Herzen zu Beginn
        UpdateHearts();
    }

    void Update()
    {
        // Aktualisiere die Herzen jedes Frame
        UpdateHearts();
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth.currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}