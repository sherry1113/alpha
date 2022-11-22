using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenueButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(1);        
    }

    public void Replay()
    {
            Time.timeScale = 1f;
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
    public void Menu()
    {
            Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
