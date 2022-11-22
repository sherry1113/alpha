using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    public float speed = 0.1f;
    public float health = 3f;
    public float Lives = 3f;
    public bool isGrounded;
    public float jumpAmount;
    Rigidbody rb;
    AudioSource playerAudio;
    public AudioClip HurtSound;
    public AudioClip Collected;
    public AudioClip HeartCollect;

    public int Flags;
    public Text flagNumber;

    public GameObject WinPanel;
    public GameObject OverPanel;
    public Slider HealthBar;

    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;

    public Animator Soldier;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        flagNumber.text = "Checkpoint = " + Flags + "/4";
        HealthBar.value = health;
        if(health <= 0 && Lives > 0)
        {
            Lives -= 1;
            health = 3;
        }
        
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * speed;

        transform.Translate(rotation, 0, translation);

        if(translation > 0.01f || rotation > 0.01f)
        Soldier.SetBool("Walking", true);
        else Soldier.SetBool("Walking", false);
        

        if (Input.GetKeyDown("space"))
        {
            if(isGrounded == true)
            {
                rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
                isGrounded = false;                
            }
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = 0.2f;
        }else{
            speed = 0.1f;
        }

        if(Flags >= 4)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(2);
        }

        if(Lives == 3)
        {
            Heart3.SetActive(true);
            Heart2.SetActive(true);
            Heart1.SetActive(true);
        }
        else if(Lives == 2)
        {
            Heart3.SetActive(false);
            Heart2.SetActive(true);
            Heart1.SetActive(true);
        }else if(Lives == 1)
        {
            Heart3.SetActive(false);
            Heart2.SetActive(false);
            Heart1.SetActive(true);
        }else if(Lives == 0)
        {
            Heart3.SetActive(false);
        }
        if(Lives <= 0f)
        {
            Debug.Log("You Died");
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(3);
        }
    }

    public void TakeDamage()
    {
        health -= 1;
        playerAudio.PlayOneShot(HurtSound);
    }


    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "flag")
        {
            playerAudio.PlayOneShot(Collected);
            Flags += 1;
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "heart")
        {
            if(Lives < 3)
            {
                Lives += 1;
            playerAudio.PlayOneShot(HeartCollect);
            Destroy(other.gameObject);
            }
            else if(health < 3)
            {
                health = 3f;
            playerAudio.PlayOneShot(HeartCollect);
            Destroy(other.gameObject);
            }
        }
    }
}
