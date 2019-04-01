using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public bool noChao;
    public float forcaPulo;
    public float velocidadeMaxima;
	//public Animator m_Anim;
    // Start is called before the first frame update
    void Start()
    {
       // m_Anim = this.transform.Find("Player_Knight").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movimento
        Rigidbody2D rigBody = GetComponent<Rigidbody2D>();
        float movimento = Input.GetAxis("Horizontal");
        rigBody.velocity = new Vector2(movimento*velocidadeMaxima, rigBody.velocity.y);
		if(movimento < 0)
			GetComponent<SpriteRenderer>().flipX = true;
		else
			if(movimento > 0)
					GetComponent<SpriteRenderer>().flipX = false;

		// Pulo + Animação
        if(Input.GetKeyDown(KeyCode.Space)){
            GetComponent<Animator>().SetBool("Pulando", true);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0,forcaPulo));	
        }else
			GetComponent<Animator>().SetBool("Pulando", false);
		
		// Andando Animação
		if(movimento > 0 || movimento < 0){
			GetComponent<Animator>().SetBool("Andando", true);
		}else
			GetComponent<Animator>().SetBool("Andando", false);
		
		// Ataque
		if(Input.GetKey(KeyCode.Mouse0)){
			GetComponent<Animator>().SetTrigger("Atacando");
			
		}	
			
	}
	
}
/*
void OnCollisionEnter2D(Collision2D collision2D){
	if( collision2D.gameObject.CompareTag("Chao"))
		noChao  = true;
}

void OnCollisionExit2D(Collision2D collision2D){
	if( collision2D.gameObject.CompareTag("Chao"))
		noChao  = false;
}
*/
