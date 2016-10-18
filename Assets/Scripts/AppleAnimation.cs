using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AppleAnimation : MonoBehaviour
{
	public Sprite one;
	public Sprite two;
	public Sprite three;
	public Sprite oneRed;
	public Sprite twoRed;
	public Sprite threeRed;
	public Sprite oneYellow;
	public Sprite twoYellow;
	public Sprite threeYellow;
	private int counter = 0;
	private int counterApple = 0;

	public void StartAnimation()
	{
		StartCoroutine("AnimateApple");
	}

	public void appleHitten()
	{
		counter = 0;
		counterApple++;
		if (counterApple == 3)
		{
			counterApple = 0;
		}
	}

	IEnumerator AnimateApple()
	{
		yield return new WaitForSeconds(0.08f);
		switch (counter)
		{
			case 0: 
				switch (counterApple)
				{
					case 0:
						this.gameObject.GetComponent<Image>().sprite = one;
						break;
					case 1:
						this.gameObject.GetComponent<Image>().sprite = oneRed;
						break;
					case 2:
						this.gameObject.GetComponent<Image>().sprite = oneYellow;
						break;
					default:
						break;
				}
				counter++;
				break;
			case 1: 
				switch (counterApple)
				{
					case 0:
						this.gameObject.GetComponent<Image>().sprite = two;
						break;
					case 1:
						this.gameObject.GetComponent<Image>().sprite = twoRed;
						break;
					case 2:
						this.gameObject.GetComponent<Image>().sprite = twoYellow;
						break;
					default:
						break;
				}
				counter++;
				break;
			case 2: 
				switch (counterApple)
				{
					case 0:
						this.gameObject.GetComponent<Image>().sprite = three;
						break;
					case 1:
						this.gameObject.GetComponent<Image>().sprite = threeRed;
						break;
					case 2:
						this.gameObject.GetComponent<Image>().sprite = threeYellow;
						break;
					default:
						break;
				}
				counter = 0;
				break;
			default:
				break;
		}
		
		StartCoroutine("AnimateApple");
	}
}
