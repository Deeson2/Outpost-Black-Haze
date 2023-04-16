using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
 public Animator animator;
//unslash below for scope overlay
//public GameObject scopeOverlay;
//public GameObject weaponCamera;


 
 private bool isScoped = false;

 void Update ()
 {
     if(Input.GetButtonDown("Fire2"))
     {
         isScoped = !isScoped;
         animator.SetBool("IsScoped", isScoped);
        
        // unslash for scope overlay
         //if (isScoped)
         //StartCoroutine(OnScoped());
         //else
         //OnUnscoped();
     }
 } 
//unslash for scope overlay
 //void OnUnscoped ()
// {
   // scopeOverlay.SetActive(false);
   //weaponCamera.SetActive(true);
 //}

// IEnumerator OnScoped ()
 //{
//    yield return new WaitForSeconds(.15f);

    //scopeOverlay.SetActive(true);
    //weaponCamera.SetActive(true);
// } 
}

