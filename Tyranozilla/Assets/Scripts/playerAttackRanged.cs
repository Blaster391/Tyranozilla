using UnityEngine;
using System.Collections;

public class playerAttackRanged : MonoBehaviour {

	public int attackDamage;
	public float speed;

	// Use this for initialization
	void Start () {

		attackDamage = (int) GameObject.Find ("Player").GetComponent<PlayerContoller>().playerSize * attackDamage;

		gameObject.transform.localScale = gameObject.transform.localScale * (GameObject.Find ("Player").GetComponent<PlayerContoller> ().playerSize * 1.5f);

		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 fireballDirection = new Vector2 (mousePos.x -gameObject.transform.position.x, mousePos.y - gameObject.transform.position.y);
		//Vector(x2-x1,y2-y1)

		GetComponent<Rigidbody2D> ().AddForce (fireballDirection*speed);
	}

	void Update(){
		Vector2 dir = transform.GetComponent<Rigidbody2D>().velocity;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

	}
	
	void OnTriggerEnter2D(Collider2D coll) {

		if(!(coll.gameObject.tag == "Player")){
			if (coll.gameObject.tag == "Enemy") {
				coll.gameObject.GetComponent<Enemy> ().takeDamage (attackDamage);
			}
			GameObject.Destroy (gameObject);
		}
	}
}
