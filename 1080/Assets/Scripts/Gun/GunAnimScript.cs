using UnityEngine;
using System.Collections;

public class GunAnimScript : MonoBehaviour {

    //deals with all the animation events, such as finished reload
    //also plays the sounds

    void reloadEnd()
    {
        //called by animation event in the reload animation
        //send relaod message to above
        transform.parent.SendMessage("reloaded");
    }

    void pickUpEnd()
    {
        //called by animation event in the pickup animation
        //send pickup message to above
        transform.parent.SendMessage("pickedUp");
    }

    void throwingEnd()
    {
        //called by animation event in the throwing animation
        //send thrwon message to above
        SendMessageUpwards("gunThrown");
    }
}
