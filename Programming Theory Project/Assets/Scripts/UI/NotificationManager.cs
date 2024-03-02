using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }

    [SerializeField] private GameObject notificationsPanel;
    [SerializeField] private GameObject notificationPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        if(notificationsPanel == null)
        {
            Debug.LogWarning($"{nameof(notificationsPanel)} field not assigned, drag and drop it in editor");
        }

        if(notificationPrefab == null)
        {
            Debug.LogWarning($"{nameof(notificationPrefab)} field not assigned, drag and drop it in editor");
        }
    }    

    public void CreateNotify(string msg, Notification.NotificationTypes type)
    {
        var notificationInstance = Instantiate(notificationPrefab);
        notificationInstance.transform.SetParent(notificationsPanel.transform);

        Notification notif_component = notificationInstance.GetComponent<Notification>();

        notif_component.NotificationFormat(msg, type);
    }

    public void CreateNotify(string msg, Color color)
    {
        var notificationInstance = Instantiate(notificationPrefab);
        notificationInstance.transform.SetParent(notificationsPanel.transform);

        Notification notif_component = notificationInstance.GetComponent<Notification>();

        notif_component.NotificationFormat(msg, color);
    }
}
