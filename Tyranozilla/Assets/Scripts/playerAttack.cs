using UnityEngine;
using System.Collections;

public class playerAttack : MonoBehaviour {

	public int startingAttackDamage;
	public int attackDamage;

	// Use this for initialization
	void Start () {
		startingAttackDamage = attackDamage;

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D coll) {

		if (coll.gameObject.tag == "Enemy") {
			attackDamage = (int) GameObject.Find ("Player").GetComponent<PlayerContoller>().playerSize * startingAttackDamage;
			coll.gameObject.GetComponent<Enemy> ().takeDamage (attackDamage);
		}
	}
}
