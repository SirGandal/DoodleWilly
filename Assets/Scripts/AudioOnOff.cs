using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioOnOff : MonoBehaviour
{
	public Sprite audioOn;
	public Sprite audioOff;
	
	public AudioClip audioOnClip;
	public AudioClip audioOffClip;

	public GameObject audioButton;

	private bool audioActive;

	void Start()
	{
		if (isAudioActive())
		{
			audioActive = true;
			audioButton.GetComponent<Image>().sprite = audioOn;
		}
		else
		{
			audioActive = false; 
			audioButton.GetComponent<Image>().sprite = audioOff;
		}
	}

	public bool isAudioActive()
	{
		if (PlayerPrefs.HasKey("AudioActive"))
		{
			switch (PlayerPrefs.GetInt("AudioActive"))
			{
				case 0:
					return false;
					break;
				case 1:
					return true;
					break;
				default:
					break;
			}
		}

		// can't find preference, audio is on
		return true;
	}

	public void SetAudioPreference()
	{
		if (audioActive)
		{
			AudioListener.volume = 0.0f;
		}
		else
		{
			AudioListener.volume = 1.0f;
		}
	}

	public void OnMouseDown()
	{		
		if (audioActive)
		{
			audioButton.GetComponent<Image>().sprite = audioOff;
			audioActive = false;
			GetComponent<AudioSource>().PlayOneShot(audioOffClip);
			PlayerPrefs.SetInt("AudioActive", 0);
			AudioListener.volume = 0.0f;
		}
		else
		{
			audioButton.GetComponent<Image>().sprite = audioOn;
			audioActive = true;
			GetComponent<AudioSource>().PlayOneShot(audioOnClip);
			PlayerPrefs.SetInt("AudioActive", 1);
			AudioListener.volume = 1.0f;
		}
	}

	public void touchedAudio()
	{		
		if (audioActive)
		{
			audioButton.GetComponent<Image>().sprite = audioOff;
			audioActive = false;
			GetComponent<AudioSource>().PlayOneShot(audioOffClip);
			PlayerPrefs.SetInt("AudioActive", 0);
			AudioListener.volume = 0.0f;
		}
		else
		{
			audioButton.GetComponent<Image>().sprite = audioOn;
			audioActive = true;
			GetComponent<AudioSource>().PlayOneShot(audioOnClip);
			PlayerPrefs.SetInt("AudioActive", 1);
			AudioListener.volume = 1.0f;
		}
	}
}
