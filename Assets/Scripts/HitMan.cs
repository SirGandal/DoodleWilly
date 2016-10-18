using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HitMan : MonoBehaviour
{
	private bool hit = false;
	private bool end = false;
	private bool specialPower = false;

	private float toleranceFactorHit = 55f;
	private float specialPowerTime = 16f;

	private int specialPowerCounter = 0;
	private int checkTimeInvinvible = 40;
	private int countHitMan = 0;
	private int timeSinceNoHit = 0;

	private Text endOfGameText;
	public GameObject background;
	private GameObject arrow;

	private HitArrowSpecial hitArrowSpecial;
	private HitArrow hitArrow;
	private HitArrowBig hitArrowBig;
	private HitArrowGold hitArrowGold;
	private ManAnimation manAnimation;

	public Sprite background_normal;
	public Sprite background_ouch;

	public AudioSource audioSourceMan;
	public AudioClip ouch;
	public AudioClip crackWood;
	public AudioClip superPowerActivated;
	public AudioClip fire;

	private Color red = new Color(255f / 255f, 0f / 255f, 0 / 255f, 100f / 255f);

	void Start()
	{
		audioSourceMan.clip = fire;
		manAnimation = (ManAnimation)this.gameObject.GetComponent(typeof(ManAnimation));
		InvokeRepeating("checkInvincible", 0f, 1f);
	}

	public bool isSpecialPowerOn(){
		return this.specialPower;
	}

	public void checkInvincible()
	{
		timeSinceNoHit++;

		if (countHitMan != 0)
		{
			countHitMan = 0;
			timeSinceNoHit = 0;
		}
		else
		{
			if (timeSinceNoHit == checkTimeInvinvible)
			{
				countHitMan = 0;
				timeSinceNoHit = 0;
				Main.scoreManager.invincible();
			}
		}
	}

	public void activateSpecialPower()
	{
		GetComponent<AudioSource>().PlayOneShot(superPowerActivated);
		audioSourceMan.Play();
		specialPower = true;
		specialPowerCounter++;
		manAnimation.activateSpecialPower();
		manAnimation.lowerSpecialPower(specialPowerTime);
		StartCoroutine("disableSpecialPower");
	}

	IEnumerator disableSpecialPower()
	{
		yield return new WaitForSeconds(specialPowerTime);
		specialPowerCounter--;
		if (specialPowerCounter == 0)
		{
			specialPower = false;
			manAnimation.deactivateSpecialPower();
		}
	}

	IEnumerator backToNormalBackground()
	{
		yield return new WaitForSeconds(0.4f);
		background.GetComponent<Image>().sprite = background_normal;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		arrow = GameObject.Find(coll.gameObject.name);

		switch (arrow.tag)
		{
			case "normalArrow":
				if (specialPower)
				{
					hitArrow = (HitArrow)arrow.GetComponent(typeof(HitArrow));
					hitArrow.Hit();
				}
				else
				{
					hit = true;
				}
				break;
			case "specialArrow":
				Main.scoreManager.giveLife();
				break;
			case "goldArrow":
				if (specialPower)
				{
					hitArrowGold = (HitArrowGold)arrow.GetComponent(typeof(HitArrowGold));
					hitArrowGold.Hit();
				}
				else
				{
					hit = true;
				}
				break;
			case "bigArrow":
				if (specialPower)
				{
					hitArrowBig = (HitArrowBig)arrow.GetComponent(typeof(HitArrowBig));
					switch (hitArrowBig.numberOfTap)
					{
						case 3:
							hitArrowBig.Hit();
							hitArrowBig.Hit();
							hitArrowBig.Hit();
							break;
						case 2:
							hitArrowBig.Hit();
							hitArrowBig.Hit();
							break;
						case 1:
							hitArrowBig.Hit();
							break;
						default:
							break;	
					}
				}
				else
				{
					hit = true;
				}
				break;
			default:
				break;
		}

		if (hit)
		{
			hit = false;
			countHitMan++;
			Main.scoreManager.manHitten();
			GetComponent<AudioSource>().PlayOneShot(ouch);
			background.GetComponent<Image>().sprite = background_ouch;
			StartCoroutine("backToNormalBackground");
		}
	}
}
