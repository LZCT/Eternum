using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
	public GameObject save;
	public GameObject noSaveGame;
	public GameObject newGame;
	
    public void NewGameButton()
    {
		if(PlayerPrefs.HasKey("Level")){
			newGame.SetActive(true);
			Debug.Log("entro");
		}else{
			SceneManager.LoadScene("Fase1_Story");
			save.SendMessage("delete");
		}
        
    }
	
	public void LoadGame()
    {
		if(PlayerPrefs.HasKey("Level")){
			save.SendMessage("load");
		}else
			noSaveGame.SetActive(true);
		
    }

    public void Quit()
    {
        Application.Quit();

    }
	
	public void Ok(){
		noSaveGame.SetActive(false);
    }
	
	public void Yes(){
		SceneManager.LoadScene("Fase1_Story");
		save.SendMessage("delete");
    }
	
	public void No(){
		newGame.SetActive(false);
    }

}
