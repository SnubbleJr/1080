using UnityEngine;
using System.Collections;

public class HeadDamageScript : MonoBehaviour {

    //used to make the head go limb if too much damage hass occured

    Rigidbody rigidbody;
    Transform originalTrans;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        originalTrans = transform;
	}
	
    public void goLimp()
    {
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
    }

    public void goHard()
    {
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;

        transform.position = originalTrans.position;
        transform.rotation = originalTrans.rotation;
    }
}
