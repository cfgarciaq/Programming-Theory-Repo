using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{

    //public string NotificationMessage { set; private get; }
    public enum NotificationTypes {Alert, Warning, Info }
    //private NotificationTypes Type = NotificationTypes.Alert;
    
    [SerializeField] private Text notificationText;
    
    private Dictionary<NotificationTypes, Color> NotificationColors = new Dictionary<NotificationTypes, Color>() 
    {
        {NotificationTypes.Alert, new Color(0.95f, 0.74f, 0.10f)},
        {NotificationTypes.Warning, new Color(0.83f, 0.15f, 0.15f)},
        {NotificationTypes.Info, new Color(0.18f, 0.48f, 0.92f)}
    };


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    public void NotificationFormat(string Message, NotificationTypes type)
    {
        notificationText.material.color = Color.white;
        notificationText.color = NotificationColors[type];
        notificationText.text = Message;

        Debug.Log(NotificationColors[type]);
    }

    public void NotificationFormat(string Message, Color color)
    {
        notificationText.color = color;
        notificationText.text = Message;

        Debug.Log(color);
    }
}
