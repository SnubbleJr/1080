using UnityEngine;
using System.Collections;

public class PickupSpawner : MonoBehaviour {

    //creates a pick up, generates it's stats and then puts it into a pickup object
    public GameObject pickupContainer;

    private int gunCapNo = 4; // max amount of guns player can carry
    private PickupManagerRelayer pMR;

    void Start()
    {
        try
        {
            pMR = GameObject.FindGameObjectWithTag("Player").GetComponent<PickupManagerRelayer>();
        }
        catch
        {
            UnityEngine.Debug.LogError("No player in scene!");
            this.enabled = false;
        }

        InvokeRepeating("spawnPickup", 5, 5);
    }

    public void spawnPickup(Transform location)
    {
        //create the core of the pick up
        GameObject pickup = Instantiate(pickupContainer, location.position, Quaternion.identity) as GameObject;
        GameObject pickupContent = null;

        //make a random number from 0 to rngChance, the lowwer the number, the higher the chance
        int rngChance = 100;

        //random numbers to decide if to make a pickup
        //if we get an 8 (because 8's are awesome) spawn
        if ((int)Random.Range(0, rngChance) == 8)
        {
            //random number to decide what to spawn
            int spawnNo = (int)Random.Range(0, 2);

            switch(spawnNo)
            {
                case 0:
                    //make a gun - spawn gun if we already have one
                    if (pMR.checkIfHasGun())
                        pickupContent = createGunPickup();
                    else
                    {
                        //else, don't make anything
                        Destroy(pickup);
                        return;
                    }
                    break;

                default:
                    pickupContent = createUpgradePickup();
                    break;
            }            
        }

        pickupContent.transform.parent = pickup.transform;
        pickupContent.transform.localPosition = Vector3.zero;

    }

    //makes a gun pikcup - will be next one in the list to allow for multi weilding, if cap is hit, then it just gives ammo
    private GameObject createGunPickup()
    {
        //get a copy of the players gun
        GameObject newGun = Instantiate(pMR.getCurrentGun(), transform.position, Quaternion.identity) as GameObject;
        Gunscript newGunScript = newGun.GetComponent<Gunscript>();

        //create a new gun with gun number equal to number of guns held, unless its at the cap
        int gunNo = pMR.getGunScriptCount();
        
        //cap off gun number
        if (gunNo >= gunCapNo)
            gunNo = gunCapNo-1;

        //processing of the scaling and rotation
        Vector3 scaleVector = newGun.transform.localScale;

        //if no is bigger than 1, then flip it by inverting y scale and * the y transform by -1
        if (gunNo > 1)
        {
            scaleVector.y *= -1;
            newGunScript.defaultPos.y *= -1;
        }

        //if no is odd, then flip it by inverting the x scale and * the x transform by -1
        if (gunNo % 2 != 0)
        {
            scaleVector.x *= -1;
            newGunScript.defaultPos.x *= -1;
        }

        newGunScript.gunNumber = gunNo;
        newGun.transform.localScale = scaleVector;

        newGunScript.enabled = false;

        return newGun;
    }

    private GameObject createUpgradePickup()
    {
        GameObject upgrade = new GameObject();
        upgrade.AddComponent<UpgradeScript>();

        UpgradeScript upgradeScript = upgrade.GetComponent<UpgradeScript>();

        //roll a dice to decide which upgrade it is
        int upgradeNo = (int)Random.Range(0, 6);

        //prototype spawning here - need to mamke better
        //if even, gun, else player
        if (upgradeNo % 2 == 0)
        {
            //gun
        }
        else
        {
            //player
        }

        Instantiate(upgrade, transform.position, Quaternion.identity);
        return upgrade;
    }
}
