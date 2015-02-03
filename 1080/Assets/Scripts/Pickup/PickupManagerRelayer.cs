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

    public bool checkIfHasGun()
    {
        return pMB.getHasGun();
    }
}
