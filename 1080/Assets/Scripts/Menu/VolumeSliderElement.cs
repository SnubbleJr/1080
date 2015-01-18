using UnityEngine;
using System.Collections;

public class VolumeSliderElement : MonoBehaviour {

    public static float volume = 1f;

    //when the gameobject this is attached to is activated, we do this 
    public void goTime()
    {
        print("volume was: " + volume);
        //set the volume to our value, based on our distance :3
        volume = Mathf.Abs(transform.position.x) / transform.parent.position.x;
        print("volume is: " + volume);
    }
}
