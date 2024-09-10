using UnityEngine;
using TMPro;
using System.Collections;

public class TypingEffect : MonoBehaviour
{
    public float typingSpeed = 0.01f; // Geschwindigkeit der Anzeige

    public IEnumerator TypeText(string textToType)
    {
        TextMeshProUGUI textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.text = textToType; // Setzt den gesamten Text auf einmal
        int totalLength = textToType.Length;
        int chunkSize = Mathf.Max(1, totalLength / 10); // Zeige 10% des Textes auf einmal oder mindestens 1 Zeichen

        textMeshPro.maxVisibleCharacters = 0;

        for (int i = 0; i <= totalLength; i += chunkSize)
        {
            textMeshPro.maxVisibleCharacters = Mathf.Min(i + chunkSize, totalLength);
            yield return new WaitForSeconds(typingSpeed * chunkSize); // Geschwindigkeit anpassen, basierend auf der Blockgröße
        }
    }
}
