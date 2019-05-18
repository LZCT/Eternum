using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {
	
	
	private Animator anim;
	Transform Detector_L, Detector_R;
	Vector3 localScale;
	public float moveSpeed;
	bool movingRight = true;
	Rigidbody2D rb;
	public Transform target;
	public GameObject sangueBoss;
	public GameObject fumaca;
	public Slider healthBar;
	public Text txtScore;
	public int score = 0;
	
	// Status Boss
	private bool outInDistanceDamage;
	private bool outInDistance;
	public float health;
	
	private float attackRange=4;
	private float attackRangeDamage= 3;
	private float countAttack = 0;
	private float cooldownTime = 5.0f;
	private bool BossState;
	private bool isDead;
	
	 private void Start()
    {
		txtScore.text = score.ToString();
		BossState = true;
		health = 50;
		isDead = false;
		localScale = transform.localScale;
        anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		outInDistance = false;
		outInDistanceDamage = false;
		Detector_L = GameObject.Find("Detector_L").GetComponent<Transform>();
		Detector_R = GameObject.Find("Detector_R").GetComponent<Transform>();
    }

	
	private void Update(){
		
		healthBar.value = health;
		
		
		if(BossState && !isDead){
			if(transform.position.x > Detector_R.position.x)
				movingRight = false;
			if(transform.position.x < Detector_L.position.x)
				movingRight = true;
			
			if(movingRight)
				moveRight();
			else
				moveLeft();
			
			float distanceToPlayer = Vector3.Distance(transform.position, target.position);
			
			if(distanceToPlayer < attackRange){
				if(!outInDistance){
					anim.SetTrigger("attack");
					countAttack++;
					// ADICIONAR IMPULSO AO HITAR O PLAYER
					// VERIFICAR SPRITES
					
					outInDistance = true;
					if(countAttack == 5){
						Instantiate(fumaca, transform.position, Quaternion.identity);
						anim.SetTrigger("sit");
						BossState = false;
						Invoke("ResetCooldown",cooldownTime);
					}
				}
			}else
					outInDistance = false;
			
			if(distanceToPlayer < attackRangeDamage){
				if(!outInDistanceDamage){
					anim.SetTrigger("attack");
					outInDistanceDamage = true;
					//target.GetComponent<Rigidbody2D>().AddForce( new Vector2(-70,20), ForceMode2D.Impulse);
					target.SendMessage("TakeDamage");
				}
			}else
					outInDistanceDamage = false;
		}
		
		
		
		
		
	}
	//transform.up* 500 + transform.right * 2000
	public void TakeDamageBoss(){
		if(!isDead && !BossState){
			Instantiate(sangueBoss, transform.position, Quaternion.identity);
			if(health > 0)
				health -= 3;
			else{
				anim.SetTrigger("death");
				//changeState();
				target.SendMessage("BossMorto");
				score += 5000;
				txtScore.text = score.ToString();
				Instantiate(fumaca, transform.position, Quaternion.identity);
				isDead = true;
			}
			Debug.Log("HITPLAYER");
		}
	}
	
	// Cooldown Attack
	void ResetCooldown(){
		countAttack = 0;
		anim.SetTrigger("wakeup");
		Invoke("changeState",1.4f);
	}
	
	void changeState(){
		BossState = true;
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
		rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
	}	
	
}
