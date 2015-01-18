using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    public float size = 1f;
    public bool rotate = false;
    public float timeout = 3f;

	// Use this for initialization
    void Start()
    {
        Destroy(gameObject, timeout);
	}
	
	// Update is called once per frame
	void Update () {
        if (rotate)
            transform.Rotate((Vector3.up * 10), Space.World);
	}

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        Destroy(this);
    }
}
