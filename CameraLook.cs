using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;
    
    Camera camera;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        if (camera.enabled)
        {
            MoveCamera();

            camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        else
        {
            return;
        }
    }

    private void MoveCamera()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }

}

   
