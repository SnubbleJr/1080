using UnityEngine;
using System.Collections;

public class PickupBehaviour : MonoBehaviour {

    private GameObject pickup;

    //simple pickup holder, just have the pickup as a child

	// Use this for initialization
	void Start () {
	    //if we don't have any children, disable
        if (transform.childCount < 1)
        {
            Debug.LogError("Pickup managed to spawn without any children!");
            this.enabled = false;
        }

        pickup = transform.GetChild(0).gameObject;
	}
	
    void OnTriggerEnter(Collider other)
    {
        PickupManagerRelayer pMR;

        //if the player hit us, inform them of it, and send them the pickup
        if (other.CompareTag("Player"))
        {
            pMR = other.gameObject.GetComponent<PickupManagerRelayer>();
            transform.DetachChildren();
            pMR.onPickupEnter(pickup);
            Destroy(this.gameObject);
        }
    }
}
