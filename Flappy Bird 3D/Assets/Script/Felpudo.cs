using UnityEngine;
using System.Collections;

public class Felpudo : MonoBehaviour {

	public GameObject cameraPrincipal;
    public AudioClip somBate;

	void OnTriggerEnter(Collider objeto) // entrada
	{
		if(objeto.gameObject.tag == "Finish") // cano
		{   
		 GetComponent<Rigidbody>().velocity = Vector3.zero;
			GetComponent<Rigidbody>().velocity = new Vector3(0f, 15.0f, -15.0f);
			GetComponent<Rigidbody>().AddTorque(new Vector3(-100,-100,-100));
            GetComponent<AudioSource>().PlayOneShot(somBate);
            cameraPrincipal.SendMessage("FimDeJogo");

		}
	}

	void OnTriggerExit(Collider objeto) // saida
	{
		if(objeto.gameObject.tag == "GameController") // o meio
		{
			Destroy(objeto.gameObject); // para nao marcar duas vezes a pontuaçao
			cameraPrincipal.SendMessage("MarcaPonto");
		}
	}
}
