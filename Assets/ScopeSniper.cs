using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeSniper : MonoBehaviour
{
 public Animator animator;

public GameObject scopeOverlay;
public GameObject weaponCamera;
public bool IsReloading;


 
 private bool isScoped = false;
 private bool CanScope = true;

 void OnEnable ()
 {
     scopeOverlay.SetActive(false);
 }

 void Update ()
 {
     if(IsReloading == true)
     {
         CanScope = false;
     }

     if(Input.GetButtonDown("Fire2"))
     {
         isScoped = !isScoped;
         animator.SetBool("IsScoped", isScoped);
        
         if (isScoped)
         StartCoroutine(OnScoped());
         else
         OnUnscoped();
     }
 } 

 void OnUnscoped ()
 {
    scopeOverlay.SetActive(false);
   weaponCamera.SetActive(true);
 }

 IEnumerator OnScoped ()
 {
    yield return new WaitForSeconds(.26f);

    scopeOverlay.SetActive(true);
    weaponCamera.SetActive(false);
 } 
}


