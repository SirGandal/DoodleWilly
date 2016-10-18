using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighscoresManager : MonoBehaviour
{
	const int NUMBER_OF_HIGHSCORES = 5;
	const string LEVEL_NAME = "DoodleWilly";
	GameObject go;
	private string text = "";
	int[] scores = new int[NUMBER_OF_HIGHSCORES];
	string[] finalScores = new string[NUMBER_OF_HIGHSCORES];
	string[] names = new string[NUMBER_OF_HIGHSCORES];

	void OnEnable()
	{

		string scoreEntry;
		string scoreNameEntry;

		for (int i = 0; i < NUMBER_OF_HIGHSCORES; i++)
		{
			scoreEntry = string.Format("HScore{0}{1}", LEVEL_NAME, i);
			scoreNameEntry = string.Format("HScoreName{0}{1}", LEVEL_NAME, i);

			scores[i] = PlayerPrefs.GetInt(scoreEntry);
			names[i] = PlayerPrefs.GetString(scoreNameEntry);
			finalScores[i] = names[i] + "-" + scores[i];
		}

		for (int i = 0; i < NUMBER_OF_HIGHSCORES; i++)
		{
			if (finalScores[i] != "-0")
			{
				text = string.Format("{0}{1}{2}\n", text, i + 1, finalScores[i]);
			}
		}
		Text textObj = this.gameObject.GetComponent<Text>();
		textObj.text = text;
	}
}
