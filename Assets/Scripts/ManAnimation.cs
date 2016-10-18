using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManAnimation : MonoBehaviour
{
	
	public Sprite one;
	public Sprite two;
	public Sprite three;
	
	public Sprite manSpecial1;
	public Sprite manSpecial2;
	public Sprite manSpecial3;
	public Sprite manSpecial12;
	public Sprite manSpecial22;
	public Sprite manSpecial32;
	public Sprite manSpecial13;
	public Sprite manSpecial23;
	public Sprite manSpecial33;
	public Sprite manSpecial14;
	public Sprite manSpecial24;
	public Sprite manSpecial34;
	public Sprite manSpecial15;
	public Sprite manSpecial25;
	public Sprite manSpecial35;
	public int numberOfTexturesForAnimation = 5;
	private int counter = 0;
	private int textureCounter = 0;
	private float time;
	private bool firstTime = true;
	
	private bool specialPower = false;

	public void StartAnimation()
	{

		StartCoroutine("AnimateMan");
	}

	public void activateSpecialPower()
	{
		textureCounter = 0;
		specialPower = true;
	}

	public void deactivateSpecialPower()
	{
		specialPower = false;
		textureCounter = 0;
		firstTime = true;
	}

	public void lowerSpecialPower(float time)
	{
		if (specialPower)
		{
			this.time = time;
			if (firstTime)
			{
				firstTime = false;
				StartCoroutine("switchTexture");
			}
			
		}
	}

	IEnumerator switchTexture()
	{
		textureCounter++;
//		Debug.Log("counter:"+textureCounter);
		yield return new WaitForSeconds(time / numberOfTexturesForAnimation);
		if (specialPower)
		{
			StartCoroutine("switchTexture");
		}
	}

	IEnumerator AnimateMan()
	{
		yield return new WaitForSeconds(0.08f);
		switch (counter)
		{
			case 0:
				if (!specialPower)
				{
					this.gameObject.GetComponent<Image>().sprite = one;
				}
				else
				{
					switch (textureCounter)
					{
						case 0:
							this.gameObject.GetComponent<Image>().sprite = manSpecial1;
							break;
						case 1:
							this.gameObject.GetComponent<Image>().sprite = manSpecial1; 
							break;	
						case 2:
							this.gameObject.GetComponent<Image>().sprite = manSpecial12;
							break;
						case 3:
							this.gameObject.GetComponent<Image>().sprite = manSpecial13;
							break;
						case 4:
							this.gameObject.GetComponent<Image>().sprite = manSpecial14;
							break;
						case 5:
							this.gameObject.GetComponent<Image>().sprite = manSpecial15;
							break;
						default:
							break;
					}	
				}
				counter++;
				break;
			case 1:
				if (!specialPower)
				{
					this.gameObject.GetComponent<Image>().sprite = two;
				}
				else
				{
					switch (textureCounter)
					{
						case 0:
							this.gameObject.GetComponent<Image>().sprite = manSpecial2; 
							break;
						case 1:
							this.gameObject.GetComponent<Image>().sprite = manSpecial2; 
							break;	
						case 2:
							this.gameObject.GetComponent<Image>().sprite = manSpecial22;
							break;
						case 3:
							this.gameObject.GetComponent<Image>().sprite = manSpecial23;
							break;
						case 4:
							this.gameObject.GetComponent<Image>().sprite = manSpecial24;
							break;
						case 5:
							this.gameObject.GetComponent<Image>().sprite = manSpecial25;
							break;
						default:
							break;
					}
				}
				counter++;
				break;
			case 2:
				if (!specialPower)
				{
					this.gameObject.GetComponent<Image>().sprite = three;
				}
				else
				{
					switch (textureCounter)
					{
						case 0:
							this.gameObject.GetComponent<Image>().sprite = manSpecial3; 
							break;
						case 1:
							this.gameObject.GetComponent<Image>().sprite = manSpecial3; 
							break;	
						case 2:
							this.gameObject.GetComponent<Image>().sprite = manSpecial32;
							break;
						case 3:
							this.gameObject.GetComponent<Image>().sprite = manSpecial33;
							break;
						case 4:
							this.gameObject.GetComponent<Image>().sprite = manSpecial34;
							break;
						case 5:
							this.gameObject.GetComponent<Image>().sprite = manSpecial35;
							break;
						default:
							break;
					}
				}
				counter = 0;
				break;
			default:
				counter = 0;
				break;
		}
		
		StartCoroutine("AnimateMan");
	}
}
