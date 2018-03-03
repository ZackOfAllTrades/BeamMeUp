using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPad : MonoBehaviour {

    public GameManager manager;
    public AudioClip[] wetPlop = new AudioClip[5];
    public AudioClip warpBoom;
    public Transform[] teleporterPos;
    public GameObject vfxTeleport;
    public GameObject vfxTeleportShockWave;
    // public GameObject[] hensenPrefabs; // listed worst to best
    public GameObject PerfectHensen;
    public GameObject OkayHensen;
    public GameObject BadHensen;
    public GameObject ReallyBadHensen;
    public GameObject AwefulHensen;
    public GameObject MonsterPrefab;
    public AudioSource audioWarpIn;
    public AudioSource audioWetPlop;

    int[] funcPoints = new int[] { 0, 1, 2 };

    public float teleportDuration;
    public float hensenPause = 1f;

    //public void StartTeleportVFX(int teleporter)
    //{
    //    vfxTeleporters[teleporter].Play();
    //}

    //public void StopTeleportVFX(int teleporter)
    //{
    //    vfxTeleporters[teleporter].Stop();
    //}


        void Start()
    {
        //ReallyBad();
        

    }
    public void Teleport(float value)
    {
        Debug.Log(value);
        manager.EndTeleportRequestAndStartOver();
        //bad, perfect, really bad, really really bad

        if (value > .8f)
        {
            Monster();
        }
        else
 if (value > .60f)
        {
            ReallyBad();
        }
        else if(value > .50f)
        {
            Bad();
        }
        else if (value > .40f)
        {
            Okay();
        }
        else
        {
            Perfect();
        }
        

        //if (value <= 0.008)
        //{
        //    Perfect();
        //}
        //else
        //{
        //    var func = funcPoints[Random.Range(0, 3)];  
        //   if(func == 0)
        //    {
        //        Bad();
        //    }
        //   if( func == 1)
        //    {
        //        Okay();
        //    }
        //   if(func == 2)
        //    {
        //        ReallyBad();
               
        //    }

           
        
        //}



      
    }


    void Perfect()
    {
        var trans = teleporterPos[Random.Range(0, 3)];
        var partSys = Instantiate(vfxTeleport).GetComponent<ParticleSystem>();

        partSys.transform.position = trans.position;
        var main = partSys.main;

        StartCoroutine(TeleportInHensen(partSys, PerfectHensen, teleportDuration, trans));
        partSys.Play();
    }

    void Okay()
    {
        var trans = teleporterPos[Random.Range(0, 3)];
        var partSys = Instantiate(vfxTeleport).GetComponent<ParticleSystem>();

        partSys.transform.position = trans.position;
        var main = partSys.main;

        StartCoroutine(TeleportInHensen(partSys, OkayHensen, teleportDuration, trans));
        partSys.Play();
    }
    void Bad()
    {
        var trans = teleporterPos[Random.Range(0, 3)];
        var partSys = Instantiate(vfxTeleport).GetComponent<ParticleSystem>();

        partSys.transform.position = trans.position;
        var main = partSys.main;

        StartCoroutine(TeleportInHensen(partSys, BadHensen, teleportDuration, trans));
        partSys.Play();
    }

    void ReallyBad()
    {
        var trans = teleporterPos[Random.Range(0, 3)];
        var partSys = Instantiate(vfxTeleport).GetComponent<ParticleSystem>();

        partSys.transform.position = trans.position;
        var main = partSys.main;

        StartCoroutine(TeleportInReallyBadHensen(partSys, ReallyBadHensen, teleportDuration, trans));
        partSys.Play();
    }
    void Monster()
    {
        var trans = teleporterPos[Random.Range(0, 3)];
        var partSys = Instantiate(vfxTeleport).GetComponent<ParticleSystem>();

        partSys.transform.position = trans.position;
        var main = partSys.main;

        StartCoroutine(TeleportInMonster(partSys, MonsterPrefab, teleportDuration, trans));
        partSys.Play();
    }

    // void Perfect()
    // {
    //     var trans = teleporterPos[Random.Range(0, 3)];
    //     var partSys = Instantiate(vfxTeleport).GetComponent<ParticleSystem>();

    //     partSys.transform.position = trans.position;
    //     var main = partSys.main;

    //     StartCoroutine(TeleportInHensen(partSys, hensenPrefabs[0], teleportDuration, trans));
    //     partSys.Play();

    // }

    // void Okay()
    // {
    //     //play bad music
    //     var trans = teleporterPos[Random.Range(0, 3)];
    //     var partSys = Instantiate(vfxTeleport).GetComponent<ParticleSystem>();
    //     partSys.transform.position = trans.position;
    //     var main = partSys.main;

    //     StartCoroutine(TeleportInHensen(partSys, hensenPrefabs[1], teleportDuration, trans));
    //     partSys.Play();

    // }

    // void Bad()
    // {
    //     var trans = teleporterPos[Random.Range(0, 3)];
    //     var partSys = Instantiate(vfxTeleport).GetComponent<ParticleSystem>();
    //     partSys.transform.position = trans.position;
    //     var main = partSys.main;

    //     StartCoroutine(TeleportInHensen(partSys, hensenPrefabs[2], teleportDuration, trans));
    //     partSys.Play();
    // }

    //void ReallyBad()
    // {

    //     var trans = teleporterPos[Random.Range(0, 3)];
    //     var partSys = Instantiate(vfxTeleport).GetComponent<ParticleSystem>();
    //     partSys.transform.position = trans.position;
    //     var main = partSys.main;

    //     StartCoroutine(TeleportInReallyBadHensen(partSys, hensenPrefabs[3], teleportDuration, trans));
    //     partSys.Play();
    // }

    IEnumerator TeleportInHensen(ParticleSystem teleportPartSys, GameObject hensenPrefab, float timeToInvoke, Transform teleporterTrans)
    {

        yield return new WaitForSeconds(timeToInvoke);
       

        audioWarpIn.clip = warpBoom;
        audioWarpIn.Play();
        var hensenGO = Instantiate(hensenPrefab);
        hensenGO.transform.position = teleporterTrans.position;
        hensenGO.transform.rotation = teleporterTrans.rotation;


        var teleportShock = Instantiate(vfxTeleportShockWave);
        teleportShock.transform.position = teleportPartSys.transform.position;

        Destroy(teleportShock, teleportShock.GetComponent<ParticleSystem>().main.duration);
        Destroy(teleportPartSys.gameObject);

       
            yield return new WaitForSeconds(hensenPause);
            var hensen = hensenGO.GetComponent<Hensen>();
            hensen.GoToExit();
        
        manager.isRunning = false;
       

    }

    IEnumerator TeleportInReallyBadHensen(ParticleSystem teleportPartSys, GameObject hensenPrefab, float timeToInvoke, Transform teleporterTrans)
    {
        yield return new WaitForSeconds(timeToInvoke);
        audioWarpIn.clip = wetPlop[Random.Range(0, 4)]; ;
        audioWarpIn.Play();
     

        audioWarpIn.clip = warpBoom;
        audioWarpIn.Play();
        var hensenGO = Instantiate(hensenPrefab);
        hensenGO.transform.position = teleporterTrans.position;
        hensenGO.transform.rotation = teleporterTrans.rotation;

        var teleportShock = Instantiate(vfxTeleportShockWave);
        teleportShock.transform.position = teleportPartSys.transform.position;

        Destroy(teleportShock, teleportShock.GetComponent<ParticleSystem>().main.duration);
        Destroy(teleportPartSys.gameObject);


       
   


    }


    IEnumerator TeleportInMonster(ParticleSystem teleportPartSys, GameObject monster, float timeToInvoke, Transform teleporterTrans)
    {
        yield return new WaitForSeconds(timeToInvoke);
        audioWarpIn.clip = wetPlop[Random.Range(0, 4)]; ;
        audioWarpIn.Play();


        audioWarpIn.clip = warpBoom;
        audioWarpIn.Play();
        var hensenGO = Instantiate(monster);
        hensenGO.transform.position = teleporterTrans.position;
        hensenGO.transform.rotation = teleporterTrans.rotation;

        var teleportShock = Instantiate(vfxTeleportShockWave);
        teleportShock.transform.position = teleportPartSys.transform.position;

        Destroy(teleportShock, teleportShock.GetComponent<ParticleSystem>().main.duration);
        Destroy(teleportPartSys.gameObject);
    }
}
