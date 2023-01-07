using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int health;
	public int healthGain;
	public int attackDamage;
	public float alertRange;

	public GameObject bullet;
	
	public float range;
	public float attackSpeed;

	public float speed;
	public float maxSpeed;
	public float jumpHeight;

	public GameObject Player;

	public int killScore;

	private bool active = false;
	public bool busy = false;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		StartCoroutine ("AILoop");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator AILoop(){
		while (true) {
			yield return new WaitForEndOfFrame ();
			if (!busy) {
				if (active == false) {
					if (Vector3.Distance (transform.position, Player.transform.position) < Player.GetComponent<PlayerContoller> ().playerSize * alertRange) {
						active = true;
					}
				} 
				if(active){
					if (Vector3.Distance (transform.position, Player.transform.position) < range*(Player.GetComponent<PlayerContoller> ().playerSize/1.2)) {
						StartCoroutine("AIAttackRanged");
					}
					else{
						moveTowardsPlayer();
					}

					if (Vector3.Distance (transform.position, Player.transform.position) > Player.GetComponent<PlayerContoller> ().playerSize * alertRange) {
						active = false;
					}
				}
			}
		}
	}

	 public virtual IEnumerator AIAttackRanged(){
		busy = true;

		yield return new WaitForEndOfFrame ();
		//Set Aiming Aimation


		yield return new WaitForSeconds(attackSpeed);

		Instantiate(bullet, gameObject.transform.Find ("gunPos").transform.position, new Quaternion(0,0,0,0));

		busy = false;
	}

	private void moveTowardsPlayer(){
		if (Player.transform.position.x < gameObject.transform.position.x) {
			StartCoroutine("AIMoveLeft");
		} else {
			StartCoroutine("AIMoveRight");
		}
	}

	IEnumerator AIMoveLeft(){
		busy = true;
		//Set Moving Aimation
		gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
		yield return new WaitForEndOfFrame ();
		if (GetComponent<Rigidbody2D> ().velocity.magnitude < maxSpeed) {
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (-speed * Time.deltaTime, 0));
		}
		
		
		busy = false;
	}

	IEnumerator AIMoveRight(){

		busy = true;
		//Set Moving Aimation
		gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
		yield return new WaitForEndOfFrame ();
		if (GetComponent<Rigidbody2D> ().velocity.magnitude < maxSpeed) {
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (speed * Time.deltaTime, 0));
		}
		
		
		busy = false;

	}

	public void takeDamage(int amount){
		health -= amount;
		if (health < 0) {
			death();
		}
	}

	public virtual void death(){

		//Death Animation
		Player.GetComponent<PlayerContoller> ().addScore (killScore);
		Player.GetComponent<PlayerContoller> ().incrementKills ();
		Player.GetComponent<PlayerContoller> ().addHealth (healthGain);

		GameObject.Destroy (gameObject);

	}
}
