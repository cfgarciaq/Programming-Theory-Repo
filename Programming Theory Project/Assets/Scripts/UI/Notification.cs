using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{

    public enum NotificationTypes {Alert, Warning, Info }
    private NotificationTypes Type = NotificationTypes.Alert;
    public string NotificationMessage { set; private get; }

    //public Color[] NotificationColors = new Color[4] { new Color(0, 0, 0), new Color(0, 0, 0), new Color(0, 0, 0), new Color(0, 0, 0) };

    [SerializeField] private Text notificationText;
    
    [SerializeField] 
    private Dictionary<NotificationTypes, Color> NotificationColors = new Dictionary<NotificationTypes, Color>() 
    {
        {NotificationTypes.Alert, Color.HSVToRGB(56,100,93)},
        {NotificationTypes.Warning, Color.HSVToRGB(355,100,85)},
        {NotificationTypes.Info, Color.HSVToRGB(216,80,93)}
    };


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FormatNotification(string Message, NotificationTypes type)
    {
        notificationText.text = Message;
        notificationText.color = NotificationColors[type];

        /*
        switch (type)
        {
            case NotificationTypes.Alert:
                notificationText.color = NotificationColors[];
                break;

            case NotificationTypes.Warning:
                notificationText.color = Color.red;
                break;

            case NotificationTypes.Info:
                notificationText.color = Color.blue;
                break;

            default:
            notificationText.color = Color.white;
                break;
        }
        */
    }

}
