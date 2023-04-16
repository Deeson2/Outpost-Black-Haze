using UnityEngine;
using System.Collections;

public class WeaponSwitching : MonoBehaviour
{
    
    public int selectedWeapon = 0;

    public AudioClip holsterSound;
    public AudioSource audioSource;

    private bool IsSwitching = false;
    public Animator animator;



    void Start()
    {
        StartCoroutine(SelectWeapon());
        return;
    }

     void OnEnable ()
    {
        IsSwitching = false;
        animator.SetBool("IsSwitching", false);
    }

    void Update()
    {

        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selectedWeapon = 3;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            StartCoroutine(SelectWeapon());
            return;
        }
    }

    IEnumerator SelectWeapon ()
    {
        Gun.canShoot = false;
        IsSwitching = true;
        animator.SetBool("IsSwitching", true);
        
        audioSource.PlayOneShot(holsterSound);
        
        yield return new WaitForSeconds(.75f);

        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++; 
        }

        Gun currentGun = transform.GetChild(selectedWeapon).gameObject.GetComponent<Gun>();

        animator.SetBool("IsSwitching", false);
        IsSwitching = false;
        Gun.canShoot = true;

        AmmoCounter.changeText(currentGun.currentAmmo, currentGun.maxAmmo);

    }
}
