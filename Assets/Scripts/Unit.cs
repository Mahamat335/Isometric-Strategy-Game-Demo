using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Unit : MonoBehaviour
{
    public float movementSpeed;
    public float movementTime;
    public bool isIdle;
    float currentTime;
    float waitingTime;
    Vector3 targetLocation;
    private NavMeshAgent navAgent;
    RaycastHit hit;
    public GameObject targetObjectPrefab;
    GameObject targetObject;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 5f;
        isIdle = true;
        waitingTime = 2f;
        currentTime = 0f;
        navAgent = GetComponent<NavMeshAgent>();
        targetObject = Instantiate(targetObjectPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }
    void Move(){
        if(targetObject.GetComponent<TargetObject>().isColliding)
            //Debug.Log("OLLLEYYY");
        if(isIdle){
            Debug.Log( "deneme");
            do{
                
                targetObject.transform.position = transform.position + new Vector3(Random.Range(5,10)*(Random.Range(0,2)*2-1), 0, Random.Range(5,10)*(Random.Range(0,2)*2-1));
                Debug.Log(targetObject.transform.position);
                
            }while(targetObject.GetComponent<TargetObject>().isColliding);
            Debug.Log( "çıktık" + targetObject.GetComponent<TargetObject>().isColliding);
            navAgent.destination = targetObject.transform.position;
            isIdle = false;
        }
        if(Vector3.Distance(transform.position, targetObject.transform.position)<=0.1f){
            currentTime += Time.deltaTime;
            if(currentTime>= waitingTime){
                isIdle = true;
                currentTime = 0;
            }
        }
    }
    void calculateTargetLocation(){
        targetObject.transform.position = transform.position + new Vector3(Random.Range(5,10)*(Random.Range(0,2)*2-1), 0, Random.Range(5,10)*(Random.Range(0,2)*2-1));
        
        
    }
}
