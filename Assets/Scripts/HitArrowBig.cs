using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HitArrowBig : MonoBehaviour, IPointerClickHandler
{
	private bool hit = false;
	
	public int numberOfTap = 3;
	
	private float currentX;
	
	private ArrowLinearMovement arrowLinearMovement;
	
	public AudioClip crackWood;

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
		numberOfTap--;
		this.transform.localScale = new Vector3(this.transform.localScale.x - (this.transform.localScale.x / 3), this.transform.localScale.y - (this.transform.localScale.y / 3), this.transform.localScale.z);
		Main.scoreManager.arrowStopped();
		GetComponent<AudioSource>().PlayOneShot(crackWood);
		if (!hit && numberOfTap == 0)
		{
			hit = true;
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
