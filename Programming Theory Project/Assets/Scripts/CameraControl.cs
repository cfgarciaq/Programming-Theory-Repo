using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    private Vector3 initialPosition = new Vector3(0,10,-12);
    private const int defaultFOV = 60;
    [SerializeField] private float scrollScale = 0.25f;

    private Vector3 m_Position;
    private Vector3 mouseDirection;
    private float mouseDistanceToCenter;
    

    private Vector3 ScreenCenterPoint; 
    private float screenWidth10Percent;
    private float screenHeight10Percent;

    private void Awake()
    {        
        ScreenCenterPoint = CalculateScreenCenter();
        transform.position = initialPosition;
        Camera.main.fieldOfView = defaultFOV;
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

        CameraZoom();

        if (Input.GetMouseButton(1))
        {
            CameraMovement();
        }
    }
    
    private void CameraZoom()
    {

        //increasing FOV zooms out, decreasing Fov zooms in
        float currentFov = Camera.main.fieldOfView;

        bool q = Input.GetKey(KeyCode.Q);
        bool e = Input.GetKey(KeyCode.E);
        
        if (e ^ q)
        {
            if (e) currentFov += scrollScale * -1; // -1 to zoom in
            if (q) currentFov += scrollScale; //  to zoom out
        }

        // scroll up: +1 (zoom out)  scroll down: -1 (zoom in) no scroll: 0        
        currentFov += Input.mouseScrollDelta.y * scrollScale * -1; // multiply by -1 to invert behavior

        //clamp min and max zoom
        Camera.main.fieldOfView = Mathf.Clamp(currentFov, 25f, defaultFOV);
    }

    private void CameraMovement()
    {
        //float averageDistanceFromCenter = (ScreenCenterPoint.x + ScreenCenterPoint.y) * 0.5f;
        float distanceToCenter10Percent = ScreenCenterPoint.y * 0.10f;

        if(mouseDistanceToCenter < distanceToCenter10Percent * 5) return; //if mouse distance is les than 30% away do nothing
        
        if (mouseDistanceToCenter > distanceToCenter10Percent * 8) MoveCamera(12f);
        else if (mouseDistanceToCenter >= distanceToCenter10Percent * 7) MoveCamera(8f);
        else if (mouseDistanceToCenter >= distanceToCenter10Percent * 6) MoveCamera(4f);
        else if (mouseDistanceToCenter >= distanceToCenter10Percent * 5) MoveCamera(2.5f);
    }

    private void MoveCamera(float speed)
    {
        Debug.Log($"camera speed: {speed}");
        Vector3 camPos = transform.position;
        camPos.x += mouseDirection.x * speed * Time.deltaTime;
        camPos.z += mouseDirection.y * speed * Time.deltaTime;
        camPos.x = Mathf.Clamp(camPos.x, -18, 18);
        camPos.z = Mathf.Clamp(camPos.z, -17, 5);
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
        Vector3 scrCenter = CalculateScreenCenter();
        if (ScreenCenterPoint != scrCenter)
        {
            ScreenCenterPoint = scrCenter;
        }        
    }
}
