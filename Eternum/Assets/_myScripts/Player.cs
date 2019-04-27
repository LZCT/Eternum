using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject gameOver;
	public GameObject MenuPauseCompleto;
    public bool noChao;
	public GameObject sangue;
	
	// Variaveis - Movimentação
    public float forcaPulo;
    public float velocidadeMaxima;
	
    
	// Variaveis - Vida
	public int numVidas = 3;
    public int score = 0;
	public int coins = 0;
    public Text txtVidas;
    public Text txtScore;
	public Text txtCoins;
    public bool morto;
	
    // Start is called before the first frame update
    void Start()
    {
        txtScore.text = score.ToString();
        txtVidas.text = numVidas.ToString();
		txtCoins.text = coins.ToString();
        morto = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (!morto && !MenuPauseCompleto.activeInHierarchy)
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
			if(Input.GetKeyDown(KeyCode.Space) && noChao){
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
			if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.K)){
				GetComponent<Animator>().SetTrigger("Atacando");
				Collider2D[] colliders = new Collider2D[3];
				if(GetComponent<SpriteRenderer>().flipX == true){
					transform.Find("EspadaArea_L").gameObject.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliders);	
				}else
					transform.Find("EspadaArea_R").gameObject.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliders);	
				
				for(int i=0; i < colliders.Length; i++){
							if(colliders[i]!=null && colliders[i].gameObject.CompareTag("Enemies")){
								Instantiate(sangue, transform.position, Quaternion.identity);
								Destroy(colliders[i].gameObject);
								score += 100;
								txtScore.text = score.ToString();
								
							}
				}
			}
			
			
		}
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
		// Verifica se está no chão
        if (collision2D.gameObject.CompareTag("Chao"))
            noChao = true;
		// Reduz Vida na Colisão com Inimigos
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
		// Mata o personagem em itens de InstaDeath
        if (collision2D.gameObject.CompareTag("InstaDeath"))
             instaDeath();
    }

    void OnCollisionExit2D(Collision2D collision2D)
    {
		// Verifica se está no chão
        if (collision2D.gameObject.CompareTag("Chao"))
            noChao = false;
    }
	
	void OnTriggerEnter2D(Collider2D collider2D){
		// Coleta Moedas
		if (collider2D.gameObject.CompareTag("Coletavel")){
			Destroy(collider2D.gameObject);
			score += 25;
			coins++;
			txtScore.text = score.ToString();
			txtCoins.text = coins.ToString();
		}
		// Mata o personagem em itens de InstaDeath
		if (collider2D.gameObject.CompareTag("InstaDeath"))
            instaDeath();
        
	}
	
	// Função para matar um personagem instantâneamente
	void instaDeath(){
		numVidas = 0;
        txtVidas.text = numVidas.ToString();
        GetComponent<Animator>().SetTrigger("Morreu");
        morto = true;
        gameOver.SetActive(true);
	}	
}





