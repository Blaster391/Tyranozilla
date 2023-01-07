using UnityEngine;
using System.Collections;

public class JohnCena : Enemy {
	public float kickSpeed;

	public GameObject kickhitbox;

	public Sprite CenaAttack;
	public Sprite CenaWalk1;
	public Sprite CenaWalk2;

	public override IEnumerator AIAttackRanged(){
		busy = true;
		
		yield return new WaitForEndOfFrame ();
		//Set Aiming Aimation
		
		
		yield return new WaitForSeconds(attackSpeed/2);
		gameObject.GetComponent<AudioSource> ().Play ();
		GetComponent<SpriteRenderer> ().sprite = CenaAttack;

		StartCoroutine ("cenaKickDirection");
		kickhitbox.SetActive (true);
		Vector2 fireballDirection = new Vector2 (GameObject.Find ("Player").transform.position.x -gameObject.transform.position.x, GameObject.Find ("Player").transform.position.y - gameObject.transform.position.y);
		
		GetComponent<Rigidbody2D> ().AddForce (fireballDirection*kickSpeed);

		yield return new WaitForSeconds(attackSpeed/2);

		StopCoroutine("cenaKickDirection");
		transform.rotation = new Quaternion(0,0,0,0);
		kickhitbox.SetActive (false);
		GetComponent<SpriteRenderer> ().sprite = CenaWalk1;

		yield return new WaitForSeconds(attackSpeed);

		busy = false;
	}

	IEnumerator cenaKickDirection(){
		while (true) {
			Vector2 dir = transform.GetComponent<Rigidbody2D> ().velocity;
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
			yield return new WaitForEndOfFrame();
		}

	}

	public override void death(){
		
		//Death Animation
		Player.GetComponent<PlayerContoller> ().addScore (killScore);
		Player.GetComponent<PlayerContoller> ().incrementKills ();
		Player.GetComponent<PlayerContoller> ().addHealth (healthGain);
		Player.GetComponent<PlayerContoller> ().cenaKilled ();
		GameObject.Destroy (gameObject);
		
	}
}
