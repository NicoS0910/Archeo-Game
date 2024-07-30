using UnityEngine;

public class TaskListManager : MonoBehaviour
{
    public GameObject taskListPanel; // Das Panel, das die Task-Liste enthält
    private bool isTaskListVisible = true; // Zeigt an, ob die Task-Liste sichtbar ist

    void Start()
    {
        if (taskListPanel == null)
        {
            Debug.LogError("TaskListPanel is not assigned.");
        }
    }

    void Update()
    {
        // Überprüfe, ob die Space-Taste gedrückt wurde
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Wechsle die Sichtbarkeit der Task-Liste
            isTaskListVisible = !isTaskListVisible;
            taskListPanel.SetActive(isTaskListVisible);
        }
    }
}
