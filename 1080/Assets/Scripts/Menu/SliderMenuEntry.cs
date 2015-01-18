using UnityEngine;
using System.Collections;

public class SliderMenuEntry : MonoBehaviour {

    //workd ontop of menu entry, disables updown of menu script for our own needs

    public bool selected = false;
    	
	// Update is called once per frame
	void Update () {
        //exit on input (assuming we have actually been selected!
        if (Input.GetButtonDown("Jump") && selected)
            Invoke("deactivate", 0);
	}

    //called with invoke, so eveything works in order
    void activate()
    {
        selected = true;

        //activate menu of our own
        this.GetComponent<MenuScript>().enabled = true;

        //send a message to the parent menu system that we're in controll now, and don't act apon button presses 
        transform.parent.gameObject.SendMessage("setControllable", false);

        //disable menu entry script
        this.GetComponent<MenuEntry>().enabled = false;
    }

    //called with invoke, so eveything works in order
    void deactivate()
    {
        //set selected
        selected = false;

        //deactivate menu of our own
        this.GetComponent<MenuScript>().enabled = false;

        //give control back to the menu after we're done
        transform.parent.gameObject.SendMessage("setControllable", true);

        //renable our menu entry script
        this.GetComponent<MenuEntry>().enabled = true;
    }

    //when the gameobject this is attached to is activated, we do this 
    public void goTime()
    {
        //set selected   
        Invoke("activate", 0);
    }
}
