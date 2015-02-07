using UnityEngine;
using System.Collections;

public class PickupManagerRelayer : MonoBehaviour {

    //simple script to receive pickup messages and send it down

    private GameObject pickupManager;

    private PickupManagerBehaviour pMB;

    void Start()
    {
        pMB = GetComponentInChildren<PickupManagerBehaviour>();

        if (pMB == null)
        {
            Debug.LogError("No PickupManagerBehaviour found in player!");
        }
    }

    public void onPickupEnter(GameObject pickUp)
    {
        pMB.onPickupEnter(pickUp);
    }

    //called by pickup spawner to get a copy of the first current gun
    public GameObject getCurrentGun()
    {
        return pMB.getCurrentGun();
    }

    public int getGunScriptCount()
    {
        return pMB.getGunScriptCount();
    }

    public bool checkIfHasGun()
    {
        return pMB.getHasGun();
    }
}
