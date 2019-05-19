using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject gameOver;
	public GameObject save;
	public GameObject MenuPauseCompleto;
	public Image imageCooldown;
    public bool noChao;
	public GameObject sangue;
	public int level;
	public float cooldownTime = 0.5f;
	public Transform targetBoss;
	
	
	// Variavel Som
	private AudioSource[] soundsPlayer;
	
	// Variavel Cooldown
	private bool cooldown = false;
	
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
		soundsPlayer = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
		if (!morto && !MenuPauseCompleto.activeInHierarchy)
        {       
			if(cooldown){
				imageCooldown.fillAmount += 1 / cooldownTime * Time.deltaTime;
			}
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
			
			//Taunt
			if(Input.GetKeyDown(KeyCode.T)){
				soundsPlayer[6].Play();
			}
			
			if(Input.GetKeyDown(KeyCode.R)){
				//rigBody.velocity = new Vector2(0f, 0f);
				//rigBody.AddForce( new Vector2(500,0), ForceMode2D.Impulse);
			}
			
			// Ataque
			if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.K)){
				if ( cooldown == false ){
					GetComponent<Animator>().SetTrigger("Atacando");
					soundsPlayer[0].Play();
					Collider2D[] colliders = new Collider2D[3];
					
					if(GetComponent<SpriteRenderer>().flipX == true)
						transform.Find("EspadaArea_L").gameObject.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliders);	
					else
						transform.Find("EspadaArea_R").gameObject.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliders);	
					
					for(int i=0; i < colliders.Length; i++){
								if(colliders[i]!=null && colliders[i].gameObject.CompareTag("Enemies")){
									soundsPlayer[5].Play();
									Instantiate(sangue, transform.position, Quaternion.identity);
									Destroy(colliders[i].gameObject);
									score += 100;
									txtScore.text = score.ToString();
									
								}
								if(colliders[i]!=null && colliders[i].gameObject.CompareTag("Boss")){
									soundsPlayer[5].Play();
									//Instantiate(sangue, transform.position, Quaternion.identity);
									targetBoss.SendMessage("TakeDamageBoss");
									

								}
					}
						Invoke("ResetCooldown",cooldownTime);
						cooldown = true;
				}
					
			}
			
			
		}
    }
	// Cooldown Attack
	void ResetCooldown(){
		cooldown = false;
		imageCooldown.fillAmount = 0;
	}
	
	void final(){
		 SceneManager.LoadScene("Fase4_Story");
	}
	
	void dieFinal(){
		Instantiate(sangue, transform.position, Quaternion.identity);
        GetComponent<Animator>().SetTrigger("Morreu");
		Invoke("final",10.0f);
	}
	
	// Boss Morto
	void BossMorto(){
		morto = true;
		save.SendMessage("saveHighScore");
		
		Invoke("dieFinal",5.0f);
		
	}
	
	// Dano do Boss
	public void TakeDamage(){
		ShakeIt();
        if(numVidas > 0){
			//Rigidbody2D rigBody = GetComponent<Rigidbody2D>();
			//rigBody.velocity = new Vector2(50f, 0f);
			//rigBody.AddForce( new Vector2(300,10), ForceMode2D.Impulse);
			Instantiate(sangue, transform.position, Quaternion.identity);
			soundsPlayer[4].Play();
            numVidas--;
            txtVidas.text = numVidas.ToString();
			GetComponent<Animator>().SetTrigger("Hit");
			Debug.Log("HIT");
			if (numVidas == 0){
				Instantiate(sangue, transform.position, Quaternion.identity);
                GetComponent<Animator>().SetTrigger("Morreu");
                morto = true;
				gameOver.SetActive(true);
				soundsPlayer[2].Stop();
				soundsPlayer[3].Play();
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
			ShakeIt();
            if(numVidas > 0)
            {
				soundsPlayer[4].Play();
                numVidas--;
                txtVidas.text = numVidas.ToString();
				GetComponent<Animator>().SetTrigger("Hit");
                if (numVidas == 0)
                {
					Instantiate(sangue, transform.position, Quaternion.identity);
                    GetComponent<Animator>().SetTrigger("Morreu");
                    morto = true;
                    gameOver.SetActive(true);
					soundsPlayer[2].Stop();
					soundsPlayer[3].Play();

                }
            }
        }
		
		
		// Mata o personagem em itens de InstaDeath
        if (collision2D.gameObject.CompareTag("InstaDeath"))
             instaDeath();
		 if (collision2D.gameObject.CompareTag("Portal")){
			save.SendMessage("save", level+1);
			if(level == 1)
				save.SendMessage("saveScoreValueL1", score);
			else
				if(level == 2){
					save.SendMessage("saveScoreValueL2", score);
					//save.SendMessage("saveHighScore");
				}
					
			if(level < 3)
				SceneManager.LoadScene("Fase" + (level+1) + "_Story");
			
			
		 }
            
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
			soundsPlayer[1].Play();
		}
		// Mata o personagem em itens de InstaDeath
		if (collider2D.gameObject.CompareTag("InstaDeath"))
            instaDeath();
		        
	}
	
	// Função para matar um personagem instantâneamente
	void instaDeath(){
		Instantiate(sangue, transform.position, Quaternion.identity);
		ShakeIt();
		numVidas = 0;
        txtVidas.text = numVidas.ToString();
        GetComponent<Animator>().SetTrigger("Morreu");
        morto = true;
        gameOver.SetActive(true);
		gameOver.GetComponent<AudioSource>().Play();
		soundsPlayer[2].Stop();
		soundsPlayer[3].Play();
		soundsPlayer[5].Play();
	}	
	
	
	// Camera
	
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
}





