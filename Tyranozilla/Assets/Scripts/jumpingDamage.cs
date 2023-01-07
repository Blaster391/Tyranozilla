using UnityEngine;
using System.Collections;

public class jumpingDamage : MonoBehaviour {

	public int attackDamage;
	

	// Use this for initialization
	void Start () {
		attackDamage = (int) GameObject.Find ("Player").GetComponent<PlayerContoller>().playerSize * attackDamage;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		
		if (coll.gameObject.tag == "Enemy") {
			coll.gameObject.GetComponent<Enemy> ().takeDamage (attackDamage);
		}
	}
}
