using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    public static TextMeshProUGUI ammo;

    private void Start()
    {
        ammo = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public static void changeText(int currentAmmo, int maxAmmo)
    {
        ammo.text = currentAmmo + "/" + maxAmmo;
    }
}
