using UnityEngine;
using System.Collections;

public class ArrowGeneratorMenu : MonoBehaviour
{
	private int arrows = 0;
	public int rotateAroundSpeed;

	private float minTime = 1.0f;
	private float maxTime = 4.0f;
	public float generationDepth;
	public float generationFlatDistance;
	private float waitingTime;
	
	private float probability;
	
	public GameObject toInstantiate;
	
	private ArrowLinearMovement arrowLinearMovement;
	
	private Vector3 generationDirection;
	
	public AudioClip arrowThrownSound;

	private Canvas canvas;
	private GameObject arrowTarget;

	void Start()
	{
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		arrowTarget = GameObject.Find("ArrowTargetMenu");
		StartCoroutine("Timer");
	}

	IEnumerator Timer()
	{
		waitingTime = GetRandomBetweenBounds(minTime, maxTime);
		yield return new WaitForSeconds(waitingTime);
		GenerateNewItem();
		StartCoroutine("Timer");
	}

	void GenerateNewItem()
	{
		arrows++;
		GameObject instantiated = Instantiate(toInstantiate);
		instantiated.name = toInstantiate.name;
		instantiated.transform.SetParent(canvas.transform, false);
		instantiated.transform.localPosition = this.transform.localPosition;
		StartCoroutine(MoveObject.use.TranslateToTransform(instantiated.transform, arrowTarget.transform, 2f, MoveObject.MoveType.Time, true));
		GetComponent<AudioSource>().PlayOneShot(arrowThrownSound);
	}

	float GetRandomBetweenBounds(float min, float max)
	{
		Random.seed = (int)(System.DateTime.Now.Second * this.GetInstanceID() + Time.renderedFrameCount);
		return Random.Range(min, max);
	}
}
