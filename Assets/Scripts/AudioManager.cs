using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;
    public AudioClip bombMusic;
    public AudioClip levelUpSound;
    public AudioClip lostLifeSound;
    public AudioSource soundEffectAudio;
    public int a;  // flag for Level 1
    public int b;  // flag for GameMenu
    public bool vibration;
    //public SecretBomb secretBomb;
    public PodSpawner pods;
    void Start()
    {
        pods.Setup();

        a = 0;
        b = 0;
        //vibration = true;
        if (Instance == null)
        {
            Instance = this;    // makes sure this is the only SoundManager
        }
        else if (Instance != null)
        {
            Destroy(gameObject);    // if there are others, destroy them
        }

        AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if (source.clip == null)
            {
                soundEffectAudio = source;
            }
        }

        soundEffectAudio.clip = bombMusic;
        soundEffectAudio.loop = true;
        soundEffectAudio.Play(0);
        /*
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            soundEffectAudio.clip = gameMusic;
            soundEffectAudio.loop = true;
            soundEffectAudio.Play(0);
        }
        if (SceneManager.GetActiveScene().name == "GameMenu")
        {
            soundEffectAudio.clip = menuMusic;
            soundEffectAudio.loop = true;
            soundEffectAudio.Play(0);
        }

        if (PlayerPrefs.GetInt("sound") == 0)
        {
            soundEffectAudio.mute = true;
        }
        else
        {
            soundEffectAudio.mute = false;
        }

        DontDestroyOnLoad(gameObject.transform);
        */

        
    }

    void Update()
    {
        /*
        soundEffectAudio.clip = bombMusic;
        soundEffectAudio.loop = true;
        soundEffectAudio.Play(0);
        /*
        if (SceneManager.GetActiveScene().name == "MainScene" && a == 0)
        {
            soundEffectAudio.clip = gameMusic;
            soundEffectAudio.loop = true;
            soundEffectAudio.Play(0);
            a++;
        }
        if (SceneManager.GetActiveScene().name == "GameMenu" && b == 0)
        {
            soundEffectAudio.clip = menuMusic;
            soundEffectAudio.loop = true;
            soundEffectAudio.Play(0);
            b++;
        }
        
        if (secretBomb.CheckRaycast() == 3)
        {
            soundEffectAudio.volume = 0.8f;
        }
        else if (secretBomb.CheckRaycast() == 2)
        {
            soundEffectAudio.volume = 0.5f;
        }
        else if (secretBomb.CheckRaycast() == 1)
        {
            soundEffectAudio.volume = 0.2f;
        }
        else
        {
            soundEffectAudio.volume = 0.0f;
        }
        */
        
        foreach(GameObject sb in pods.pods)
        {
            if(sb.GetComponent<SecretBomb>().CheckRaycast() != 0)
            {
                soundEffectAudio.volume = sb.GetComponent<SecretBomb>().CheckRaycast();
            }
        }
    }

    public void PlayOneShot(AudioClip clip)
    {
        soundEffectAudio.PlayOneShot(clip);
    }

    /*
    public void PlayGameOverSound()
    {
        soundEffectAudio.clip = paperRipSound;
        soundEffectAudio.loop = false;
        soundEffectAudio.Play(0);
    }
    */

    public void RestartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        AudioManager.Instance.soundEffectAudio.Stop();
        Application.Quit();
    }
}
