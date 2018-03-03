
using UnityEngine;
using VRTK.UnityEventHelper;
using VRTK;


public class ControlReactorRight : MonoBehaviour
{
    GameScript gameScript;
    GameObject gameCircle;
    private VRTK_Control_UnityEvents controlEvents;

    private void Start()
    {
        gameScript = GameObject.Find("GameScript").GetComponent<GameScript>();
        gameCircle = gameScript.PlayerCircle;     
        controlEvents = GetComponent<VRTK_Control_UnityEvents>();
        if (controlEvents == null)
        {
            controlEvents = gameObject.AddComponent<VRTK_Control_UnityEvents>();
        }

        controlEvents.OnValueChanged.AddListener(HandleChange);
    }

    private void HandleChange(object sender, Control3DEventArgs e)
    {    
        gameScript.UpdatePlayerCircleY(e.normalizedValue);
      
    }
}
