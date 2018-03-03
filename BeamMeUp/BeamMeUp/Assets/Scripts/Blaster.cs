using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Blaster : MonoBehaviour {
    public ParticleSystem MuzzleFlash;
    AudioSource audio;
    VRTK_InteractableObject blaster;
    public Transform GunEnd;
	// Use this for initialization
	void Start () {
        blaster = GetComponent<VRTK_InteractableObject>();
        blaster.InteractableObjectUsed += Fire;
        audio = GetComponent<AudioSource>();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Fire(object sender, InteractableObjectEventArgs e)
    {
        MuzzleFlash.Play();
        audio.Play();
        RaycastHit hit;
        if (Physics.Raycast(GunEnd.position, GunEnd.forward, out hit, 400))
        {
            if (hit.collider.gameObject.GetComponent<HitEffect>())
            {
                hit.collider.gameObject.GetComponent<HitEffect>().SpawnHitEffect(hit);
            }

            if (hit.collider.gameObject.GetComponent<Shootable>())
                {
                var shootScript = hit.collider.gameObject.GetComponent<Shootable>();
                shootScript.GetHit(hit, GunEnd.forward) ;
            }
            //if (hit.collider.gameObject.transform.parent.GetComponent<Shootable>())
            //{
            //    var shootScript = hit.collider.gameObject.GetComponent<Shootable>();
            //    shootScript.GetHit(hit, GunEnd.forward);
            //}
            //if (hit.collider.gameObject.transform.parent.transform.parent.GetComponent<Shootable>())
            //{
            //    var shootScript = hit.collider.gameObject.GetComponent<Shootable>();
            //    shootScript.GetHit(hit, GunEnd.forward);
            //}
            //if (hit.collider.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<Shootable>())
            //{
            //    var shootScript = hit.collider.gameObject.GetComponent<Shootable>();
            //    shootScript.GetHit(hit, GunEnd.forward);
            //}

          
        }
    }
}
