using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBuildingScript : MonoBehaviour
{
    public int layerNumber = 6;
    public Material redMaterial;
    public Material greenMaterial;
    public bool isBuildable;
    public Transform collidedBuildingTransform;
    void Start(){
        isBuildable = true;
    }
    // Start is called before the first frame update
    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.layer == layerNumber) 
            
        {
            ChangeMaterial(redMaterial);
            isBuildable = false;
            collidedBuildingTransform = collision.gameObject.GetComponent<Transform>();
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == layerNumber)
        {
            ChangeMaterial(greenMaterial);
            isBuildable = true;
            collidedBuildingTransform = null;
        }
    }

    void ChangeMaterial(Material newMat)
     {
         Renderer[] children;
         //GameObject parent;
         children = this.GetComponentsInChildren<Renderer>();
         foreach (Renderer rend in children)
         {
             var mats = new Material[rend.materials.Length];
             for (var j = 0; j < rend.materials.Length; j++)
             {
                 mats[j] = newMat;
             }
             rend.materials = mats;
         }
     }
     public void defaultTriggerExit(){
        ChangeMaterial(greenMaterial);
        isBuildable = true;
        collidedBuildingTransform = null;
     }
}
