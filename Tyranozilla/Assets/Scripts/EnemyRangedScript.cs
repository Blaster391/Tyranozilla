using UnityEngine;
using System.Collections;

public class EnemyRangedScript : MonoBehaviour {

	public int attackDamage;
	public float speed;
	
	// Use this for initialization
	void Start () {
		

		Vector2 fireballDirection = new Vector2 (GameObject.Find ("Player").transform.position.x -gameObject.transform.position.x, GameObject.Find ("Player").transform.position.y - gameObject.transform.position.y);
		//Vector(x2-x1,y2-y1)
		
		GetComponent<Rigidbody2D> ().AddForce (fireballDirection*speed);
	}
	
	void Update(){
		Vector2 dir = transform.GetComponent<Rigidbody2D>().velocity;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		
		if(!(coll.gameObject.tag == "Enemy")){
			if (coll.gameObject.name == "Player") {
				coll.gameObject.GetComponent<PlayerContoller> ().takeDamage(attackDamage);
				GameObject.Destroy (gameObject);
			}
			else if(!(coll.gameObject.tag == "Player")){
				GameObject.Destroy (gameObject);
			}
		}
	}
}
