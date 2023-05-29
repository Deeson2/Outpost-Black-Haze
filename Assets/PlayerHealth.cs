using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
   public int currentHealth;
   public int maxHealth;
   public Transform respawnPoint;
   private Vector3 initialPosition;


   void Start()
   {
       maxHealth = 100;
       currentHealth = maxHealth;
       Debug.Log("Start method called. Current health: " + currentHealth);
   }

   void Update()
   {
       if (currentHealth <= 0)
       {
           Debug.Log("death");
           PlayerRespawn();
       }
   }

   public void PlayerRespawn()
   {
       transform.position = respawnPoint.position;
       currentHealth = maxHealth;
   }
}
