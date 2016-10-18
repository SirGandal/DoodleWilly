using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CustomSplashScreen : MonoBehaviour
{
	public float UpTimeSpeed = 1;
	public float WaitingTime = 3;
	public float DownTimeSpeed = 1;

	public GameObject splash;

	IEnumerator Start()
	{
		Color c = Color.white;

		splash.GetComponent<Image>().color = c;

		while (c.a < 1)
		{
			c.a += Time.deltaTime * UpTimeSpeed;
			splash.GetComponent<Renderer>().material.color = c;
			yield return null;
		}
		yield return new WaitForSeconds(WaitingTime);
		while (c.a > 0)
		{
			c.a -= Time.deltaTime * DownTimeSpeed;
			splash.GetComponent<Image>().color = c;
			yield return null;
		}
		Application.LoadLevel("Game");
	}

}