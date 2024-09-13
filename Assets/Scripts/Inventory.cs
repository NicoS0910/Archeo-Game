using UnityEngine;
using UnityEngine.UI; // Für die Button-Komponente
using UnityEngine.SceneManagement; // Für den Szenenwechsel

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [field: SerializeField] private SerializableDictionary<Resource, int> Resources { get; set; }
    [SerializeField] private Button displayScoreButton; // Button, der die ScorePoints Resource ausgibt

    [Header("ScorePoints Resource")]
    [SerializeField] private Resource scorePointsResource; // Die Resource, die Sie ausgeben möchten

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Inventar bleibt über Szenen hinweg erhalten
        }
        else
        {
            Destroy(gameObject); // Verhindert doppelte Instanzen
        }
    }

    private void Start()
    {
        // Überprüfen, ob der Button und die Resource zugewiesen sind
        if (displayScoreButton != null && scorePointsResource != null)
        {
            displayScoreButton.onClick.AddListener(DisplayScorePoints);
        }
        else
        {
            Debug.LogWarning("Button oder ScorePoints Resource ist nicht zugewiesen.");
        }
    }

    // Finds and returns the count of a resource in the dictionary
    public int GetResourceCount(Resource type)
    {
        if (Resources.TryGetValue(type, out int currentCount))
        {
            return currentCount;
        }
        else
        {
            return 0;
        }
    }

    public int AddResources(Resource type, int count)
    {
        if (Resources.TryGetValue(type, out int currentCount))
        {
            return Resources[type] += count;
        }
        else
        {
            Resources.Add(type, count);
            return count;
        }
    }

    // Methode, die beim Button-Klick aufgerufen wird
    private void DisplayScorePoints()
    {
        if (scorePointsResource != null)
        {
            int count = GetResourceCount(scorePointsResource);
            Debug.Log($"ScorePoints Resource Count: {count}");

            // Überprüfen, ob der Wert der ScorePoints Resource mindestens 1500 beträgt
            if (count >= 1500)
            {
                // Wechsel zur Win_Outro Szene
                SceneManager.LoadScene("Win_Outro");
            }
            else
            {
                // Wechsel zur Loose_outro Szene, wenn der Punktestand weniger als 1500 ist
                SceneManager.LoadScene("Loose_outro");
            }
        }
        else
        {
            Debug.LogWarning("ScorePoints Resource ist nicht zugewiesen.");
        }
    }
}
