using UnityEngine;
using System.Collections;

public class lockCursor : MonoBehaviour 
{
    private bool locked = true;

    void Update()
    {
        if (Input.GetButtonDown("escape"))
        {
            locked = !locked;
        }
        Screen.lockCursor = locked;
    }
}
