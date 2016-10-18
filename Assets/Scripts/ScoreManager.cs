using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{

	const string LEVEL_NAME = "DoodleWilly";

	public delegate void GameOver();

	public static event GameOver OnGameOver;

	//GameObject
	public Text life1;
	public Text life2;
	public Text life3;
	public GameObject man;
	public Text textPowerUp;
	public Text newHighscore;
	
	//Colori
	private Color originalTextColor;
	private Color fadedBlack = new Color(50f / 255f, 50f / 255f, 50f / 255f, 125f / 255f);
	private Color red = new Color(255f / 255f, 0f / 255f, 0 / 255f, 100f / 255f);
	private Color green = new Color(73f / 255f, 244f / 255f, 0 / 255f, 100f / 255f);
	private Color yellow = new Color(247f / 255f, 255f / 255f, 0 / 255f, 100f / 255f);
	private Color black = new Color(0f / 255f, 0f / 255f, 0f / 255f, 100f / 255f);
	private Color purple = new Color(223f / 255f, 0f / 255f, 190f / 255f, 100f / 255f);
	
	//Script
	private HitMan hitMan;
	
	//Stringhe
	private string newName;
	private string oldName;
	private string textScore;
	
	//interi
	private int newScore;
	private int oldScore;
	private int score = 0;
	private int comboCounter = 0;
	private float increment = 0;
	private int countLifes = 3;
	private int updateScoreSlowererCoefficient = 10;
	private int delayStartScore = 0;
	private float disappearTextPowerUp = 1.5f;
	private float minNumberOfArrowsBeforRandomBonus = 15f;
	private float maxNumberOfArrowsBeforRandomBonus = 50f;
	private float numberOfArrowsBeforRandomBonus;
	private int counterRandomBonus = 0;
	private float timeCombo = 0.7f;
	private int boundCombo = 3;
	//boolean
	public bool enableCleanHighscore = false;
	private bool stopScore = false;
		
	//audio
	public AudioClip oneMoreLife;
	public AudioClip allTheLife;
	public AudioClip randomBonus;

	public void Reset()
	{
		newScore = 0;
		oldScore = 0;
		score = 0;
		comboCounter = 0;
		increment = 0;
		countLifes = 3;
		updateScoreSlowererCoefficient = 10;
		delayStartScore = 0;
				 
		counterRandomBonus = 0;
		timeCombo = 0.7f;
		boundCombo = 3;
		stopScore = false;
		this.GetComponent<Text>().color = fadedBlack;
		life1.color = fadedBlack;
		life2.color = fadedBlack;
		life3.color = fadedBlack;
	}

	public void StartScoring()
	{
		if (enableCleanHighscore)
		{
			cleanHighscore();
		}
		
		newHighscore.gameObject.SetActive(false);
		
		numberOfArrowsBeforRandomBonus = getRandomBetweenBounds(minNumberOfArrowsBeforRandomBonus, maxNumberOfArrowsBeforRandomBonus); 		
		
		hitMan = (HitMan)man.GetComponent(typeof(HitMan));
		
		textPowerUp.gameObject.SetActive(false);
		
		originalTextColor = this.GetComponent<Text>().color;
		updateTextMesh();
	}

	IEnumerator deactivateTextPowerUp()
	{
		yield return new WaitForSeconds(disappearTextPowerUp);
		textPowerUp.gameObject.SetActive(false);
	}

	public void showtextPowerUp(String text, Color color)
	{
		textPowerUp.gameObject.SetActive(true);
		textPowerUp.GetComponent<Text>().color = color;
		textPowerUp.text = text;	
		StartCoroutine("deactivateTextPowerUp");
	}

	public void arrowStopped()
	{
		counterRandomBonus++;
		
		comboCounter++;
		if (comboCounter >= boundCombo)
		{
			comboCounter = 0;
			showtextPowerUp("COMBOOO!", red);
			GetComponent<AudioSource>().PlayOneShot(randomBonus);
			score += 500;
		}
		StartCoroutine("zeroCombo");
		
		if (counterRandomBonus >= numberOfArrowsBeforRandomBonus)
		{
			counterRandomBonus = 0;
			numberOfArrowsBeforRandomBonus = getRandomBetweenBounds(minNumberOfArrowsBeforRandomBonus, maxNumberOfArrowsBeforRandomBonus); 
			showtextPowerUp("GREAT!", red);
			GetComponent<AudioSource>().PlayOneShot(randomBonus);
		}
		score += 70;
		this.GetComponent<Text>().color = yellow;
		StartCoroutine("backToNormalColour");	
	}

	IEnumerator zeroCombo()
	{
		yield return new WaitForSeconds(timeCombo);
		comboCounter = 0;
	}

	public void arrowStoppedEarly()
	{
		showtextPowerUp("EARLY!", red);
		GetComponent<AudioSource>().PlayOneShot(randomBonus);
		score += 210;
		this.GetComponent<Text>().color = red;
		StartCoroutine("backToNormalColour");	
	}

	public void arrowStoppedClose()
	{
		showtextPowerUp("SO CLOSE!", red);
		GetComponent<AudioSource>().PlayOneShot(randomBonus);
		score += 140;
		this.GetComponent<Text>().color = red;
		StartCoroutine("backToNormalColour");	
	}

	public void goldArrowStopped()
	{
		if (hitMan.isSpecialPowerOn())
		{
			score += 70;
			this.GetComponent<Text>().color = red;
			StartCoroutine("backToNormalColour");
		}
		else
		{
			showtextPowerUp("RAGE!!!!!", red);
			score += 5000;
			hitMan.activateSpecialPower();
			this.GetComponent<Text>().color = red;
			StartCoroutine("backToNormalColour");
		}
	}

	public void invincible()
	{
		showtextPowerUp("INVINCIBLE", red);
		
		score += 10000;
		hitMan.activateSpecialPower();
		this.GetComponent<Text>().color = red;
		StartCoroutine("backToNormalColour");	
	}

	public void appleHitten()
	{
		showtextPowerUp("DELICIOUS!", green);
		score += 1000;
		this.GetComponent<Text>().color = green;
		StartCoroutine("backToNormalColour");
	}

	public void manHitten()
	{
		countLifes--;
		
		if (countLifes == 0)
		{
			life3.color = red;
			if (OnGameOver != null)
			{
				OnGameOver();
			}

		}
		else
		{
			increment = 0;
			StartCoroutine("backToNormalColour");
			
			if (countLifes == 2)
			{
				life1.color = red;
			}
			else
			{
				life2.color = red;
			}
		}	
	}


	
	public void giveLife()
	{
		if (countLifes < 3)
		{
			GetComponent<AudioSource>().PlayOneShot(oneMoreLife);
			countLifes++;
			showtextPowerUp("1 LIFE!", yellow);
		}
		else
		{
			GetComponent<AudioSource>().PlayOneShot(allTheLife);
			showtextPowerUp("AWESOME!", purple);
			score += 2000;
			this.GetComponent<Text>().color = purple;
			StartCoroutine("backToNormalColour");
		}
		
		switch (countLifes)
		{
			case 1:
				life3.color = fadedBlack;
				life2.color = red;
				life1.color = red;
				break;
			case 2:
				life3.color = fadedBlack;
				life2.color = fadedBlack;
				life1.color = red;
				break;
			case 3:
				life3.color = fadedBlack;
				life2.color = fadedBlack;
				life1.color = fadedBlack;
				break;
			default:
				break;
		}
	}

	public int getNumberOfLifesLeft()
	{
		return countLifes;	
	}

	IEnumerator backToNormalColour()
	{
		yield return new WaitForSeconds(0.3f);
		if (countLifes != 0)
		{
			this.GetComponent<Text>().color = originalTextColor;
		}
	}

	private void updateTextMesh()
	{
		textScore = score.ToString();
		this.GetComponent<Text>().text = textScore;
	}

	private void updateScore()
	{
		score = (int)(score + Mathf.Log(((float)(++increment)) / 10) + 5);
	}

	public void stopUpdateScore()
	{
		stopScore = true;
		addScore("", score);
	}

	public void SetStopScore(bool trigger)
	{
		stopScore = trigger;
	}

	void FixedUpdate()
	{
		if (stopScore == false)
		{
			if ((int)Time.timeSinceLevelLoad >= delayStartScore)
			{
				if ((Time.frameCount % updateScoreSlowererCoefficient) == 0)
				{
					updateScore();
					updateTextMesh();
				}
			}
		}
	}

	public void addScore(string name, int score)
	{
		newScore = score;
		newName = name;

		string scoreEntry;
		string scoreNameEntry;
		for (int i = 0; i < 5; i++)
		{
			scoreEntry = string.Format("HScore{0}{1}", LEVEL_NAME, i);
			scoreNameEntry = string.Format("HScoreName{0}{1}", LEVEL_NAME, i);
			if (PlayerPrefs.HasKey(scoreEntry))
			{
				if (PlayerPrefs.GetInt(scoreEntry) < newScore)
				{ 
					oldScore = PlayerPrefs.GetInt(scoreEntry);
					oldName = PlayerPrefs.GetString(scoreNameEntry);
					PlayerPrefs.SetInt(scoreEntry, newScore);
					PlayerPrefs.SetString(scoreNameEntry, newName);
					newScore = oldScore;
					newName = oldName;
				}
			}
			else
			{
				PlayerPrefs.SetInt(scoreEntry, newScore);
				PlayerPrefs.SetString(scoreNameEntry, newName);
				newScore = 0;
				newName = "";
			}
		}

		string topEntry = string.Format("HScore{0}{1}", LEVEL_NAME, 0);
		if (PlayerPrefs.GetInt(topEntry) == score)
		{
			newHighscore.gameObject.SetActive(true);
		}
	}

	private void cleanHighscore()
	{
		string scoreEntry;
		string scoreNameEntry;
		for (int i = 0; i < 5; i++)
		{
			scoreEntry = string.Format("HScore{0}{1}", LEVEL_NAME, i);
			scoreNameEntry = string.Format("HScoreName{0}{1}", LEVEL_NAME, i);

			PlayerPrefs.SetInt(scoreEntry, 0);
			PlayerPrefs.SetString(scoreNameEntry, "");
		}
	}

	float getRandomBetweenBounds(float min, float max)
	{
		UnityEngine.Random.seed = (int)(System.DateTime.Now.Second * this.GetInstanceID() + Time.renderedFrameCount);
		return UnityEngine.Random.Range(min, max);
	}
}