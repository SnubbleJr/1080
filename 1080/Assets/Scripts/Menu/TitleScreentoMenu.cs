using UnityEngine;
using System.Collections;

public class TitleScreentoMenu : MonoBehaviour {

    public GameObject mainMenu;

    //when the gameobject this is attached to is activated, we do this 
    public void goTime()
    {
        if (mainMenu != null)
        {
            //find and enable mainmenu
            mainMenu.SetActive(true);

            //disable us
            this.transform.parent.gameObject.SetActive(false);
        }
    }
}
