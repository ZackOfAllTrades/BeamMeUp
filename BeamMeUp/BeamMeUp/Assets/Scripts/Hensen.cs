using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Hensen : MonoBehaviour
{
    public Material[] shirts = new Material[3];
    public float speedDamp = .1f;
    public Transform exit;
    int speedHashParam;
    [HideInInspector]
    public NavMeshAgent agent;
    Animator anim;
    bool isExiting;
 
    // Use this for initialization
    void Start()
    {

        var body = transform.GetChild(0).transform.Find("Body");
        body.GetComponent<SkinnedMeshRenderer>().material = shirts[Random.Range(0, 3)];


        anim = GetComponent<Animator>();
        speedHashParam = Animator.StringToHash("Speed");
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        exit = GameObject.Find("Exit").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        var speed = agent.desiredVelocity.magnitude;
        anim.SetFloat(speedHashParam, speed, speedDamp, Time.deltaTime);


        if (isExiting)
        {
            if (agent.remainingDistance < 6)
            {
                Destroy(gameObject);
            }
        }
    }

    public void GoToExit()
    {
        agent.SetDestination(exit.position);
        agent.isStopped = false;
        isExiting = true;
    }

    public void GetHit()
    {
        Debug.Log("DIE");
    }
    //public override void GetHit(RaycastHit hit, int damage, int force, Vector3 hitDirection)
    //{       
    //    //var hitEffectCount = HitEffects.Length;
    //    //if (hitEffectCount > 0)
    //    //{
    //    //    HitEffects[UnityEngine.Random.Range(0, hitEffectCount - 1)].Play(hit, hitDirection);
    //    //}
    //    _rigidBody.isKinematic = false;    
    //    var veclocity = MathZ.GetVelocity(Vector3.Distance(transform.position, _posLastFrame), Time.time - _timeLastFrame);
    //    _rigidBody.useGravity = true;
    //    _rigidBody.velocity = transform.forward * veclocity;
               
    //    _rigidBody.AddTorque(50 * hitDirection, ForceMode.Impulse);
    //    TakeDamage(damage);
    //}
}
