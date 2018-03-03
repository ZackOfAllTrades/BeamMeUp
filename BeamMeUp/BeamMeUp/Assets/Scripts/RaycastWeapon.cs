using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using VRTK;
using VRTK.Examples;

public class RaycastWeapon : VRTK_InteractableObject
{
    public int Range = 200;
    public int Damage = 10;
    public int Force = 2000;
    public float FireRate = 0.5f;


    public bool HapticPulse = false;
    public float Strength = 0.63f;
    public float Duration = 0.2f;
    public float Interval = 0.01f;

    public Transform GunEnd;
    public ParticleSystem MuzzleFlash;
    public ParticleSystem CartridgeEjection;
    // public GameObject HitEffect;
    public AudioClip GunFireClip;

    private AudioSource _audioSource;

    private GameObject _trigger;
    private RealGun_Slide _slide;

    private Rigidbody _slideRigidbody;
    private Collider _slideCollider;

    private VRTK_ControllerEvents controllerEvents;

    private float minTriggerRotation = -10f;
    private float maxTriggerRotation = 45f;
    private float _lastShotTime;



    public override void StartUsing(VRTK_InteractUse currentUsingObject)
    {
        Debug.Log("FIRE");
        if (Time.time - _lastShotTime > 1 / FireRate)
        {
            base.StartUsing(currentUsingObject);

            FireRay();


            if (HapticPulse)
                VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(controllerEvents.gameObject), 0.63f, 0.2f, 0.01f);

            _lastShotTime = Time.time;
        }
    }








    protected override void Awake()
    {
        base.Awake();

        //  _trigger = transform.Find("TriggerHolder").gameObject;
      //  _audioSource = GetComponent<AudioSource>();
     //   _audioSource.clip = GunFireClip;

        //_slide = transform.Find("Slide").GetComponent<RealGun_Slide>();
        //_slideRigidbody = _slide.GetComponent<Rigidbody>();
        //_slideCollider = _slide.GetComponent<Collider>();
    }

    //protected override void Update()
    //{
    //    base.Update();
    //    if (controllerEvents)
    //    {
    //        var pressure = (maxTriggerRotation * controllerEvents.GetTriggerAxis()) - minTriggerRotation;
    //        _trigger.transform.localEulerAngles = new Vector3(0f, pressure, 0f);
    //    }
    //    else
    //    {
    //        _trigger.transform.localEulerAngles = new Vector3(0f, minTriggerRotation, 0f);
    //    }
    //}

    private void FireRay()
    {
     //   MuzzleFlash.Play();

        if (CartridgeEjection)
            CartridgeEjection.Play();

      //  _audioSource.Play();

        Vector3 rayOrigin = GunEnd.position;
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, GunEnd.forward, out hit, Range))
            HandleHit(hit);

    }

    void HandleHit(RaycastHit hit)
    {
        
        if (hit.collider)
        {
            //var rb = hit.collider.GetComponent<Rigidbody>();
            //if (rb != null)
            //{
            //    rb.AddForceAtPosition(Force * GunEnd.forward, hit.point);
            //}

            var hitObject = hit.collider.gameObject;
            if (hitObject)
            {
                if (hitObject.name == "Head")
                {
                    hitObject.GetComponent<Rigidbody>().AddForce(hit.point * 20);
                    hitObject.GetComponent<Hensen>().GetHit();
                }else
                {
                    //if (hitObject.GetComponent<Rigidbody>())
                    //{
                    //    hitObject.GetComponent<Rigidbody>().AddForce()
                    //}
                }
            }
               
                   




                    //_hitParticles.transform.position = hit.point;
                    //_hitParticles.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    //_hitParticles.Emit();


                    //  if (HitEffect)
                    // SpawnDecal(hit, HitEffect);
                }
    }

    //void SpawnDecal(RaycastHit hit)
    //{
    //    if (hit.collider.sharedMaterial != null)
    //    {
    //        string materialName = hit.collider.sharedMaterial.name;

    //        switch (materialName)
    //        {
    //            //case "Metal":
    //            //    SpawnDecal(hit, metalHitEffect);
    //            //    break;
    //            //case "Sand":
    //            //    SpawnDecal(hit, sandHitEffect);
    //            //    break;
    //            //case "Stone":
    //            //    SpawnDecal(hit, stoneHitEffect);
    //            //    break;
    //            //case "WaterFilled":
    //            //    SpawnDecal(hit, waterLeakEffect);
    //            //    SpawnDecal(hit, metalHitEffect);
    //            //    break;
    //            //case "Wood":
    //            //    SpawnDecal(hit, woodHitEffect);
    //            //    break;
    //            //case "Meat":
    //            //    SpawnDecal(hit, fleshHitEffects[Random.Range(0, fleshHitEffects.Length)]);
    //            //    break;
    //            //case "Character":
    //            //    SpawnDecal(hit, fleshHitEffects[Random.Range(0, fleshHitEffects.Length)]);
    //            //    break;
    //            //case "WaterFilledExtinguish":
    //            //    SpawnDecal(hit, waterLeakExtinguishEffect);
    //            //    SpawnDecal(hit, metalHitEffect);
    //              //  break;
    //        }
    //    }



    //    //GameObject spawnedDecal = GameObject.Instantiate(prefab, hit.point,
    //    //    Quaternion.LookRotation(hit.normal));
    //    //// Quaternion.FromToRotation(Vector3.up, hit.normal));
    //    //spawnedDecal.transform.SetParent(hit.collider.transform);
    //}


    public override void Grabbed(VRTK_InteractGrab currentGrabbingObject)
    {
        base.Grabbed(currentGrabbingObject);
        controllerEvents = currentGrabbingObject.GetComponent<VRTK_ControllerEvents>();

        // ToggleSlide(true);


        //Limit hands grabbing when picked up
        if (VRTK_DeviceFinder.GetControllerHand(currentGrabbingObject.controllerEvents.gameObject) == SDK_BaseController.ControllerHand.Left)
        {
            allowedTouchControllers = AllowedController.LeftOnly;
            allowedUseControllers = AllowedController.LeftOnly;
            //  _slide.allowedGrabControllers = AllowedController.RightOnly;
        }
        else if (VRTK_DeviceFinder.GetControllerHand(currentGrabbingObject.controllerEvents.gameObject) == SDK_BaseController.ControllerHand.Right)
        {
            allowedTouchControllers = AllowedController.RightOnly;
            allowedUseControllers = AllowedController.RightOnly;
            //_slide.allowedGrabControllers = AllowedController.LeftOnly;
        }
    }

    public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
    {
        base.Ungrabbed(previousGrabbingObject);

        //ToggleSlide(false);


        //Unlimit hands
        allowedTouchControllers = AllowedController.Both;
        allowedUseControllers = AllowedController.Both;
        //_slide.allowedGrabControllers = AllowedController.Both;

        controllerEvents = null;
    }
    private void ToggleCollision(Rigidbody objRB, Collider objCol, bool state)
    {
        objRB.isKinematic = state;
        objCol.isTrigger = state;
    }

    private void ToggleSlide(bool state)
    {
        if (!state)
        {
            _slide.ForceStopInteracting();
        }
        _slide.enabled = state;
        _slide.isGrabbable = state;
        ToggleCollision(_slideRigidbody, _slideCollider, state);
    }


}
//// private GameObject bullet;
////public float bulletSpeed = 1000f;
//// public float bulletLife = 5f;
//public int WeaponRange;
//public ParticleSystem MuzzleFlash;
//public ParticleSystem CartridgeEjection;
//public AudioClip SFX;
//[SerializeField]
//private GameObject _hitEffect;
//public Transform GunEnd;
//[SerializeField]
//private int _force = 2000;
//private AudioSource _audioSource;


//public override void StartUsing(VRTK_InteractUse usingObject)
//{
//    Debug.Log("FIRE");
//    base.StartUsing(usingObject);
//    FireBullet();
//}



//protected void Start()
//{
//    //bullet = transform.Find("Bullet").gameObject;
//    // bullet.SetActive(false);
//    _audioSource = GetComponent<AudioSource>();
//    _audioSource.clip = SFX;
//}

//private void FireBullet()
//{
//    MuzzleFlash.Play();
//    CartridgeEjection.Play();
//    _audioSource.Play();
//    Vector3 rayOrigin = GunEnd.position;
//    RaycastHit hit;
//    if (Physics.Raycast(rayOrigin, GunEnd.forward, out hit, WeaponRange))
//    {
//        HandleHit(hit);
//    }

//}


////        var direction = transform.forward;
////        var hit : RaycastHit;
//// var force = 2000;


//// if (Physics.Raycast(transform.position,direction,hit)){

//// if (hit.rigidbody)
////          hit.rigidbody.AddForceAtPosition(force* direction, hit.point);

//// if (hitParticles) {


////          hitParticles.transform.position = hit.point;
////          hitParticles.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
////          hitParticles.Emit();

////        }
////}

//void HandleHit(RaycastHit hit)
//{
//    if (hit.collider)
//    {
//        var rb = hit.collider.GetComponent<Rigidbody>();
//        if (rb != null)
//        {
//            rb.AddForceAtPosition(_force * GunEnd.forward, hit.point);
//        }
//        //_hitParticles.transform.position = hit.point;
//        //_hitParticles.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
//        //_hitParticles.Emit();

//        //if (_hitEffect)
//        // SpawnDecal(hit, _hitEffect);









//        //if (hit.collider.sharedMaterial != null)
//        //{
//        //    string materialName = hit.collider.sharedMaterial.name;

//        //    switch (materialName)
//        //    {
//        //        case "Metal":
//        //            SpawnDecal(hit, metalHitEffect);
//        //            break;
//        //        case "Sand":
//        //            SpawnDecal(hit, sandHitEffect);
//        //            break;
//        //        case "Stone":
//        //            SpawnDecal(hit, stoneHitEffect);
//        //            break;
//        //        case "WaterFilled":
//        //            SpawnDecal(hit, waterLeakEffect);
//        //            SpawnDecal(hit, metalHitEffect);
//        //            break;
//        //        case "Wood":
//        //            SpawnDecal(hit, woodHitEffect);
//        //            break;
//        //        case "Meat":
//        //            SpawnDecal(hit, fleshHitEffects[Random.Range(0, fleshHitEffects.Length)]);
//        //            break;
//        //        case "Character":
//        //            SpawnDecal(hit, fleshHitEffects[Random.Range(0, fleshHitEffects.Length)]);
//        //            break;
//        //        case "WaterFilledExtinguish":
//        //            SpawnDecal(hit, waterLeakExtinguishEffect);
//        //            SpawnDecal(hit, metalHitEffect);
//        //            break;
//    }
//}





