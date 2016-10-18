using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArrowAnimation : MonoBehaviour
{
	public Sprite one;
	public Sprite two;
	public Sprite three;
	private int counter = 0;

	void Start()
	{
		StartCoroutine("AnimateArrow");
	}

	IEnumerator AnimateArrow()
	{
		yield return new WaitForSeconds(0.08f);
		switch (counter)
		{
			case 0:
				this.gameObject.GetComponent<Image>().sprite = one;
				counter++;
				break;
			case 1:
				this.gameObject.GetComponent<Image>().sprite = two;
				counter++;
				break;
			case 2:
				this.gameObject.GetComponent<Image>().sprite = three;
				counter = 0;
				break;
			default:
				break;
		}
		
		StartCoroutine("AnimateArrow");
	}
}
