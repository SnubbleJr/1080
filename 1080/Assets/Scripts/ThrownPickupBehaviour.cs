using UnityEngine;
using System.Collections;

public class ThrownPickupBehaviour : MonoBehaviour {

    //enables the pick up to be picked up after it hits the ground

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Invoke("enablePickup", 1f);
        }
    }

    void enablePickup()
    {
        //set the opject upright
        transform.rotation = Quaternion.identity;
        
        //apply a force; make it hop up
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;

        //make it rotate and bob
        GetComponent<SlowlyRotate>().enabled = true;
        GetComponent<SlowlyBob>().enabled = true;

        //set colliders to true
        GetComponent<BoxCollider>().enabled = true;
    }
}
