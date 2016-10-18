using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
	private static int numberOfArrowBroken = 0;
	private static int numberOfArrowThrown = 0;
	private static int numberOfAppleHitten = 0;

	public GameObject score;
	public static ScoreManager scoreManager;

	public GameObject time;
	public static TimeManager timeManager;

	public GameObject arrowGeneratorGo;
	private ArrowGenerator arrowGenerator;

	public GameObject apple;
	public static AppleAnimation appleAnimation;

	public GameObject man;
	private ManAnimation manAnimation;

	public GameObject mainMenuCanvas;
	public GameObject gameCanvas;
	public GameObject settingsCanvas;
	public GameObject tutorialCanvas;
	public GameObject highscoresCanvas;

	public GameObject endOfGameCanvas;

	public AudioClip tap;

	// Use this for initialization
	void Start()
	{
		scoreManager = score.GetComponent<ScoreManager>();
		timeManager = time.GetComponent<TimeManager>();
		arrowGenerator = arrowGeneratorGo.GetComponent<ArrowGenerator>();
		appleAnimation = apple.GetComponent<AppleAnimation>();
		manAnimation = man.GetComponent<ManAnimation>();
		ScoreManager.OnGameOver += GameOver;
		if(!this.GetComponent<AudioOnOff>().isAudioActive()){
			AudioListener.volume = 0.0f;
		}
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public void Menu()
	{
		Time.timeScale = 1.0f;

		GetComponent<AudioSource>().PlayOneShot(tap);

		mainMenuCanvas.SetActive(true);
		gameCanvas.SetActive(false);
		settingsCanvas.SetActive(false);
		tutorialCanvas.SetActive(false);
		highscoresCanvas.SetActive(false);
		endOfGameCanvas.SetActive(false);
	}

	public void Play()
	{
		// Destroy any arrows left from the menu
		var arrowsMenu = GameObject.FindGameObjectsWithTag ("arrowMenu");
		for(int i=0; i<arrowsMenu.Length; i++){
			Destroy(arrowsMenu[i]);
		}

		GetComponent<AudioSource>().PlayOneShot(tap);

		mainMenuCanvas.SetActive(false);
		gameCanvas.SetActive(true);
		settingsCanvas.SetActive(false);
		tutorialCanvas.SetActive(false);
		highscoresCanvas.SetActive(false);
		endOfGameCanvas.SetActive(false);

		manAnimation.StartAnimation();
		appleAnimation.StartAnimation();
		timeManager.StartTimer();
		arrowGenerator.StartGeneration();
		scoreManager.StartScoring();
	}

	public void Settings()
	{
		GetComponent<AudioSource>().PlayOneShot(tap);

		mainMenuCanvas.SetActive(false);
		gameCanvas.SetActive(false);
		settingsCanvas.SetActive(true);
		tutorialCanvas.SetActive(false);
		highscoresCanvas.SetActive(false);
		endOfGameCanvas.SetActive(false);
	}

	public void Highscores()
	{
		GetComponent<AudioSource>().PlayOneShot(tap);

		mainMenuCanvas.SetActive(false);
		gameCanvas.SetActive(false);
		settingsCanvas.SetActive(false);
		tutorialCanvas.SetActive(false);
		highscoresCanvas.SetActive(true);
		endOfGameCanvas.SetActive(false);


	}

	public void Tutorial()
	{
		GetComponent<AudioSource>().PlayOneShot(tap);

		mainMenuCanvas.SetActive(false);
		gameCanvas.SetActive(false);
		settingsCanvas.SetActive(false);
		tutorialCanvas.SetActive(true);
		highscoresCanvas.SetActive(false);
	}

	public static void incrementArrowBroken()
	{
		numberOfArrowBroken++;
	}

	public static void incrementArrowThrown()
	{
		numberOfArrowThrown++;
	}

	public static void incrementAppleHitten()
	{
		numberOfAppleHitten++;
	}

	public void GameOver()
	{
		GameObject[] go;
		timeManager.StopTimer();
		scoreManager.stopUpdateScore();
		go = GameObject.FindGameObjectsWithTag("normalArrow");
		for (int i = 0; i < go.Length; i++)
		{
			Destroy(go[i]);
		}
		go = GameObject.FindGameObjectsWithTag("specialArrow");
		for (int i = 0; i < go.Length; i++)
		{
			Destroy(go[i]);
		}
		go = GameObject.FindGameObjectsWithTag("bigArrow");
		for (int i = 0; i < go.Length; i++)
		{
			Destroy(go[i]);
		}
		go = GameObject.FindGameObjectsWithTag("goldArrow");
		for (int i = 0; i < go.Length; i++)
		{
			Destroy(go[i]);
		}


		if (GameObject.Find("textPowerUp") != null)
		{
			GameObject.Find("textPowerUp").SetActive(false);
		}

		endOfGameCanvas.SetActive(true);
		arrowGenerator.StopGeneration();
		scoreManager.Reset();
		appleAnimation.StopAllCoroutines();
		manAnimation.StopAllCoroutines();
	}
}
