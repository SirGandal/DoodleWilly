using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArrowGenerator : MonoBehaviour
{
	private int arrows = 0;
	private int level = 0;
	private int numberOfLevels = 11;
	public int rotateAroundSpeed;

	private float[] goldArrowProbabilities = { 3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f, 1f, 1f, 1f };
	private float[] specialArrowProbabilities = { 7f, 7f, 7f, 7f, 7f, 7f, 7f, 7f, 1f, 2f, 2f };
	private float[] bigArrowProbabilities = { 13f, 13f, 13f, 13f, 13f, 13f, 13f, 13f, 8f, 10f, 10f };
	private float[] changeLevelTime = { 20f, 20f, 20f, 30f, 40f, 50f, 50f, 60f, 30f, 30f, 50f };
	private float[] maxTimer = { 4f, 3.5f, 3f, 2.5f, 2f, 1.5f, 2f, 1.7f, 1.5f, 1f, 1.3f };
	private float[] linearMovementsSpeedParameters = { 37f, 38f, 40f, 37f, 40f, 43f, 37f, 46f, 48f, 35f, 50f };
	private float[] linearMovementsFuzzinessParameters = { 0f, 0f, 0f, 0.4f, 0f, 0f, 10f, 20f, 30f, 50f, 0f };

	public float minTime = 0.0f;
	public float maxTime;
	private float waitBeforeChange = 15f;
	public float generationDepth;
	public float generationFlatDistance;
	private float waitingTime;
	
	private float probabilityGoldArrow = 3f;
	//3%
	private float probabilitySpecialArrow = 7f;
	//7%
	private float probabilityBigArrow = 13f;
	//13%
	private float probability;
	
	private float minPosNormalArrow;
	private float maxPosNormalArrow;
	private float minPosSpecialArrow;
	private float maxPosSpecialArrow;
	private float minPosGoldArrow;
	private float maxPosGoldArrow;
	private float minPosBigArrow;
	private float maxPosBigArrow;

	public GameObject toInstantiate;
	public GameObject bigArrow;
	public GameObject specialArrow;
	public GameObject goldArrow;
	public Text levelIndicator;
	public GameObject thrownArrowsAmount;

	private ArrowLinearMovement arrowLinearMovement;

	private Vector3 generationDirection;

	public AudioClip arrowThrownSound;

	private GameObject arrowTarget;

	public void StartGeneration()
	{
		level = 0;
		levelIndicator.text = "level " + (level + 1);
		arrowTarget = GameObject.Find("ArrowTarget");
		StartCoroutine("Timer");
		StartCoroutine("change");
		Vector3[] fourCorners = new Vector3[4];
		this.GetComponent<RectTransform>().GetLocalCorners(fourCorners);
		minPosNormalArrow = fourCorners[0].y;
		minPosBigArrow = fourCorners[0].y;
		minPosGoldArrow = fourCorners[0].y;
		minPosSpecialArrow = fourCorners[0].y;

		maxPosNormalArrow = fourCorners[1].y;
		maxPosBigArrow = fourCorners[1].y;
		maxPosGoldArrow = fourCorners[1].y;
		maxPosSpecialArrow = fourCorners[1].y;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			generateGoldArrow();
		}
		
		if (Input.GetKeyDown(KeyCode.S))
		{
			generateSpecialArrow();
		}
		
		if (Input.GetKeyDown(KeyCode.N))
		{
			generateNewItem();
		}
		
		if (Input.GetKeyDown(KeyCode.B))
		{
			generateBigArrow();
		}
	}

	public void StopGeneration()
	{
		StopAllCoroutines();
	}

	IEnumerator Timer()
	{
		
		maxTime = maxTimer[level];
		waitingTime = getRandomBetweenBounds(minTime, maxTime);
		
		yield return new WaitForSeconds(waitingTime);
		
		probability = getRandomBetweenBounds(0f, 100f);
		
		if (probability < probabilityGoldArrow)
		{
			generateGoldArrow();
		}
		else
		{
			if (probability < probabilitySpecialArrow)
			{
				generateSpecialArrow();
			}
			else
			{
				if (probability < probabilityBigArrow)
				{
					generateBigArrow();
				}
				else
				{
					generateNewItem();
				}
			}
		}
		StartCoroutine("Timer");
	}

	
	IEnumerator change()
	{
		waitBeforeChange = changeLevelTime[level];
		probabilityGoldArrow = goldArrowProbabilities[level];
		probabilitySpecialArrow = specialArrowProbabilities[level];
		probabilityBigArrow = bigArrowProbabilities[level];
		
		yield return new WaitForSeconds(waitBeforeChange);
		
		if ((level + 1) < numberOfLevels)
		{
			level++;
		}
		levelIndicator.text = "level " + (level + 1);
		StartCoroutine("change");	
	}

	void generateNewItem()
	{
		Main.incrementArrowThrown();
		arrows++;
		Vector2 randomTemp = new Vector2(-2f, getRandomBetweenBounds(minPosNormalArrow, maxPosNormalArrow));		
		Vector3 generationPosition = new Vector3(this.transform.localPosition.x, randomTemp.y, generationDepth + this.transform.position.z);
		
		GameObject instantiated = GameObject.Instantiate(toInstantiate);
		instantiated.transform.SetParent(this.transform.parent.transform, false);
		instantiated.transform.localPosition = generationPosition;
		instantiated.name = arrows.ToString();

		arrowTarget.transform.localPosition = new Vector3(arrowTarget.transform.localPosition.x, generationPosition.y);
		arrowLinearMovement = (ArrowLinearMovement)instantiated.GetComponent(typeof(ArrowLinearMovement));
		arrowLinearMovement.setLength(linearMovementsSpeedParameters[level]);
		if (linearMovementsFuzzinessParameters[level] != 0f)
		{
			arrowLinearMovement.setFuzziness(linearMovementsFuzzinessParameters[level]);
			arrowLinearMovement.activateFuzziness();
		}
		else
		{
			arrowLinearMovement.deactivateFuzziness();
		}
		
		GetComponent<AudioSource>().PlayOneShot(arrowThrownSound);
	}

	void generateBigArrow()
	{
		Main.incrementArrowThrown();
		arrows++;

		Vector2 randomTemp = new Vector2(-2f, getRandomBetweenBounds(minPosBigArrow, maxPosBigArrow));
		Vector3 generationPosition = new Vector3(this.transform.localPosition.x, randomTemp.y, generationDepth + this.transform.position.z);

		GameObject instantiated = GameObject.Instantiate(bigArrow);
		instantiated.transform.SetParent(this.transform.parent.transform, false);
		instantiated.transform.localPosition = generationPosition;

		
		instantiated.name = arrows.ToString();
		GetComponent<AudioSource>().PlayOneShot(arrowThrownSound);
	}

	void generateSpecialArrow()
	{
		Main.incrementArrowThrown();
		arrows++;
		Vector2 randomTemp = new Vector2(-2f, getRandomBetweenBounds(minPosSpecialArrow, maxPosSpecialArrow));		
		Vector3 generationPosition = new Vector3(this.transform.localPosition.x, randomTemp.y, generationDepth + this.transform.position.z);

		GameObject instantiated = GameObject.Instantiate(specialArrow);
		instantiated.transform.SetParent(this.transform.parent.transform, false);
		instantiated.transform.localPosition = generationPosition;

		instantiated.name = arrows.ToString();

		GetComponent<AudioSource>().PlayOneShot(arrowThrownSound);
	}

	void generateGoldArrow()
	{
		Main.incrementArrowThrown();
		arrows++;
		Vector2 randomTemp = new Vector2(-2f, getRandomBetweenBounds(minPosGoldArrow, maxPosGoldArrow));		
		Vector3 generationPosition = new Vector3(this.transform.localPosition.x, randomTemp.y, generationDepth + this.transform.position.z);

		GameObject instantiated = GameObject.Instantiate(goldArrow);
		instantiated.transform.SetParent(this.transform.parent.transform, false);
		instantiated.transform.localPosition = generationPosition;
		instantiated.name = arrows.ToString();
		GetComponent<AudioSource>().PlayOneShot(arrowThrownSound);
	}

	float getRandomBetweenBounds(float min, float max)
	{
		Random.seed = (int)(System.DateTime.Now.Second * this.GetInstanceID() + Time.renderedFrameCount);
		return Random.Range(min, max);
	}
}
