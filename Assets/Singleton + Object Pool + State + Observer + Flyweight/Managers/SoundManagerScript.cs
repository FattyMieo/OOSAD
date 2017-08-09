using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioClipID
{
	BGM_MAIN_MENU = 0,
	BGM_GAMEPLAY,
	BGM_LOSE,
	BGM_WIN,

	SFX_ATTACK,
	SFX_HIT,
	SFX_EXPLOSION,

	TOTAL
}

[System.Serializable]
public class AudioClipInfo
{
	public AudioClipID audioClipID;
	public AudioClip audioClip;
}

public class SoundManagerScript : MonoBehaviour
{
	#region Singleton
	private SoundManagerScript() {}

	private static SoundManagerScript mInstance;

	public static SoundManagerScript Instance
	{
		get
		{
			if(mInstance == null) Debug.LogError(typeof(SoundManagerScript).Name + " doesn't exist in the scene!");
			return mInstance;
		}
	}

	private void InitializeSingleton()
	{
		if (mInstance == null) mInstance = this;				//Assign this object to this reference
		else if (mInstance != this) Destroy(this.gameObject);	//Existed two or more instances, destroy duplicates
		DontDestroyOnLoad (gameObject);							//Avoid destroying when switching to another scene
	}
	#endregion Singleton

	[Header("Volume")]
	public float bgmVolume = 1.0f;
	public float sfxVolume = 1.0f;

	[Header("Audio Clips")]
	public List<AudioClipInfo> audioClipInfoList = new List<AudioClipInfo>();

	[Header("Audio Sources / Players")]
	public AudioSource sfxAudioSource;
	public AudioSource bgmAudioSource;

	public List<AudioSource> sfxAudioSourceList = new List<AudioSource>();
	public List<AudioSource> bgmAudioSourceList = new List<AudioSource>();

	// Preload before any Start() runs in other scripts
	void Awake () 
	{
		InitializeSingleton();

		if(bgmAudioSource == null) Debug.LogError("BGM Audio Source is not assigned in SoundManager!");
		if(sfxAudioSource == null) Debug.LogError("SFX Audio Source is not assigned in SoundManager!");
	}

	AudioClip FindAudioClip(AudioClipID audioClipID)
	{
		for(int i = 0; i < audioClipInfoList.Count; i++)
		{
			if(audioClipInfoList[i].audioClipID == audioClipID)
			{
				return audioClipInfoList[i].audioClip;
			}
		}

		Debug.LogWarning("Audio Clip " + audioClipID + " could not be found.");
		return null;
	}

	AudioSource FindAudioSource(AudioClip audioClip, List<AudioSource> audioSourceList)
	{
		for(int i = 0; i < audioSourceList.Count; i++)
		{
			if(audioSourceList[i].clip == audioClip)
			{
				return audioSourceList[i];
			}
		}
		return null;
	}

	// Background Music (BGM)
	public void PlayBGM(AudioClipID audioClipID)
	{
		bgmAudioSource.clip = FindAudioClip(audioClipID);
		bgmAudioSource.volume = bgmVolume;
		bgmAudioSource.loop = true;
		bgmAudioSource.Play();
	}

	public void PauseBGM()
	{
		if(bgmAudioSource.isPlaying)
		{
			bgmAudioSource.Pause();
		}
	}

	public void StopBGM()
	{
		if(bgmAudioSource.isPlaying)
		{
			bgmAudioSource.Stop();
		}
	}

	// Sound Effects (SFX)
	public void PlaySFX(AudioClipID audioClipID)
	{
		sfxAudioSource.PlayOneShot(FindAudioClip(audioClipID), sfxVolume);
	}

	public void PlayLoopingSFX(AudioClipID audioClipID)
	{
		AudioClip clipToPlay = FindAudioClip(audioClipID);

		// Searching for any existing audio source with the same clip
		AudioSource source = FindAudioSource(clipToPlay, sfxAudioSourceList);

		if(source != null)
		{
			if(source.isPlaying) return;

			source.volume = sfxVolume;
			source.loop = true;
			source.Play();
		}
		else
		{
			// If no instance is found, create a new one
			AudioSource newSource = gameObject.AddComponent<AudioSource>();
			newSource.clip = clipToPlay;
			newSource.volume = sfxVolume;
			newSource.loop = true;
			newSource.Play();
			// Add the new one into the list to track
			sfxAudioSourceList.Add(newSource);
		}
	}

	public void PauseLoopingSFX(AudioClipID audioClipID)
	{
		AudioClip clipToPause = FindAudioClip(audioClipID);

		AudioSource source = FindAudioSource(clipToPause, sfxAudioSourceList);

		if(source != null)
		{
			source.Pause();
		}
	}

	public void StopLoopingSFX(AudioClipID audioClipID)
	{
		AudioClip clipToStop = FindAudioClip(audioClipID);

		AudioSource source = FindAudioSource(clipToStop, sfxAudioSourceList);

		if(source != null)
		{
			source.Stop();
		}
	}

	public void ChangePitchLoopingSFX(AudioClipID audioClipID, float value)
	{
		AudioClip clipToChange = FindAudioClip(audioClipID);

		AudioSource source = FindAudioSource(clipToChange, sfxAudioSourceList);

		if(source != null)
		{
			source.pitch = value;
		}
	}

	public void SetBGMVolume(float value)
	{
		bgmVolume = value;
	}

	public void SetSFXVolume(float value)
	{
		sfxVolume = value;
	}
}
