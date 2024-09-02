using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    // Singleton-Instanz des AchievementManagers
    private static AchievementManager instance;
    public static AchievementManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AchievementManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(AchievementManager).Name;
                    instance = obj.AddComponent<AchievementManager>();
                }
            }
            return instance;
        }
    }

    // Klasse für die Darstellung eines Achievements
    public class Achievement
    {
        public string id;
        public string description;
        public GameObject activateOnPickup; // GameObject, das bei Pickup aktiviert werden soll
        public GameObject activateOnScan;   // GameObject, das bei Scan aktiviert werden soll
        public bool isAchieved;             // Ob das Achievement erreicht wurde
        public bool hasServer;              // Zusätzliche Eigenschaft spezifisch für das Beispiel

        public Achievement(string id, string description, GameObject pickupObject, GameObject scanObject)
        {
            this.id = id;
            this.description = description;
            this.activateOnPickup = pickupObject;
            this.activateOnScan = scanObject;
            this.isAchieved = false;
            this.hasServer = false;
        }
    }

    // Liste aller Achievements
    public List<Achievement> achievements = new List<Achievement>();

    public GameObject pickupAchievementObject; // Referenz auf das GameObject für das Pickup-Achievement
    public GameObject scanAchievementObject;   // Referenz auf das GameObject für das Scan-Achievement

    void Start()
    {
        // Beispiel: Füge hier Achievements hinzu
        Achievement scanAchievement = new Achievement("scan_achievement", "You scanned an object!", null, scanAchievementObject);
        Achievement pickupAchievement = new Achievement("pickup_achievement", "You picked up an item!", pickupAchievementObject, null);

        // Füge die Achievements der Liste hinzu
        achievements.Add(scanAchievement);
        achievements.Add(pickupAchievement);
    }

    public void ActivateObject(string achievementId, bool isPickup)
    {
        Achievement achievement = achievements.Find(a => a.id == achievementId);
        if (achievement == null)
        {
            Debug.LogError("Achievement not found with id: " + achievementId);
            return;
        }

        GameObject objToActivate = isPickup ? achievement.activateOnPickup : achievement.activateOnScan;
        if (objToActivate != null)
        {
            objToActivate.SetActive(true);
            StartCoroutine(DeactivateObjectAfterDelay(objToActivate, 3f)); // Deaktiviere das Objekt nach 3 Sekunden
        }
        else
        {
            Debug.LogError("Object to activate is not assigned for achievement: " + achievementId);
        }
    }

    private IEnumerator DeactivateObjectAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }

    // Methode zum Überprüfen, ob ein Achievement bereits erreicht wurde
    public bool IsAchievementReached(string achievementId)
    {
        Achievement achievement = achievements.Find(a => a.id == achievementId);
        if (achievement != null)
        {
            return achievement.isAchieved;
        }
        return false;
    }

    // Methode, um den Zustand des "hasServer"-Flags im AchievementManager zu setzen
    public void SetHasServer(bool value)
    {
        // Setze den Zustand von hasServer im entsprechenden Achievement
        Achievement achievement = achievements.Find(a => a.id == "scan_achievement"); // Geändert auf das verbleibende Achievement
        if (achievement != null)
        {
            achievement.hasServer = value;
            if (value)
            {
                ActivateObject("scan_achievement", false); // Aktiviere das Scan-Objekt
            }
        }

        // Kein Pickup Achievement mehr zu setzen, daher auskommentiert
        // Achievement pickupAchievement = achievements.Find(a => a.id == "pickup_achievement");
        // if (pickupAchievement != null)
        // {
        //     pickupAchievement.hasServer = value;
        //     if (value)
        //     {
        //         ActivateObject("pickup_achievement", true); // Aktiviere das Pickup-Objekt
        //     }
        // }
    }
}
