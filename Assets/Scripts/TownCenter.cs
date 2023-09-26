using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCenter : MonoBehaviour
{
    public GameObject cameraRig;
    public GameObject unit;
    public float timeCounter;
    public float period;
    // Start is called before the first frame update
    void Start()
    {   
        timeCounter = 0.0f;
        period = 5f;
        GetComponent<Transform>().position = new Vector3(Random.Range(0, 300), 0, Random.Range(0, 300));
        cameraRig.GetComponent<Transform>().position = GetComponent<Transform>().position;
        CreateUnit();
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        if(timeCounter >= period){
            timeCounter = 0;
            //CreateUnit();
        }
    }

    void CreateUnit(){
        float randomValue1 = Random.Range(-5, 5);
        float randomValue2 = Random.Range(-5, 5);
        float randomValue3 = Random.Range(-10, 10);
        if(randomValue1 == 0){
            randomValue1 += 0.01f;
        }
        if(randomValue2 == 0){
            randomValue2 += 0.01f;
        }
        if(randomValue3 == 0){
            randomValue3 = 1;
        }
        Instantiate(unit, GetComponent<Transform>().position + new Vector3(randomValue1*(Mathf.Abs(randomValue3)/randomValue3)+3*(Mathf.Abs(randomValue1)/randomValue1), 0, randomValue2*(Mathf.Abs(randomValue3)/randomValue3)+3*(Mathf.Abs(randomValue2)/randomValue2) * 5), GetComponent<Transform>().rotation);
    }
}
