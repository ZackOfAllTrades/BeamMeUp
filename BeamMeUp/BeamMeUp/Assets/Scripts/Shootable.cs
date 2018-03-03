using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shootable : MonoBehaviour {
    public abstract void GetHit(RaycastHit hit, Vector3 impactDir);
    
}
