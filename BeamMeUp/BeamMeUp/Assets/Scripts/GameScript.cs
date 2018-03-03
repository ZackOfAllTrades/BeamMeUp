using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.UnityEventHelper;

public class GameScript : MonoBehaviour {
    float playerScore = 1000;
    public TeleportPad TeleportScript;
   public GameObject Monster;
    public Transform BlasterCaseTrans;
    public GameObject BlasterCase;
    public Color blasterReady;
    public Color teleportReady;
    public float gameCircleXSizeMax;
    public float gameCircleYSizeMax;
    public float gameCircleXSizeMin;
    public float gameCircleYSizeMin;
    public GameObject BlasterButton;
    public GameObject PlayerCircle;
    public GameObject GameCircle;
    public GameObject TeleportButton;
    public GameObject CirclePanel;
    Material CirclePanelMat;
    MeshRenderer circlePanelmeshRend;
    MeshRenderer gameCircleMeshRend; 
    MathZ.ScalarNormalizer scaleInputNormalizer;
    MathZ.ScalarNormalizer colorInputNormalizer;

    bool isLeversChangingScale;
    bool isLeversChangingColor;
    bool isScaleLocked;
    bool isColorLocked;
    float playerXscale;
    float playerYscale;

    float gameCircleScale;
    float playerCircleScael;

    float scaleVariance;
    float colorVariance;
    // Use this for initialization
    void Start ()
    {
        StartCoroutine(OpenBlasterCase());
        TeleportScript = GameObject.Find("TeleportPadActor").GetComponent<TeleportPad>();
        // StartCoroutine(Test());
        //TeleportButton.GetComponent<MeshRenderer>().material.color = teleportNotReady;
        isLeversChangingScale = true;

        circlePanelmeshRend = CirclePanel.GetComponent<MeshRenderer>();
        CirclePanelMat = circlePanelmeshRend.material;
        CirclePanelMat.color = Color.gray;
       
     

        gameCircleMeshRend = GameCircle.GetComponent<MeshRenderer>();
        gameCircleMeshRend.material.color = Color.green;


        scaleInputNormalizer = new MathZ.ScalarNormalizer(0, 100, gameCircleXSizeMin, gameCircleXSizeMax);
        colorInputNormalizer = new MathZ.ScalarNormalizer(0, 100, 0, 1);
        TeleportButton.GetComponent<BoxCollider>().enabled = false;

      
        Reset();
          
}
    //IEnumerator Test()
    //{
    //    //yield return new WaitForSeconds(1);
    //    //Instantiate(Monster);
    //}
   
    public void Reset()
    {
       
        PlayerCircle.SetActive(true);
        CirclePanelMat.color = Color.gray;
        isScaleLocked = false;
        isColorLocked = false;
     
        TeleportButton.GetComponent<BoxCollider>().enabled = false;
     
        isLeversChangingScale = true;
        GameCircle.transform.localScale = new Vector3(
            Random.Range(-0.01367794f, -0.2026417f),
            Random.Range(-0.015f, -0.2f));   
        gameCircleMeshRend.material.color = new Color(Random.Range(0f, 1f),
            Random.Range(0f, 1f),
           .5f,1f);
     
    }

    public void UpdatePlayerCircleX(float scale)
    {
        if (!isScaleLocked)
        {
         
            playerXscale = scaleInputNormalizer.NormalizeScalar(scale);

            PlayerCircle.transform.localScale = new Vector3(playerXscale, PlayerCircle.transform.localScale.y, PlayerCircle.transform.localScale.z);
        }else if (!isColorLocked)
        {         
            playerXscale = colorInputNormalizer.NormalizeScalar(scale);
           CirclePanelMat.color = new Color(circlePanelmeshRend.material.color.r, playerXscale, circlePanelmeshRend.material.color.b, .5f);
            
        }

    }

    public void UpdatePlayerCircleY(float scale)
    {
        if (!isScaleLocked)
        {
            playerYscale = scaleInputNormalizer.NormalizeScalar(scale);
            PlayerCircle.transform.localScale = new Vector3(PlayerCircle.transform.localScale.x, playerYscale, PlayerCircle.transform.localScale.z);
        }else if (!isColorLocked)
        {
            playerYscale = colorInputNormalizer.NormalizeScalar(scale);        
            var newColor = new Color(playerYscale, circlePanelmeshRend.material.color.g, circlePanelmeshRend.material.color.b, .5f);
            CirclePanelMat.color = newColor;
            
        }

    }
    public void UpdateGameCircleColor(float color)
    {
        
        circlePanelmeshRend.material.color += new Color(0, color, 0);
    }

    public void LockButton()
    {
        if (isScaleLocked)
        {
            Debug.Log("LCOK Color");
            isColorLocked = true;
            var circleColor = gameCircleMeshRend.material.color;
            var playerColor= circlePanelmeshRend.material.color;
         //   Debug.Log("playerColor " + playerColor + " vs " + circleColor);
            colorVariance = Mathf.Abs(circleColor.r - playerColor.r) +
                Mathf.Abs(circleColor.b - playerColor.b);
           // Debug.Log("Color var: " + colorVariance);
            colorVariance *= 1.5f;
        }
        if (!isScaleLocked)
        {
            Debug.Log("LCOK SCALE");
            isScaleLocked = true;
            var circleScale = GameCircle.transform.localScale;
            var playerScale = PlayerCircle.transform.localScale;

            scaleVariance = (Mathf.Abs(
                Mathf.Abs(circleScale.x) - Mathf.Abs(playerScale.x)) +
              Mathf.Abs(
                  Mathf.Abs(circleScale.y) - Mathf.Abs(playerScale.y)));
            PlayerCircle.SetActive(false);

            Debug.Log("plyaer scale: " + playerScale + " vs " + circleScale + " = " + scaleVariance);
            
        }
        
        if(isScaleLocked && isColorLocked)
        {
            TransportReady();
        }
      
     
    }

    void TransportReady()
    {
        Debug.Log("TRANS RADY");
        TeleportButton.GetComponent<BoxCollider>().enabled = true;
        TeleportButton.GetComponent<MeshRenderer>().material.color = teleportReady;
    }

    public void Transport()
    {
    
        var variance = colorVariance + scaleVariance;
        TeleportScript.Teleport(variance);
        playerScore -= variance * 150;       
        Reset();
    }

    public IEnumerator OpenBlasterCase()
    {
        BlasterButton.GetComponent<BoxCollider>().enabled = true;
        BlasterButton.GetComponent<MeshRenderer>().material.color = blasterReady;
        Debug.Log("BlasterReady");

        //if (BlasterCase) Destroy(BlasterCase);

       yield return new WaitForEndOfFrame();
      //  var go = Instantiate(BlasterCase);
    //    go.transform.position = BlasterCaseTrans.position;
    //    go.transform.rotation = BlasterCaseTrans.rotation;

      //  BlasterCase = go;

     
    }

    public void OpenCase()
    {

        //BlasterButton.GetComponent<BoxCollider>().enabled = false;
        BlasterCase.GetComponent<BlasterCase>().Open();
    }
}
