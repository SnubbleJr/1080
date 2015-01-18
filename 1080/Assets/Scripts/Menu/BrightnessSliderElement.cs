using UnityEngine;
using System.Collections;

public class BrightnessSliderElement : MonoBehaviour {

    public static float brightness = 1f;

    //when the gameobject this is attached to is activated, we do this 
    public void goTime()
    {
        print("brightness was: " + brightness);
        //set the volume to our value, based on our distance :3
        brightness = Mathf.Abs(transform.position.x) / transform.parent.position.x;
        print("brightness is: " + brightness);
    }
}
