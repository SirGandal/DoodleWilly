using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HitArrow : MonoBehaviour, IPointerClickHandler
{
	private float currentX;
	private ArrowLinearMovement arrowLinearMovement;
	public AudioClip crackWood;
	private bool hit = false;
	private float earlyLimit = -3f;
	private float minCloseLimit = 3.4f;
	private float maxCloseLimit = 4.4f;
	private float maxBoundMan = 3.5f;

	void Awake()
	{
		arrowLinearMovement = (ArrowLinearMovement)this.gameObject.GetComponent(typeof(ArrowLinearMovement));
	}

	void Start()
	{
		StartCoroutine("checkIfNotMoving");
	}

	void Update()
	{
		currentX = this.transform.position.x;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Hit();
	}

	public void Hit()
	{
		if (!hit)
		{
			hit = true;
			if (this.gameObject.transform.position.x < earlyLimit)
			{
				Main.scoreManager.arrowStoppedEarly();	
			}
			else
			{
				if (this.gameObject.transform.position.x > minCloseLimit && this.gameObject.transform.position.x < maxCloseLimit && this.gameObject.transform.position.y < maxBoundMan)
				{
					Main.scoreManager.arrowStoppedClose();
				}
				else
				{
					Main.scoreManager.arrowStopped();
				}
			}
			GetComponent<AudioSource>().PlayOneShot(crackWood);
			Main.incrementArrowBroken();
			arrowLinearMovement.hit();
			this.GetComponent<Image>().transform.Rotate(new Vector3(0, 0, 270));
			this.GetComponent<Rigidbody2D>().gravityScale = 100;
		}
	}

	IEnumerator checkIfNotMoving()
	{
		yield return new WaitForSeconds(5f);
		Destroy(this.gameObject);
	}
}
