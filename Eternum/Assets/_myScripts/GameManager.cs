using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menuPause;
    
    private bool pausado = false;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Pause();  
    }

    public void Pause()
    {
        if (!pausado)
        {
            menuPause.SetActive(true);
            pausado = true;
            Time.timeScale = 0;
        }
        else
        {
            menuPause.SetActive(false);
            pausado = false;
            Time.timeScale = 1;
        }
            

    }

    public void Resume()
    {
        if (pausado)
        {
            menuPause.SetActive(false);
            pausado = false;
            Time.timeScale = 1;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Fase1_ETM");
        menuPause.SetActive(false);
        pausado = false;
        Time.timeScale = 1;
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
        menuPause.SetActive(false);
        pausado = false;
        Time.timeScale = 1;
    }

    public void ExitGameOver()
    {
        SceneManager.LoadScene("Menu");
    }



}
