
using UnityEngine;
using System.Collections;
public enum ReloadSounds
{
    MagOut, 
    MagIn,
    Slide
}
public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;

    public int maxAmmo = 10;
    public int currentAmmo { get; private set; }
    public float reloadTime = 1f;
    private bool isReloading = false;
    public static bool canShoot = true;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float fireRate = 10f;
    private float nextTimeToFire = 0f;

    public Animator animator;

    public AudioClip ShootingSound;
    public AudioClip[] reloadSounds;
    public AudioSource audioSource;
    

    void start ()
    {
        currentAmmo = maxAmmo;

        AmmoCounter.changeText(currentAmmo, maxAmmo);

        //audioSource = gameObject.GetComponent<AudioSource>();
        
    }

    void OnEnable ()
    {
        isReloading = false;
        animator.SetBool("IsReloading", false);
    }
    void Update ()
    {

        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
        }
    }

    IEnumerator Reload ()
    {
        isReloading = true;
        Debug.Log ("Reloading...");

        animator.SetBool("IsReloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);

        animator.SetBool("IsReloading", false);

        yield return new WaitForSeconds(.25f);


        currentAmmo = maxAmmo;
        AmmoCounter.changeText(currentAmmo, maxAmmo);

        isReloading = false;
    }


    void Shoot ()
    {

        if (canShoot == false) { return; }

        muzzleFlash.Play();

        audioSource.PlayOneShot(ShootingSound);

        currentAmmo--;

        AmmoCounter.changeText(currentAmmo, maxAmmo);

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null) 
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impaceGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impaceGO, 2f);
        }
    }

    public void TriggerReloadSounds (ReloadSounds sound)
    {
        audioSource.PlayOneShot(reloadSounds[(int)sound]);
    }

}
