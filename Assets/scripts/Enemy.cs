using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public GameObject BloodEffect; 
    public ParticleSystem muzzleFlash;

    public Transform aim;
    public Transform player;
    public float range = 100f;

    bool inRange;

    AudioSource enemySound;
    public AudioClip GunShot;

    public GameObject HeartLive;

    public GameObject Explosin;
    public GameObject projectile;

    public void TakeDamage (float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(Explosin, transform.position, Quaternion.identity);
        Instantiate(HeartLive, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    

    void Shoot()
    {
        Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), transform.rotation);

        muzzleFlash.Play();
        enemySound.PlayOneShot(GunShot);
    }

    void Start()
    {
        enemySound = GetComponent<AudioSource>();
        Check();
    }


    void Update()
    {
        aim.LookAt(player);

        Vector3 targetPostition = new Vector3( player.position.x, 
                                        this.transform.position.y, 
                                        player.position.z ) ;
 this.transform.LookAt( targetPostition ) ;


    }

    void Check()
    {
        
        RaycastHit hit;
        if(Physics.Raycast(aim.position, aim.forward, out hit, range) && hit.collider.gameObject.tag == "Player")
        {
            Shoot();
            
        }
        Invoke("Check", 3f);

    }
}
