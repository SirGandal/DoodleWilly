using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArrowAnimationSpecial : MonoBehaviour
{
	public Sprite one;
	public Sprite two;
	public Sprite three;
	public Sprite oneBlack;
	public Sprite twoBlack;
	public Sprite threeBlack;
	
	private int counter = 0;
	
	private bool hit = false;

	void Start()
	{
		StartCoroutine("AnimateArrow");
	}

	public void specialArrowHitten()
	{
		hit = true;
	}

	IEnumerator AnimateArrow()
	{
		yield return new WaitForSeconds(0.08f);
		switch (counter)
		{
			case 0:
				if (!hit)
				{
					this.GetComponent<Image>().sprite = one;
				}
				else
				{
					this.GetComponent<Image>().sprite = oneBlack;
				}
				counter++;
				break;
			case 1:
				if (!hit)
				{
					this.GetComponent<Image>().sprite = two;
				}
				else
				{
					this.GetComponent<Image>().sprite = twoBlack;
				}
				counter++;
				break;
			case 2:
				if (!hit)
				{
					this.GetComponent<Image>().sprite = three;
				}
				else
				{
					this.GetComponent<Image>().sprite = threeBlack;
				}
				counter = 0;
				break;
			default:
				break;
		}
		
		StartCoroutine("AnimateArrow");
	}
}
