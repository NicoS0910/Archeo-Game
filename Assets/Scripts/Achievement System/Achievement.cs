using UnityEngine;

[System.Serializable]
public class Achievement
{
    public string id; // Eindeutige ID des Achievements
    public string description; // Beschreibung des Achievements
    public GameObject activateOnPickup; // Objekt, das bei Aufsammeln aktiviert wird
    public GameObject activateOnScan; // Objekt, das beim Scannen aktiviert wird
    public bool isAchieved; // Flag, ob das Achievement erreicht wurde (optional)
    public int progress; // Fortschritt für Fortschritts-Achievements (optional)
    public bool hasServer; // Flag, ob der Spieler den Server hat

    // Konstruktor für einfache Erstellung eines Achievements
    public Achievement(string id, string description, GameObject activateOnPickup, GameObject activateOnScan)
    {
        this.id = id;
        this.description = description;
        this.activateOnPickup = activateOnPickup;
        this.activateOnScan = activateOnScan;
        this.isAchieved = false;
        this.progress = 0;
        this.hasServer = false; // Initialisierung des hasServer-Flags
    }
}
