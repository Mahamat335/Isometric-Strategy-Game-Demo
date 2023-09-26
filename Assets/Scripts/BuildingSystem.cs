using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    //public GameObject greenBuilding;
    //public GameObject building;
    public GameObject buildingPanel;
    public List<GameObject> greenBuildingList;
    public List<GameObject> buildingList;
    public LayerMask BluePrintLayer;
    public GameObject selectionFrame;
    int currentBuildingIndex;
    bool BuildingModeOn;
    Transform collidedBuildingTransform;
    Quaternion newRotation;
    RaycastHit hit;
    Vector3 rotateStartPosition, rotateCurrentPosition;
    Animator buildingPanelAnimator;
    void Start(){
        BuildingModeOn = false;
        currentBuildingIndex = 0;
        buildingPanelAnimator = buildingPanel.GetComponent<Animator>();
    }

    void Update()
    {
        if(BuildingModeOn && currentBuildingIndex != 0){
            
            selectionFrame.SetActive(true);
            selectionFrame.GetComponent<RectTransform>().anchoredPosition = new Vector3(5 + 45*(currentBuildingIndex - 1), 0, 0);
        
            greenBuildingList[currentBuildingIndex-1].SetActive(true);
            bool isBuildable = greenBuildingList[currentBuildingIndex-1].GetComponent<GreenBuildingScript>().isBuildable;
            collidedBuildingTransform = greenBuildingList[currentBuildingIndex-1].GetComponent<GreenBuildingScript>().collidedBuildingTransform;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//Camera.main.transform.position, Camera.main.transform.forward

            if(Input.GetKeyDown(KeyCode.Mouse2)){
                    rotateStartPosition = Input.mousePosition;
            } 
            if(Input.GetKey(KeyCode.Mouse2)){
                    rotateCurrentPosition = Input.mousePosition;
                    Vector3 difference =  rotateStartPosition - rotateCurrentPosition;
                    rotateStartPosition = rotateCurrentPosition;
                    newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
                    if(Input.GetKeyDown(KeyCode.Mouse0) && isBuildable){
                        Instantiate(buildingList[currentBuildingIndex-1], greenBuildingList[currentBuildingIndex-1].transform.position, greenBuildingList[currentBuildingIndex-1].transform.rotation);
                        if(!Input.GetKey(KeyCode.LeftShift)){
                            greenBuildingList[currentBuildingIndex-1].transform.rotation = Quaternion.Euler(Vector3.up * 0);
                            SetCurrentBuildingIndex(0);
                            selectionFrame.SetActive(false);
                        }
                    }
                    if(Input.GetKeyDown(KeyCode.Mouse1)){
                        greenBuildingList[currentBuildingIndex-1].GetComponent<GreenBuildingScript>().defaultTriggerExit();
                        greenBuildingList[currentBuildingIndex-1].transform.rotation = Quaternion.Euler(Vector3.up * 0);
                        SetCurrentBuildingIndex(0);
                        selectionFrame.SetActive(false);
                    }
            } else if(Physics.Raycast(ray, out hit, 999, ~BluePrintLayer)){
                
                greenBuildingList[currentBuildingIndex-1].transform.position = hit.point;
                newRotation = greenBuildingList[currentBuildingIndex-1].transform.rotation;
                
                if(Input.GetKeyDown(KeyCode.Mouse0) && isBuildable){
                    Instantiate(buildingList[currentBuildingIndex-1], hit.point, greenBuildingList[currentBuildingIndex-1].transform.rotation);
                    if(!Input.GetKey(KeyCode.LeftShift)){
                        greenBuildingList[currentBuildingIndex-1].transform.rotation = Quaternion.Euler(Vector3.up * 0);
                        SetCurrentBuildingIndex(0);
                        selectionFrame.SetActive(false);
                    }
                }
                if(Input.GetKeyDown(KeyCode.Mouse1)){
                    greenBuildingList[currentBuildingIndex-1].GetComponent<GreenBuildingScript>().defaultTriggerExit();
                    greenBuildingList[currentBuildingIndex-1].transform.rotation = Quaternion.Euler(Vector3.up * 0);
                    SetCurrentBuildingIndex(0);
                    selectionFrame.SetActive(false);
                }
            }

            if(currentBuildingIndex>0)
                greenBuildingList[currentBuildingIndex-1].transform.rotation = newRotation;

            
            if(Input.GetKeyDown(KeyCode.R)){
                if(isBuildable){
                    float currentY = greenBuildingList[currentBuildingIndex-1].transform.eulerAngles.y;
                    if(currentY<0)
                        newRotation = Quaternion.Euler(Vector3.up * (Mathf.Round(currentY / 90f) * 90f ));//CeilToInt
                    else
                        newRotation = Quaternion.Euler(Vector3.up * (Mathf.Round(currentY / 90f) * 90f ));//FloorToInt
                    newRotation *= Quaternion.Euler(Vector3.up * 90);
                    greenBuildingList[currentBuildingIndex-1].transform.rotation = newRotation;
                }else if(collidedBuildingTransform != null){
                    greenBuildingList[currentBuildingIndex-1].transform.rotation = collidedBuildingTransform.transform.rotation;
                }
                
            }

        }
        /* else{
            greenBuilding.SetActive(false);
        } */


        if(Input.GetKeyDown(KeyCode.B)){
            if(BuildingModeOn){
                buildingPanelAnimator.SetBool("opening", false);
                if(currentBuildingIndex!=0){
                    greenBuildingList[currentBuildingIndex-1].GetComponent<GreenBuildingScript>().defaultTriggerExit();
                    greenBuildingList[currentBuildingIndex-1].transform.rotation = Quaternion.Euler(Vector3.up * 0);
                    greenBuildingList[currentBuildingIndex-1].SetActive(false);
                    SetCurrentBuildingIndex(0);
                    selectionFrame.SetActive(false);
                }
            }   
            else{
                buildingPanel.SetActive(true);
                buildingPanelAnimator.SetBool("opening", true);
                if(currentBuildingIndex!=0){
                    greenBuildingList[currentBuildingIndex-1].GetComponent<GreenBuildingScript>().defaultTriggerExit();
                    greenBuildingList[currentBuildingIndex-1].transform.rotation = Quaternion.Euler(Vector3.up * 0);
                    greenBuildingList[currentBuildingIndex-1].SetActive(false);
                    SetCurrentBuildingIndex(0);
                    selectionFrame.SetActive(false);
                }
                
            }
                
            BuildingModeOn = !BuildingModeOn;
           
        }
        if(buildingPanel.activeSelf && !BuildingModeOn){
            if(buildingPanel.GetComponent<RectTransform>().anchoredPosition.y == -50){
                buildingPanel.SetActive(false);
            }
        }
        
        
        if(BuildingModeOn && Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2)){
            SetCurrentBuildingIndex(int.Parse(Input.inputString[0].ToString()));
        }


        
    }

    public void SetCurrentBuildingIndex(int Index){
        if(currentBuildingIndex > 0){
            greenBuildingList[currentBuildingIndex-1].GetComponent<GreenBuildingScript>().defaultTriggerExit();
            greenBuildingList[currentBuildingIndex-1].transform.rotation = Quaternion.Euler(Vector3.up * 0);
            greenBuildingList[currentBuildingIndex-1].SetActive(false);
        }
        currentBuildingIndex = (Index<0 && Index>buildingList.Count)?0:Index;
    }
}
