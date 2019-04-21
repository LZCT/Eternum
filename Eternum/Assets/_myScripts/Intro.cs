using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
	 public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component
      
    // Start is called before the first frame update
    void Start()
    {
         VideoPlayer.loopPointReached += LoadScene;
    }

    // Update is called once per frame
    void Update()
    {

    }
	void LoadScene(VideoPlayer vp)
     {
          SceneManager.LoadScene("Menu");
      }
}


