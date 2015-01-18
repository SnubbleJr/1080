using UnityEngine;
using System.Collections;

public class SettingsGoBackScript : MonoBehaviour {

    private GameObject settingsMenu;

    //simple script that just asks for the users go head, then closes the menu

	// Use this for initialization
	void Start () {
        settingsMenu = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //when the gameobject this is attached to is activated, we do this 
    public void goTime()
    {
        //first ask for users permission

        if (settingsMenu != null)
            settingsMenu.SetActive(false);
    }
}
