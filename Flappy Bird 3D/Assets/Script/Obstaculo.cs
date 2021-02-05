using UnityEngine;
using System.Collections;

public class Obstaculo : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,10); // eu - começa nessa posiçao
		GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, -5.0f); //  andar
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position.z < -20){ // quebra se chegar aqui
			Destroy(this.gameObject);
		}
	}
}
