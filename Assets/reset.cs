using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class reset : MonoBehaviour
{

    public GameObject targetsPrefab;
    private GameObject currentTargets;
    private int MaxTargets;
    private int TargetsLeft;
    public TextMeshProUGUI CounterText;

   
    void Start()
    {
        currentTargets = GameObject.FindGameObjectWithTag("Targets");

        MaxTargets = targetsPrefab.transform.childCount;   
    }

    void Update()
    {   
        
        TargetsLeft = currentTargets.transform.childCount;
        CounterText.text = $"{TargetsLeft}/{MaxTargets}";

        if(Input.GetKeyDown(KeyCode.H))  
        {
         ResetTargetsNoWait();
         Timer.Instance.ResetTimer();
        }

        if(Input.GetKeyDown(KeyCode.G))  
        {
         ResetTargetsNoWait();
         Timer.Instance.StartTimer();
        }
       
        if(TargetsLeft <= 0)
        {
         StartCoroutine(ResetTargets());
         Timer.Instance.EndTimer();
        }    

    }

    IEnumerator ResetTargets()
    {
        yield return new WaitForSeconds(2f);
        Destroy(currentTargets);
        currentTargets = Instantiate(targetsPrefab, transform.position, Quaternion.identity);
        Debug.Log("Respawning targets...");
    }

    void ResetTargetsNoWait()
    {
        Destroy(currentTargets);
        currentTargets = Instantiate(targetsPrefab, transform.position, Quaternion.identity);
        Debug.Log("Respawning targets...");
    }

}
