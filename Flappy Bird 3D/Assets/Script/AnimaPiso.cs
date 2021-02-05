using UnityEngine;
using System.Collections;

public class AnimaPiso : MonoBehaviour {

	private float velociade = -0.75f; // da animaçao do piso
	public Material materialPiso;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float offset = Time.time * velociade; // adiciona deeis de q o jogo começa
		materialPiso.SetTextureOffset("_MainTex", new Vector2(offset, 0)); //nome, novo vetor
	}
}
