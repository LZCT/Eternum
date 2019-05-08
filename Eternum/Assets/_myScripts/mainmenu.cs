using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public void NewGameButton()
    {
        SceneManager.LoadScene("Fase1_Story");
    }

    public void Quit()
    {
        Application.Quit();

    }

}
