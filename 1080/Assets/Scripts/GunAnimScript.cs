using UnityEngine;
using System.Collections;

public class GunAnimScript : MonoBehaviour {

    void reload()
    {
        //send relaod message to above
        transform.parent.SendMessage("reload");
    }
}
