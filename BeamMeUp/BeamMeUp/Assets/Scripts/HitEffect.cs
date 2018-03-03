using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour {
    public GameObject Prefab;

   public void SpawnHitEffect(RaycastHit hit)
    {
        var go = GameObject.Instantiate(Prefab, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(go, 4f);
    }

}
