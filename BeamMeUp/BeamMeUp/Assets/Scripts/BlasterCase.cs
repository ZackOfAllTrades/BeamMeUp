using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterCase : MonoBehaviour {
    public GameObject Lid;
    public bool opening;
    public bool closing;
    bool closed;
    bool opened;
    Vector3 openPos;
    Vector3 closePos;
	// Use this for initialization
	void Start () {
        closed = true;
        openPos = Lid.transform.position + new Vector3(0, .45f,0);
       // openPos = Lid.transform.position + new Vector3(0, 20f, 0);
        closePos = Lid.transform.position;

    }
	
	// Update is called once per frame
	void Update () {

//        Lid.transform.position = Vector3.Lerp(Lid.transform.position, openPos, Time.deltaTime / 2);
        if (opening)
        {
            Debug.Log(Lid.gameObject);
           Lid.transform.position =  Vector3.Lerp(Lid.transform.position, openPos, Time.deltaTime);
           
        }
        //if (closing)
        //{
        //    Lid.transform.position = Vector3.Lerp(Lid.transform.position, closePos, Time.deltaTime / 2);
        //}
    }

    public void Open()
    {

        if (closed)
        {
            closing = false;
            opening = true;
            opened = true;
            closed = false;
        }
    }
    //    }else if(opened)
    //    {
    //        opening = false;
    //        closing = true;
    //        closed = true;
    //        opened = false;
    //    }
    //    StartCoroutine(Close());
    //}

    //IEnumerator Close()
    //{
    //    yield return new WaitForSeconds(8);
    //    Open();
    //}
   
}
