using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {

	public float spawnTimer;
	public float offset;
	public GameObject player;
	PlayerContoller pc;

	public GameObject smallEnemy;
	public GameObject mediumEnemy;
	public GameObject bigEnemy;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		pc = player.GetComponent<PlayerContoller> ();

		StartCoroutine ("spawnClock");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator spawnClock(){
	
		yield return new WaitForSeconds(offset);

		while (true) {
			if((pc.maxNoOfEnemies > pc.noOfEnemies) && (Vector3.Distance (transform.position, player.transform.position) > 50)){
				spawn ();
			}
			yield return new WaitForSeconds(spawnTimer);
		}
	}

	public void spawn(){
		if (pc.playerSize > 8) {
			Instantiate (bigEnemy, transform.position, new Quaternion(0,0,0,0));
		} else if (pc.playerSize > 3) {
			Instantiate (mediumEnemy, transform.position, new Quaternion(0,0,0,0));
		} else {
			Instantiate(smallEnemy, transform.position, new Quaternion(0,0,0,0));
		}
		pc.noOfEnemies++;
	}
}
