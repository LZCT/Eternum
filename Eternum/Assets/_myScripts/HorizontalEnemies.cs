using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorizontalEnemies : MonoBehaviour
{
	//private bool colisao = false;
	public float moveSpeed = 2f;
	Transform Detector_L, Detector_R;
	Vector3 localScale;
	bool movingRight = true;
	Rigidbody2D rb;
	public string idEnemy = "";
	
	
    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
		rb = GetComponent<Rigidbody2D>();
		Detector_L = GameObject.Find("Detector_L_" + idEnemy).GetComponent<Transform>();
		Detector_R = GameObject.Find("Detector_R_" + idEnemy).GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > Detector_R.position.x)
			movingRight = false;
		if(transform.position.x < Detector_L.position.x)
			movingRight = true;
		
		if(movingRight)
			moveRight();
		else
			moveLeft();
    }
	
	void moveRight(){
		movingRight = true;
		if(localScale.x < 0)
			localScale.x = -localScale.x;
		transform.localScale = localScale;
		rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
	}
	
	void moveLeft(){
		movingRight = false;
		if(localScale.x > 0)
			localScale.x = -localScale.x;
		transform.localScale = localScale;
		rb.velocity = new Vector2(-1 * moveSpeed, rb.velocity.y);
	}
	
	
	
	
		
}

