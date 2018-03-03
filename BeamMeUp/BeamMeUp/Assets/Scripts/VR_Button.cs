//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using VRTK;
//using VRTK.UnityEventHelper;

//public class VR_Button : MonoBehaviour
//{
//    GameScript game;
//    private VRTK_Button_UnityEvents buttonEvents;

//    private void Start()
//    {
//        game = GameObject.Find("GameScript").GetComponent<GameScript>();
//        buttonEvents = GetComponent<VRTK_Button_UnityEvents>();
//        if (buttonEvents == null)
//        {
//            buttonEvents = gameObject.AddComponent<VRTK_Button_UnityEvents>();
//        }
//        buttonEvents.OnPushed.AddListener(handlePush);
//    }

//    private void handlePush(object sender, Control3DEventArgs e)
//    {
//        VRTK_Logger.Info("Pushed");
//        game.LockScale();
        
//    }
//}


