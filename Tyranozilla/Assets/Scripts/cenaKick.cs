using UnityEngine;
using System.Collections;

public class cenaKick : MonoBehaviour {

	public int attackDamage;

	void OnTriggerEnter2D(Collider2D coll) {
		
		if(!(coll.gameObject.tag == "Enemy")){
			if (coll.gameObject.name == "Player") {
				coll.gameObject.GetComponent<PlayerContoller> ().takeDamage(attackDamage);
			}
		}
		gameObject.SetActive (false);
	}
}
