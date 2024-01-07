using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    private Vector3 m_Position;
    private Vector3 mouseDirection;
    private float mouseDistanceToCenter;
    

    private Vector3 ScreenCenterPoint; 
    private float screenWidth10Percent;
    private float screenHeight10Percent;

    private void Awake()
    {        
        ScreenCenterPoint = CalculateScreenCenter();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        CheckScreenSize();
        
        m_Position = Input.mousePosition;
        MouseDistanceAndDirectionFromCenter(m_Position);

        CameraMovement();

    }

    private void CameraMovement()
    {
        float averageDistanceFromCenter = (Screen.width / 2 + Screen.height / 2) * 0.5f;
        float distanceToCenter10Percent = averageDistanceFromCenter * 0.10f;

        if(mouseDistanceToCenter < distanceToCenter10Percent * 2) return;

        if (mouseDistanceToCenter > distanceToCenter10Percent * 3) MoveCamera(15f);
        else if (mouseDistanceToCenter >= distanceToCenter10Percent * 2) MoveCamera(5f);
    }

    private void MoveCamera(float speed)
    {
        Debug.Log($"camera speed: {speed}");
        Vector3 camPos = transform.position;
        camPos.x += mouseDirection.x * speed * Time.deltaTime;
        camPos.z += mouseDirection.y * speed * Time.deltaTime;
        transform.position = camPos;
    }

    private void MouseDistanceAndDirectionFromCenter(Vector3 mousePosition)
    {        
        Vector3 difference = mousePosition - ScreenCenterPoint;

        mouseDirection = difference.normalized; // mouse direction relative to screen center
        mouseDistanceToCenter = difference.magnitude; // mouse distance relative to screen center
    }

    private Vector3 CalculateScreenCenter()
    {
        return new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }

    private void CheckScreenSize()
    {
        if(ScreenCenterPoint != CalculateScreenCenter())
        {
            ScreenCenterPoint = CalculateScreenCenter();
        }        
    }
}
