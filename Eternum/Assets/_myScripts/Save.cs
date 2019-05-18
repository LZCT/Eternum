using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
	
	public Text txtScore;
	int levelValue = 1;
	int scoreValueL1 = 0;
	int scoreValueL2 = 0;
	int highSCoreValue = 0;
	int totalScore = 0;
	
    // Start is called before the first frame update
    void Start()
    {
        loadHighScore();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
	
	public void save(int lvl){
		levelValue = lvl;
		 PlayerPrefs.SetInt("Level", levelValue);
	}
	
	public void saveHighScore(){
		scoreValueL1 = PlayerPrefs.GetInt("scoreValueL1",0);
		Debug.Log("LoadL1: " + scoreValueL1);
		scoreValueL2 = PlayerPrefs.GetInt("scoreValueL2",0);
		Debug.Log("LoadL2: " + scoreValueL2);
		totalScore = scoreValueL1 + scoreValueL2 + 5000;
		if(highSCoreValue < totalScore)
			highSCoreValue = totalScore;
		PlayerPrefs.SetInt("highSCoreValue", highSCoreValue);
		Debug.Log("HighScoreLoad: " + highSCoreValue);
	}
	
	public void loadHighScore(){
		highSCoreValue = PlayerPrefs.GetInt("highSCoreValue",0);
		if(PlayerPrefs.HasKey("highSCoreValue"))
			txtScore.text = highSCoreValue.ToString();
		else
			txtScore.text = "No Score";		
		Debug.Log("HIGH:" + highSCoreValue);
	}
	
	public void saveScoreValueL1(int scoreL1){
		scoreValueL1 = scoreL1;
		PlayerPrefs.SetInt("scoreValueL1", scoreValueL1);
		Debug.Log("scoreValueL1: " + scoreValueL1);
	}
	
	public void saveScoreValueL2(int scoreL2){
		scoreValueL2 = scoreL2;
		PlayerPrefs.SetInt("scoreValueL2", scoreValueL2);
		Debug.Log("scoreValueL2: " + scoreValueL2);
	}
	
	
	public void load(){
		levelValue = PlayerPrefs.GetInt("Level",0);
		if(PlayerPrefs.HasKey("Level")){
			
			SceneManager.LoadScene("Fase" + levelValue + "_Story");
		}
			
			
	}
	
	public void delete(){
		PlayerPrefs.DeleteKey("Level");
		PlayerPrefs.DeleteKey("scoreValueL1");
		PlayerPrefs.DeleteKey("scoreValueL2");
	}
}
