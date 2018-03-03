using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    
    public TeleportPad teleportPad;
    public bool isRunning;
    public float startTime = 60;
    public float TeleportTimeLimit = 20f;
    public int numOfTeleports = 5555;
    public AudioClip[] numbersClip;
    //public AudioClip wetPlop1;
    //public AudioClip wetPlop2;
    //public AudioClip wetPlop3;
    //public AudioClip wetPlop4;
    //public AudioClip wetPlop5;

    public AudioClip cpuAlg;
    public AudioClip cpuCalc;
    public AudioClip cpuManual;
    public AudioClip cpuAss;
    AudioClip[] sassTalk;

    public AudioClip cpuFaliure;
    public AudioClip cpuIntro;
    
    public AudioClip cpuSuccess;

    public AudioClip musicBeamMeUp;
    public AudioClip gameOver;
    public AudioClip victory;


    public GameObject WaveInterp;
    WaveFormDraw waveDraw;
    public AudioSource music;
    public AudioSource audioNumberSource;
    public AudioSource audioSFX;

    Coroutine IncomingTeleport;
	// Use this for initialization
	void Start ()
    {    
      
        sassTalk = new AudioClip[4]{ cpuAlg, cpuCalc, cpuManual, cpuAss };
        waveDraw = WaveInterp.GetComponent<WaveFormDraw>();
       // audioSFX = GetComponent<AudioSource>();
        music.loop = true;
        music.volume = 0.20f;
     

        Invoke("PlayIntro", 5f);
       
        
       
	}

    void PlayIntro()
    {

        audioSFX.clip = cpuIntro;//get ready;
        audioSFX.Play();
        Invoke("StartNewTeleport", audioSFX.clip.length);
        Invoke("PlayIntroMusic", audioSFX.clip.length);
    }

    void PlayIntroMusic()
    {
        music.clip = musicBeamMeUp;
        music.Play();
    }
	
   public void StartNewTeleport()
    {
        if (numOfTeleports > 0)
        {
            IncomingTeleport =  StartCoroutine(TransportRequest());
        }else
        {
            StartCoroutine(GameOver());
        }
    }

    public void EndTeleportRequestAndStartOver()
    {

        if (IncomingTeleport != null)
        {
            StopCoroutine(IncomingTeleport);
            Debug.Log("ENDING " + IncomingTeleport.GetHashCode());
        }

            numOfTeleports--;
        StartNewTeleport();
        
    }

   public IEnumerator TransportRequest()
    {
        if (!isRunning)
        {
            isRunning = true;
            Debug.Log("STARTED");
            yield return new WaitForSeconds(5);

            audioSFX.clip = sassTalk[Random.Range(0, 3)];
            audioSFX.Play();
            yield return new WaitForSeconds(audioSFX.clip.length);

            waveDraw.reset();

            yield return new WaitForSeconds(TeleportTimeLimit);

            audioNumberSource.clip = numbersClip[0];
            audioNumberSource.Play();
            yield return new WaitForSeconds(1);

            audioNumberSource.clip = numbersClip[1];
            audioNumberSource.Play();
            yield return new WaitForSeconds(1);

            audioNumberSource.clip = numbersClip[2];
            audioNumberSource.Play();
            yield return new WaitForSeconds(1);

            audioNumberSource.clip = numbersClip[3];
            audioNumberSource.Play();
            yield return new WaitForSeconds(1);

            audioNumberSource.clip = numbersClip[4];
            audioNumberSource.Play();
            yield return new WaitForSeconds(1);

            audioNumberSource.clip = numbersClip[5];
            audioNumberSource.Play();
            yield return new WaitForSeconds(1);

            audioNumberSource.clip = numbersClip[6];
            audioNumberSource.Play();
            yield return new WaitForSeconds(1);

            audioNumberSource.clip = numbersClip[7];
            audioNumberSource.Play();
            yield return new WaitForSeconds(1);

            audioNumberSource.clip = numbersClip[8];
            audioNumberSource.Play();

            yield return new WaitForSeconds(1);

            audioNumberSource.clip = numbersClip[9];
            audioNumberSource.Play();

            yield return new WaitForSeconds(1);


            teleportPad.Teleport(.008f);
        }
        isRunning = false;
    }

    IEnumerator GameOver()
    {
        Debug.Log("GAMEOVER");
        yield return new WaitForSeconds(2);
        audioSFX.clip = cpuFaliure;
        audioSFX.Play();
        yield return new WaitForSeconds(audioSFX.clip.length);
        music.clip = gameOver;
        music.Play();

        //do other stuff
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Application.Quit();
        }
    }
}

   




