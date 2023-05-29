using UnityEngine;
using System.Collections;

public class EnemyCan : MonoBehaviour
{
   public float health = 1f;

   public void TakeDamage (float amount)
{
    health -= amount;
    if (health <= 0f)
    {
        StartCoroutine(DieCan());
            return;
    }
}

IEnumerator DieCan ()
{
    yield return new WaitForSeconds(1f);
    Destroy(gameObject);
}


}

