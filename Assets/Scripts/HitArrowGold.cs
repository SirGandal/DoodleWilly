using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HitArrowGold : MonoBehaviour, IPointerClickHandler
{
	private float currentX;
	private ArrowLinearMovement arrowLinearMovement;
	public AudioClip crackWood;
	private bool hit = false;

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
			GetComponent<AudioSource>().PlayOneShot(crackWood);
			Main.scoreManager.goldArrowStopped();
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
