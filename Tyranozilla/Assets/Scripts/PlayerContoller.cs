using UnityEngine;
using System.Collections;

public class PlayerContoller : MonoBehaviour {

	public AudioClip johnCenaTheme;
	public AudioClip jurrasicTheme;

	public int maxNoOfEnemies;
	public int noOfEnemies;

	public int cenasKilled;
	public GameObject johnCena;
	public GameObject megaJohnCena;

	public GameObject sparkles;

	public int maxHealth;
	public int health;
	public int killCount;
	public int score;

	public float moveSpeed;
	public float maxSpeed;
	public float jumpHeight;
	public float attackRangeMelee;
	public float attackRange;

	public float attackSpeedMelee;
	public float attackSpeedRanged;

	public float playerSize;

	public GameObject meleeAttackObject;
	public GameObject rangedAttackObject;

	public GameObject deathScreen;

	private bool isAttacking = false;

	private Vector3 startingSize;
	private float cameraSize;

	private int startingMaxHealth;
	private float startingSpeed;
	private float startingJumpHeight;
	private float startingMaxSpeed;
	private float startingAttackSpeedMelee;

	// Use this for initialization
	void Start () {
	
		startingMaxHealth = maxHealth;
		startingSpeed = moveSpeed;
		startingMaxSpeed = maxSpeed;
		startingJumpHeight = jumpHeight;


		startingSize = transform.localScale;
		cameraSize = GameObject.Find ("Camera").GetComponent<Camera> ().orthographicSize;
		setSize ();

	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetButtonDown("Attack") && !isAttacking){
			meleeAttack();
		}
		if (Input.GetButtonDown ("RangedMode") && !isAttacking) {
			rangedAttack ();
		}


		if (Input.GetButton ("MoveRight")) {
			if(GetComponent<Rigidbody2D>().velocity.magnitude < maxSpeed || GetComponent<Rigidbody2D>().velocity.x < 0){
				GetComponent<Rigidbody2D>().AddForce(new Vector2(moveSpeed * Time.deltaTime,0));
			}
			gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
		}
		if (Input.GetButton ("MoveLeft")) {
			if(GetComponent<Rigidbody2D>().velocity.magnitude < maxSpeed || GetComponent<Rigidbody2D>().velocity.x > 0){
				GetComponent<Rigidbody2D>().AddForce(new Vector2(-moveSpeed * Time.deltaTime,0));
			}
			gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
		}
		if (Input.GetButtonDown ("Jump")) {
			GetComponent<AudioSource> ().Play ();
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0,jumpHeight));
			StartCoroutine("sparklesCo");
		}
		if (Input.GetButtonDown ("JumpDown")) {
			GetComponent<AudioSource> ().Play ();
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0,-jumpHeight*0.7f));
			StartCoroutine("sparklesCo");
		}
		if (Input.GetButtonDown ("Quit")) {
			Application.Quit ();
		}
		if (Input.GetButtonDown ("Respawn")) {
			Application.LoadLevel (Application.loadedLevel);
		}

	}

	public void addScore(int score){
		this.score += score;
	}

	public void incrementKills(){
		killCount++;
		noOfEnemies--;
		grow ();
	}

	public void addHealth(int amount){
		health += amount;
		if (health > maxHealth) {
			health = maxHealth;
		}
	}

	private void meleeAttack(){
		StartCoroutine ("meleeAttackCo");
	}

	private IEnumerator meleeAttackCo(){
		isAttacking = true;
		meleeAttackObject.GetComponent<AudioSource> ().Play ();
		meleeAttackObject.GetComponent<SpriteRenderer> ().enabled = true;
		meleeAttackObject.GetComponent<BoxCollider2D> ().enabled = true;

		yield return new WaitForSeconds(attackSpeedMelee/2);

		meleeAttackObject.GetComponent<SpriteRenderer> ().enabled = false;
		meleeAttackObject.GetComponent<BoxCollider2D> ().enabled = false;

		yield return new WaitForSeconds(attackSpeedMelee/2);
		isAttacking = false;
	}

	private void rangedAttack(){
		StartCoroutine ("rangedAttackCo");

	}

	private IEnumerator rangedAttackCo(){
		isAttacking = true;
		Instantiate(rangedAttackObject, GameObject.Find ("fireballSpawn").transform.position, new Quaternion(0,0,0,0)); 
		yield return new WaitForSeconds(attackSpeedRanged);
		isAttacking = false;
	}

	public void takeDamage(int amount){
		health -= amount;
		if (health <= 0) {
			death ();
		}
	}

	IEnumerator cenaTime(){
		float cenaRespawnTime = 30;
		int i = 0;
		GameObject.Find ("Camera").GetComponent<AudioSource> ().clip = johnCenaTheme;
		GameObject.Find ("Camera").GetComponent<AudioSource> ().Play ();
		while (true) {
			spawnCena ();
			yield return new WaitForSeconds(cenaRespawnTime);
			i++;
			if(cenaRespawnTime > 10){
				cenaRespawnTime -=5;
			}
			if(playerSize > 50){
				cenaRespawnTime = 4;
			}
			if(playerSize > 80){
				cenaRespawnTime = 2;
			}
			if(playerSize > 100){
				cenaRespawnTime = 1;
			}
			if(playerSize > 150){
				cenaRespawnTime = 0.5f;
			}
			if(playerSize > 200){
				cenaRespawnTime = 0.1f;
			}
		}
	}

	private void spawnCena(){
		if (playerSize < 30) {
			Instantiate (johnCena, new Vector3 (transform.position.x, transform.position.y + 200, transform.position.z), new Quaternion (0, 0, 0, 0));
		} else {
			Instantiate (megaJohnCena, new Vector3 (transform.position.x, transform.position.y + 200, transform.position.z), new Quaternion (0, 0, 0, 0));
		}
	}

	public void cenaKilled(){
		playerSize++;
		cenasKilled++;
		setSize ();
	}

	private void grow(){

		if (killCount > playerSize * (3+playerSize)) {
			playerSize++;
		}

		setSize ();
	}

	private bool cenaStarted = false;

	private void setSize(){
		transform.localScale = startingSize * playerSize;
		GameObject.Find ("Camera").GetComponent<Camera> ().orthographicSize = (cameraSize * playerSize/1.1f);

		maxHealth = (int)(startingMaxHealth * playerSize);
		maxSpeed = (int)(startingMaxSpeed * (playerSize*0.65));
		moveSpeed = (int)(startingSpeed * (playerSize*0.65));
		jumpHeight = (int)(startingJumpHeight * (playerSize*0.65));

		if (playerSize >= 12 && cenaStarted == false) {
			Debug.Log ("Cena time!");
			StartCoroutine("cenaTime");
			cenaStarted = true;
		}
	
	}

	IEnumerator sparklesCo(){
		sparkles.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		sparkles.SetActive (false);
	}

	private bool ded = false;
	private void death(){
		if (!ded) {
			GameObject.Find ("Camera").GetComponent<AudioSource> ().clip = jurrasicTheme;
			GameObject.Find ("Camera").GetComponent<AudioSource> ().Play ();
			deathScreen.GetComponent<EndGameScript> ().showEndGame (false);
			deathScreen.SetActive (true);
			ded = true;
		}
	}
}
