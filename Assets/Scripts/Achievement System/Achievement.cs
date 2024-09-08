using UnityEngine;

[System.Serializable]
public class Achievement
{
    public string id;
    public string description;
    public GameObject activateOnPickup;
    public GameObject activateOnScan;
    public bool isAchieved;
    public int progress;
    public bool hasServer;

    public Achievement(string id, string description, GameObject activateOnPickup, GameObject activateOnScan)
    {
        this.id = id;
        this.description = description;
        this.activateOnPickup = activateOnPickup;
        this.activateOnScan = activateOnScan;
        this.isAchieved = false;
        this.progress = 0;
        this.hasServer = false;
    }
}
