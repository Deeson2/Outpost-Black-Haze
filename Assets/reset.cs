using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset : MonoBehaviour
{

    public GameObject targetsPrefab;
    private GameObject currentTargets;
    void Start()
    {
        currentTargets = GameObject.FindGameObjectWithTag("Targets");
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))  
        {
         ResetTargets();
        }       

    }

    void ResetTargets()
    {
        Destroy(currentTargets);
        currentTargets = Instantiate(targetsPrefab, transform.position, Quaternion.identity);
        Debug.Log("Respawning targets...");
    }

    
}
