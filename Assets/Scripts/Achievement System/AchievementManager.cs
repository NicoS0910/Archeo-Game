using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
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

    public class Achievement
    {
        public string id;
        public string description;
        public GameObject activateOnPickup;
        public GameObject activateOnScan;
        public bool isAchieved;
        public bool hasServer;

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

    public List<Achievement> achievements = new List<Achievement>();

    public GameObject pickupAchievementObject;
    public GameObject scanAchievementObject;

    void Start()
    {
        Achievement scanAchievement = new Achievement("scan_achievement", "You scanned an object!", null, scanAchievementObject);
        Achievement pickupAchievement = new Achievement("pickup_achievement", "You picked up an item!", pickupAchievementObject, null);

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
            StartCoroutine(DeactivateObjectAfterDelay(objToActivate, 3f));
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

    public bool IsAchievementReached(string achievementId)
    {
        Achievement achievement = achievements.Find(a => a.id == achievementId);
        if (achievement != null)
        {
            return achievement.isAchieved;
        }
        return false;
    }

    public void SetHasServer(bool value)
    {
        Achievement achievement = achievements.Find(a => a.id == "scan_achievement");
        if (achievement != null)
        {
            achievement.hasServer = value;
            if (value)
            {
                ActivateObject("scan_achievement", false);
            }
        }
    }
}
