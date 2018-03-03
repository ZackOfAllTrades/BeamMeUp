using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class Monster : Shootable {
    public GameObject DeathPrefab;
    public float Health = 100f;
    public GameObject HitEffect;
    public GameScript GameScript;
    List<Vector3> Pos = new List<Vector3>();
    public float speedDamp = .1f;
    Vector3 target;
    int speedHashParam;
    [HideInInspector]
    public NavMeshAgent agent;
    Animator anim;   
    bool isMoving;
    // Use this for initialization
    void Start()
    {
        GameScript = GameObject.Find("GameScript").GetComponent<GameScript>();
        anim = GetComponent<Animator>();
        speedHashParam = Animator.StringToHash("Speed");
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        StartCoroutine(Init());

        Destroy(gameObject, 30f);
    }
    IEnumerator Init()
    {
        yield return new WaitForSeconds(1.5f);
        Pos.Add(GameObject.Find("MonsterPos1").transform.position);
        Pos.Add(GameObject.Find("MonsterPos2").transform.position);
        Pos.Add(GameObject.Find("MonsterPos3").transform.position);
        Pos.Add(GameObject.Find("MonsterPos4").transform.position);
        target = Pos[UnityEngine.Random.Range(0, 4)];
       
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            var speed = agent.desiredVelocity.magnitude;
            anim.SetFloat(speedHashParam, speed, speedDamp, Time.deltaTime);
            if (agent.remainingDistance < 2)
            {         
                agent.isStopped = true;
                GoSomewhereElse();
            }
        }
    }
    public void GoSomewhereElse()
    {
       target = Pos[UnityEngine.Random.Range(0, 4)];
        //target = GameObject.Find("Exit").transform.position;
        agent.SetDestination(target);
        agent.isStopped = false;      
    }
  

    public override void GetHit(RaycastHit hit, Vector3 impactDir)
    {    
        var go = GameObject.Instantiate(HitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Health -= 20;
        if (Health <= 0) Die();

    }

    void Die()
    {
       var dead =  Instantiate(DeathPrefab);
        dead.transform.position = gameObject.transform.position;
        dead.transform.localRotation = gameObject.transform.rotation;
        Destroy(gameObject);
        //Vector3 explosionPos = transform.position;
        //Collider[] colliders = Physics.OverlapSphere(explosionPos, 5);
        //foreach (Collider hit in colliders)
        //{
        //    Rigidbody rb = hit.GetComponent<Rigidbody>();

        //    if (rb != null)
        //        rb.AddExplosionForce(1000, explosionPos, 5, 3.0F);
        //}
    }
}
