using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
	public GameObject save;
	
	
    public void NewGameButton()
    {
        SceneManager.LoadScene("Fase1_Story");
		save.SendMessage("delete");
    }
	
	public void LoadGame()
    {
        save.SendMessage("load");
		
    }

    public void Quit()
    {
        Application.Quit();

    }

}
