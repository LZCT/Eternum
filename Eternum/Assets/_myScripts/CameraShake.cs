using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	Vector3 cameraInitialPosition;
	public float shakeMagnitude = 0.05f, shakeTime = 0.5f;
	public Camera mainCamera;
	
	public void ShakeIt(){
		cameraInitialPosition = mainCamera.transform.position;
		InvokeRepeating("StartCameraShaking", 0f, 0.005f);
		Invoke("StopCameraShaking", shakeTime);
	}
	
	void StartCameraShaking(){
		
		float cameraShakingOffsetX = Random.value * shakeMagnitude * 2 - shakeMagnitude;
		float cameraShakingOffsetY = Random.value * shakeMagnitude * 2 - shakeMagnitude;
		Vector3 cameraIntermidiatePosition = mainCamera.transform.position;
		cameraIntermidiatePosition.x += cameraShakingOffsetX;
		cameraIntermidiatePosition.y += cameraShakingOffsetY;
		mainCamera.transform.position = cameraIntermidiatePosition;
	}
	
	void StopCameraShaking(){
		CancelInvoke("StartCameraShaking");
		mainCamera.transform.position = cameraInitialPosition;
	}
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
