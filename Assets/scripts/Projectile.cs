using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    public float speed = 10f;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Move player = other.gameObject.GetComponent<Move>();
        if(player != null)
            {
                player.TakeDamage();
            }
            
        if(other.tag != "enemy")
        Destroy(gameObject);
    }
}
