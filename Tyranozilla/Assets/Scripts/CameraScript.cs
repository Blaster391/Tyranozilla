using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3( player.transform.position.x, player.transform.position.y, -10 );
	}
}
