using UnityEngine;
using System.Collections;

public class ArrowLinearMovement : MonoBehaviour
{
	private float fuzziness = 1.0f;
	public float length = 2f;
	public Vector3 target;
	public float smooth = 5.0F;
	private bool arrowHit = false;
	private bool fuzzinessActivated = false;
	private float currentX, currentY, currentZ;
	public float amplitude = 0.5f;
	public float frequency = 0.5f;

	private GameObject arrowTarget;

	void Awake()
	{
		arrowTarget = GameObject.Find("ArrowTarget");
	}

	void Start()
	{
		target = new Vector3(arrowTarget.transform.position.x + length,
			this.gameObject.transform.position.y,
			0);
	}
	
	void Update()
	{
		if (!arrowHit)
		{
			currentX = transform.position.x;
			currentY = transform.position.y;
			currentZ = transform.position.z;
			transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * smooth);
		}
		else
		{
			transform.position = new Vector3(currentX, transform.position.y, transform.position.z);
		}
	}

	void FixedUpdate()
	{
		if (fuzzinessActivated)
		{
			GetComponent<Rigidbody2D>().velocity += new Vector2(0f, Random.Range(-fuzziness, fuzziness));
		}
	}

	public void activateFuzziness()
	{
		fuzzinessActivated = true;
	}

	public void deactivateFuzziness()
	{
		fuzzinessActivated = false;
	}

	public void setFuzziness(float changedFuzziness)
	{
		this.fuzziness = changedFuzziness;
	}

	public void hit()
	{
		arrowHit = true;
	}

	public void setLength(float length)
	{
		this.length = length;
	}
}
