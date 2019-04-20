using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject gameOver;
    public bool noChao;
    public float forcaPulo;
    public float velocidadeMaxima;
    public int numVidas = 3;
    public int score = 0;

    public Text txtVidas;
    public Text txtScore;
    public bool morto;
	
    // Start is called before the first frame update
    void Start()
    {
        txtScore.text = score.ToString();
        txtVidas.text = numVidas.ToString();
        morto = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!morto)
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
        if(Input.GetKeyDown(KeyCode.Space) && noChao)        {
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

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Chao"))
            noChao = true;
        if (collision2D.gameObject.CompareTag("Enemies"))
        {
            if(numVidas > 0)
            {
                numVidas--;
                txtVidas.text = numVidas.ToString();
                if (numVidas == 0)
                {
                    GetComponent<Animator>().SetTrigger("Morreu");
                    morto = true;
                    gameOver.SetActive(true);

                }
            }

           
                
        }
        if (collision2D.gameObject.CompareTag("InstaDeath"))
        {
            numVidas = 0;
            txtVidas.text = numVidas.ToString();
            GetComponent<Animator>().SetTrigger("Morreu");
            morto = true;
            gameOver.SetActive(true);
        }    
    }

    void OnCollisionExit2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Chao"))
            noChao = false;
    }

}



