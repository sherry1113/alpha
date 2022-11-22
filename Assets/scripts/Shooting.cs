using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public Camera cam;
    public AudioClip GunShot;
    AudioSource gunSound;

    public ParticleSystem muzzleFlash;

    public GameObject sparkEffect;

    private float nextTimeToFire = 0;

    void Start()
    {
        gunSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.TakeDamage(damage);
                Instantiate(sparkEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }

        muzzleFlash.Play();
        gunSound.PlayOneShot(GunShot);
    }
}
