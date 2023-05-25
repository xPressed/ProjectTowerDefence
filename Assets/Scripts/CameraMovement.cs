using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float turnSpeed = 5f;
    private float angle = 0;

    public float zoomFOV = 30f;
    public float zoomSpeed = 500f;
    private float defaultFOV;
    private float targetFOV;
    
    private Quaternion defaultRotation;

    private Vector3 mouseWorldPosition;
    private Vector3 mousePosition;
    private bool isZoomed = false;
    private Quaternion lookRotation;

    void Start()
    {
        defaultFOV = Camera.main.fieldOfView;
        targetFOV = defaultFOV;
        
        defaultRotation = Camera.main.transform.rotation;
        lookRotation = defaultRotation;
    }
    
    void Update()
    {
        if (GameManager.gameIsOver || PauseMenu.gameIsPaused)
        {
            enabled = false;
            return;
        }
        
        CameraRotation();
        CameraZoom();
    }

    void CameraRotation()
    {
        if (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow))
        {
            angle -= 90;
            lookRotation = Quaternion.Euler(65f, angle, 0f);
            defaultRotation = lookRotation;
        }

        if (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            angle += 90;
            lookRotation = Quaternion.Euler(65f, angle, 0f);
            defaultRotation = lookRotation;
        }
        
        Vector3 pointRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, angle, 0), Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, pointRotation.y, 0f);
    }

    void CameraZoom()
    {
        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
        {
            ZoomToMouse();
            isZoomed = true;
            targetFOV = zoomFOV;
        }

        if (Input.GetKeyUp("w") || Input.GetKeyUp(KeyCode.UpArrow))
        {
            isZoomed = false;
            targetFOV = defaultFOV;
            lookRotation = defaultRotation;
        }
        
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
        
        Vector3 camRotation = Quaternion.Lerp(Camera.main.transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        Camera.main.transform.rotation = Quaternion.Euler(camRotation);
    }

    void ZoomToMouse()
    { 
        if (isZoomed)
            return;
        
        mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane + 1;
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 dir = mouseWorldPosition - Camera.main.transform.position;
        lookRotation = Quaternion.LookRotation(dir);
    }
}
