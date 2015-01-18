using UnityEngine;
using System.Collections;

public class ScopeScript : MonoBehaviour {

    private GameObject scope;
    private bool scoped = false;
    private bool m_isAxisInUse = false;

	// Use this for initialization
	void Start () {
        scope = GameObject.Find("ScopeCamera");
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire2") || Input.GetAxis("Fire2") == 1)
        {
            if (m_isAxisInUse == false)
            {
                // Call your event function here.
                m_isAxisInUse = true;
                scoped = !scoped;
                setScope();
            }
        }
        else
        {
            m_isAxisInUse = false;
        }
	}

    void setScope()
    {
        camera.enabled = scoped;
        scope.SetActive(!scoped);
    }
}
