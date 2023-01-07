using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour {

	public GameObject player;
	public Image pimpZilla;
	public Text EndGameText;
	public Text score;
	public Text kill;
	public Text size;
	public Text credits;

	private Image backgroundImage;
	private Image pimpZillaImage;
	private Text textColorChanger;
	Color c;
	Color c2;
	Color c3;
	public void showEndGame(bool won){

		gameObject.SetActive (true);
		backgroundImage = gameObject.GetComponent<Image> ();
		pimpZillaImage = pimpZilla;
		c = backgroundImage.color;
		c3 = pimpZillaImage.color;
		c.a = 0;
		c3.a = 0;
		backgroundImage.color = c;
		pimpZillaImage.color = c3;
		//gameObject.GetComponent<Image> ().color.a = 0;
		StartCoroutine ("fadeInColor");


		if (won) {
			EndGameText.text = "You Win, good job";
		}

		this.score.text = "Score: " + player.GetComponent<PlayerContoller>().score.ToString();
		this.kill.text ="Kills: " +  player.GetComponent<PlayerContoller>().killCount.ToString();
		this.size.text = "Size: " +  player.GetComponent<PlayerContoller>().playerSize.ToString();

	}

	IEnumerator fadeInColor(){
		float i = 0;
		while (i < 1) {
			yield return new WaitForSeconds(0.1f);
			i += 0.1f;
			c.a = i;
			backgroundImage.color = c;

			c3.a = i;
			pimpZillaImage.color = c3;

			//gameObject.GetComponent<Image> ().color.a = i;
			c2 = score.color;
			c2.a = i;

			textColorChanger = score;
			textColorChanger.color = c2;

			textColorChanger = kill;
			textColorChanger.color = c2;

			textColorChanger = size;
			textColorChanger.color = c2;

			textColorChanger = EndGameText;
			textColorChanger.color = c2;

			textColorChanger = credits;
			textColorChanger.color = c2;
		}
	}
}
