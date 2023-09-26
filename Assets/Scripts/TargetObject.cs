using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    public bool isColliding;
    public int layerNumber = 6;
    void Start(){
        isColliding = false;
    }
    void OnTriggerEnter(Collider collision){
        if (collision.gameObject.layer == layerNumber) {
            Debug.Log( "ERRRKEKKKKK");
        }
           // isColliding = true;
    }
    void OnTriggerStay(Collider collision){
        if (collision.gameObject.layer == layerNumber){
            Debug.Log( "deasdasdneme");
            isColliding = true;
        }
    }
    void OnTriggerExit(Collider collision){
        if (collision.gameObject.layer == layerNumber) 
            isColliding = false;
    }
}
