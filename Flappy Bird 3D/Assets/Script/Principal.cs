using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Principal : MonoBehaviour {

	public GameObject cerca;
	public GameObject arbusto;
	public GameObject nuvem;
	public GameObject pedra;
	public GameObject canos;
	public GameObject jogadorFelpudo;
	public Text scoreText;
	public Text startText;
	public Text startTextJP;
	public Text highScoreText; 
	public GameObject objectCanvasScore;
	public GameObject objectCanvasHighscore;
	public GameObject particulasPeninhas;

	public AudioClip somVoa;
	public AudioClip somScore;
	public AudioClip somBate;

	bool comecou;
	bool acabou;
	private int score, highscore;
	private string sceneName;

	void Start () { 
		Physics.gravity = new Vector3(0, -20.0F, 0); // aumenta a  gravidade da cena

		scoreText.text = "0";
		objectCanvasScore.transform.position = new Vector2(Screen.width / 6 - 46, Screen.height - 100);
		objectCanvasHighscore.transform.position = new Vector2(Screen.width / 6 - 46, Screen.height - 50);

		//Colocando o highscore para ser salvo
		sceneName = SceneManager.GetActiveScene().name;
		if (PlayerPrefs.HasKey(sceneName + "score"))
		{
			highscore = PlayerPrefs.GetInt(sceneName + "score");
			highScoreText.text = highscore.ToString();
		}

		startText.enabled = true;
		startText.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
		startText.text = "Toque para iniciar!\nTap to start!";
		startText.fontSize = 45;

		startTextJP.enabled = true;
		startTextJP.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
		startTextJP.text = "\n\nタップしてスタート！";
		startTextJP.fontSize = 45;
	}
	

	void Update(){
		if(Input.anyKeyDown) // se apertar 
		{ 
			if(!acabou) // se ainda nao terminou
			{
				if(!comecou) // se ainda nao começou
				{
					startText.enabled = false;
					startTextJP.enabled = false;
					comecou = true;
					InvokeRepeating("CriaCerca", 1, 0.15f); // repete a criaçao da cerca,espera 1s e 1decimo de s cria
					InvokeRepeating("CriaObjeto", 1, 0.75f); 
					jogadorFelpudo.GetComponent<Rigidbody>().useGravity = true;
					jogadorFelpudo.GetComponent<Rigidbody>().isKinematic = false;
					scoreText.text = score.ToString();
					//meuScore.fontSize = 100;
				}
				VoaFelpudo(); 
			}
		} 
		jogadorFelpudo.transform.rotation = Quaternion.Euler(jogadorFelpudo.GetComponent<Rigidbody>().velocity.y*-3, 0,0);
	}

	void CriaCerca()
	{
		if(!acabou){
			GameObject novoObjeto = (GameObject) Instantiate(cerca);  
		}
	} 

	void CriaObjeto()
	{
		if(!acabou){ 

			var sorteiaObjeto = Random.Range(1,5); //aleatorio de 1 a 5

            //mudar essas posiçoes pois estao indo fora do piso ou  aumentar o piso e a escala dos obj
            //precisa olhar onde o ogj esta na cena e no prefab 
			float posicaoXRandom = Random.Range(-1f,1f); //como o nome
			float posicaoYRandom = Random.Range(0.72f,4f); 
            //zerar todas as posiçoes para dar certo no caso desse projeto
			float rotacaoYRandom = Random.Range(0.0f,360.0f); 
 
			GameObject novoObjeto = new GameObject();
            GameObject novoObjetoNuvem = new GameObject();

            switch (sorteiaObjeto) 
			{ 
			case 1: novoObjeto = (GameObject) Instantiate(pedra);  posicaoYRandom=0; 
				break;

			case 2:  novoObjeto = (GameObject) Instantiate(arbusto); posicaoYRandom=0; 
				break;

			case 3:  novoObjetoNuvem  = (GameObject)  Instantiate(nuvem);  
				break;
			case 4:CriaCanos();  
				break;
			default: break;
			}
            //aqui que a posiçao e a rotaçao e modificada
            novoObjetoNuvem.transform.position = new Vector3(novoObjeto.transform.position.x + posicaoXRandom, novoObjeto.transform.position.y+posicaoYRandom,novoObjeto.transform.position.z);
            novoObjeto.transform.position = new Vector3(novoObjeto.transform.position.x + posicaoXRandom, novoObjeto.transform.position.y, novoObjeto.transform.position.z);
            novoObjeto.transform.rotation =  Quaternion.Euler(novoObjeto.transform.rotation.x,rotacaoYRandom,novoObjeto.transform.rotation.z);
            novoObjetoNuvem.transform.rotation = Quaternion.Euler(novoObjeto.transform.rotation.x, rotacaoYRandom, novoObjeto.transform.rotation.z);
        }
	}

	void CriaCanos()
	{
		if(!acabou){
			GameObject novoObjeto = (GameObject) Instantiate(canos);
			float posicaoYRandom = Random.Range(-1f,1.5f);
			novoObjeto.transform.position = new Vector3(novoObjeto.transform.position.x,posicaoYRandom,novoObjeto.transform.position.z);
			 
		}
	}

	void VoaFelpudo(){
		GetComponent<AudioSource>().PlayOneShot(somVoa);

		GameObject minhasParticulas = Instantiate(particulasPeninhas);
		minhasParticulas.transform.position = jogadorFelpudo.transform.position; // vai estar na posi do jogador
		minhasParticulas.transform.rotation = jogadorFelpudo.transform.rotation;
        
		jogadorFelpudo.GetComponent<Rigidbody>().velocity = Vector3.zero; // zera
		jogadorFelpudo.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 6.0f, 0.0f); // para cima
	}

	void MarcaPonto(){
		GetComponent<AudioSource>().PlayOneShot(somScore);
		score++;
		scoreText.text = score.ToString();

		if (score > highscore)
		{
			highscore = score;
			highScoreText.text = highscore.ToString();
			PlayerPrefs.SetInt(sceneName + "score", highscore);
		}
	}
	 
	void FimDeJogo(){ 
		acabou = true;
		GetComponent<AudioSource>().PlayOneShot(somBate);
		Invoke("RecarregaCena", 2);
	}
	void RecarregaCena(){ 
		Application.LoadLevel("aviao3D");
	}
    
}
