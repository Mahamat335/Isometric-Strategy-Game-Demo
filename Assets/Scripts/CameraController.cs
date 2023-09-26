using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;
    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;
    public Vector3 zoomAmount;
    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;
    public float edgeSize = 10f;
    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
     HandleMovementInput();   
    }
    
    void HandleMovementInput(){
        if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow)){//||Input.mousePosition.y > Screen.height - edgeSize){
            newPosition += transform.forward * movementSpeed;
        }
        if(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow)){//||Input.mousePosition.y < edgeSize){
            newPosition += transform.forward * -movementSpeed;
        }
        if(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow)){//||Input.mousePosition.x > Screen.width - edgeSize){
            newPosition += transform.right * movementSpeed;
        }
        if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow)){//||Input.mousePosition.x < edgeSize){
            newPosition += transform.right * -movementSpeed;
        }
        if(Input.GetKey(KeyCode.Q)){
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if(Input.GetKey(KeyCode.E)){
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        if(Input.mouseScrollDelta.y != 0){
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
}