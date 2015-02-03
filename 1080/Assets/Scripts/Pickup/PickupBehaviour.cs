using UnityEngine;
using System.Collections;

public class PickupBehaviour : MonoBehaviour {

    private ArrayList pickups = new ArrayList();
    private ArrayList upgradeScripts = new ArrayList();

    //simple pickup holder, just have the pickup as a child

	// Use this for initialization
	void Start () {
	    //if we don't have any children, disable
        if (transform.childCount < 1)
        {
            Debug.LogError("Pickup managed to spawn without any children!");
            this.enabled = false;
        }

        //else, grab that kid
        foreach (Transform child in transform)
        {
            pickups.Add(child.gameObject);

            //we should check to see if this a upgrade, if it is, get its script also
            if (child.gameObject.CompareTag("Upgrade"))
                upgradeScripts.Add(child.gameObject.GetComponent<UpgradeScript>());
        }
	}
	
    void OnTriggerEnter(Collider other)
    {
        PickupManagerRelayer pMR;

        //if the player hit us, inform them of it, and send them the pickup
        if (other.CompareTag("Player"))
        {
            //grab the player relayer to talk to player
            pMR = other.gameObject.GetComponent<PickupManagerRelayer>();
            
            //do extra checking if this pickup is a upgrade
            if (upgradeScripts.Count > 0)
            {
                foreach (UpgradeScript upgradeScript in upgradeScripts)
                {
                    //we don't activate if it is part gun upgrade and no gun is has
                    if (upgradeScript.isAGunUpgrade == true && pMR.checkIfHasGun() == false)
                        return;
                }
            }

            //activate
            transform.DetachChildren();

            foreach (GameObject pickup in pickups)
            {
                pMR.onPickupEnter(pickup);
            }

            Destroy(this.gameObject);
        }
    }
}
