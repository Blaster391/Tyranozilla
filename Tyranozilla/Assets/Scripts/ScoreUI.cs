using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

	public Text health;
	public Text score;
	public Text kills;
	public Text size;
	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		health.text = "Health: " + player.GetComponent<PlayerContoller> ().health.ToString ();
		score.text = "Score: " + player.GetComponent<PlayerContoller> ().score.ToString ();
		kills.text = "Kills: " + player.GetComponent<PlayerContoller> ().killCount.ToString ();
		size.text = "Size: " + player.GetComponent<PlayerContoller> ().playerSize.ToString ();
	}
}
