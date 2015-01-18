using UnityEngine;
using System.Collections;

public class PickupManagerBehaviour : MonoBehaviour {

    //need to disable if no gun or no gun with scope
    private ScopeScript scopeScript;

    //what sort of scripts pickups can have
    private Gunscript pickUpGunScript;
    //gun upgrade script
    //amm script

    private bool hasGun = false;

    private Gunscript currentGunScript;

    //manages the players pickups, and enables stuff when hit by it

	// Use this for initialization
	void Start () {
        scopeScript = this.GetComponent<ScopeScript>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void onPickupEnter(GameObject pickUp)
    {
        //the pickup has sent us the prefab it was holding

        if(pickUp.tag == "Gun")
        {
            //we have found a gun
            pickUpGunScript = pickUp.GetComponent<Gunscript>();

            //reenable the gunscript
            pickUpGunScript.enabled = true;

            //only pick up if we don't already ahve one
            if (!hasGun)
            {
                //let's pick it up!
                pickUp.transform.parent = this.transform;
                pickUp.transform.localPosition = pickUpGunScript.defaultPos;

                //update state of script, scope and gun

                currentGunScript = pickUpGunScript;
                hasGun = true;

                //need to disable scope script unless we have gun with scope
                if (currentGunScript.hasScope)
                {
                    scopeScript.enabled = true;
                }
                else
                {
                    scopeScript.enabled = false;
                }
            }
            //else just take its ammo and delete it
            //maybe unless we have duel weilding?
            //need to add that in
            else
            {
                currentGunScript.ammo += pickUpGunScript.ammo;
                Destroy(pickUp);
            }


        }
        print("we have a " + pickUp.gameObject.name);
    }
}
