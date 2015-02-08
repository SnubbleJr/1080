using UnityEngine;
using System.Collections;

public class PickupManagerBehaviour : MonoBehaviour {

    //for when we want to throw the gun
    public GameObject emptyPickUpContainer;

    //need to disable if no gun or no gun with scope
    private ScopeScript scopeScript;

    //what sort of scripts pickups can have
    private Gunscript pickUpGunScript;
    private UpgradeScript pickUpUpgradeScript;
    
    private ArrayList currentGunScripts = new ArrayList(); //for us to alter current gun state, like giving it more ammo

    private playerBehaviour playBehaviour;

    private GameObject gunPickup;

    //manages the players pickups, and enables stuff when hit by it

	// Use this for initialization
	void Start () {
        scopeScript = this.GetComponent<ScopeScript>();

        playBehaviour = this.GetComponentInParent<playerBehaviour>();

        makeThrowableGunHolder();
	}

    void makeThrowableGunHolder()
    {
        //spawn our pickup so we can move it later
        gunPickup = Instantiate(emptyPickUpContainer, transform.position, transform.rotation) as GameObject;
        gunPickup.transform.parent = transform;

        PickupBehaviour pickupBehaviour = gunPickup.GetComponent<PickupBehaviour>();
        pickupBehaviour.enabled = false;
        gunPickup.collider.enabled = false;
    }

	// Update is called once per frame
	void Update () {
	}

    public void onPickupEnter(GameObject pickUp)
    {
        //the pickup has sent us the prefab it was holding

        switch (pickUp.tag)
        {
            case "Gun":
                addGun(pickUp);
                break;

            case "Upgrade":
                parseUpgrade(pickUp);
                break;
        }

        print("we have a " + pickUp.gameObject.name);
    }
    
    private void addGun(GameObject pickUp)
    { 
        //we have found a gun
        pickUpGunScript = pickUp.GetComponent<Gunscript>();

        //reenable the gunscript
        pickUpGunScript.enabled = true;
        
        Gunscript currentGunScript = null;

        //only pick up if we don't already have this gun
        foreach (Gunscript gunScript in currentGunScripts)
        {
            if (gunScript.gunNumber == pickUpGunScript.gunNumber)
                currentGunScript = gunScript;
        }

        //is a new gun
        if (currentGunScript == null)
        {
            //let's pick it up!
            pickUp.transform.parent = gunPickup.transform;
            pickUp.transform.localPosition = pickUpGunScript.defaultPos;
            
            //update state of script, scope and gun
            
            //add gun to current gun array list
            currentGunScripts.Add(pickUpGunScript);

            //set current gun script
            currentGunScript = pickUpGunScript;

            //need to disable scope script unless we have gun with scope
            if (currentGunScript.hasScope)
            {
                scopeScript.enabled = true;
            }
            else
            {
                scopeScript.enabled = false;
            }

            //inform the player behaviour of our new gun
            playBehaviour.addGunScripts(currentGunScript);
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
    
    private void parseUpgrade(GameObject pickUp)
    {
        //we have found a gun
        pickUpUpgradeScript = pickUp.GetComponent<UpgradeScript>();
        
        //apply upgrades

        //player upgrades
        if (pickUpUpgradeScript.isAPlayerUpgrade == true)
        {
            playBehaviour.changeStats(pickUpUpgradeScript.playerUpgrade);
        }

        //gun upgrades
        if (pickUpUpgradeScript.isAGunUpgrade == true)
        {
            foreach (Gunscript gunScript in currentGunScripts)
            {
                gunScript.changeStats(pickUpUpgradeScript.gunUpgrade);
            }
        }

    }

    //called from throwing animation, make a pickup for our gun
    //by deparenting it, and wrapping it into a new pickup object
    //and add some force to it
    public void gunThrown()
    {
        //disabling our current gun - if the player can't turret, else, they can still fire while on floor!

        if (!playBehaviour.getCanTurret())
        {
            foreach (Gunscript gunScript in currentGunScripts)
            {
                gunScript.enabled = false;
            }
        }

        PickupBehaviour pickupBehaviour = gunPickup.GetComponent<PickupBehaviour>();
        pickupBehaviour.enabled = true;
        gunPickup.collider.enabled = false;
        
        //deparenting and exploding it
        gunPickup.transform.parent = null;
        gunPickup.rigidbody.isKinematic = false;
        gunPickup.rigidbody.useGravity = true;

        foreach (Gunscript gunScript in currentGunScripts)
        {
            //set animaiton to idle
            gunScript.gun.animation.Play("Idle");

            //moving gun to the centre of the pickup
            gunScript.gameObject.transform.localPosition = Vector3.zero;


            CapsuleCollider gunCollider = gunPickup.AddComponent<CapsuleCollider>();
            gunCollider.direction = 2;
            gunCollider.height = 1.5f;
            Physics.IgnoreCollision(gunCollider, transform.root.collider);

            //explosion is offset to be relatively behind the gun
            gunCollider.rigidbody.AddExplosionForce(3000.0f, (transform.position - (gunPickup.transform.forward * 10f)), 20.0f, 3.0f);
            
        }
        
        //clear currentgunscript list
        currentGunScripts.Clear();

        //spawn new throwable gun holder
        makeThrowableGunHolder();
    }

    //called by relayer, generates a copy of the first gun in gunscripts (if any) 
    public GameObject getCurrentGun()
    {
        if (currentGunScripts.Count <= 0)
            return null;

        Gunscript gunScript = (Gunscript)currentGunScripts[0];
        GameObject gun = gunScript.gameObject as GameObject;
        
        //find the scope camera and rt, and deactivate it

        foreach (Transform child in gun.transform)
        {
            if (child.CompareTag("ScopeCamera"))
            {
                child.gameObject.SetActive(false);
            }
        }
        
        return gun;
    }

    public int getGunScriptCount()
    {
        return currentGunScripts.Count;
    }

    public bool getHasGun()
    {
        return (getGunScriptCount() > 0);
    }
}
