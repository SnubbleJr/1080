using System;
using UnityEngine;
using System.Collections;

//player upgrades
[System.Serializable]
public class PlayerUpgrade
{
    public float runSpeed = 0f;
    public float jumpHeight = 0f;
    public int healthCapacityIncrease = 0;
    public bool canGunRun = false;
    public bool canTurret = false;
}

//gun upgrades
[System.Serializable]
public class GunStats
{
    public GameObject gun;
    public GameObject gunCrossHair;
    public Rigidbody bullet;
    public int ROF = 0;
    public float speed = 0f;
    public float damage = 0f;
    public int clipSize = 0;
    public int ammo = 0;
    public bool hasScope = false;
    public bool newPos = false;
    public Vector3 defaultPos = Vector3.zero;
}

public class UpgradeScript : MonoBehaviour
{

    //script that signifies which upgrade this pick up is

    //indicator for pickup manager
    public bool isAPlayerUpgrade = false;

    public PlayerUpgrade playerUpgrade;

    //indicator for pickup manager
    public bool isAGunUpgrade = false;

    public GunStats gunUpgrade;
}
