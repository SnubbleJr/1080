﻿using UnityEngine;
using System.Collections;

public class Gunscript : MonoBehaviour {

    public GameObject gun;
    public GameObject gunCrossHair;
    public Rigidbody bullet;
    public int gunNumber;

    public int ROF = 1;
    public float speed = 1f;
    public float damage = 0.5f;
    public int clipSize = 5;
    public int ammo = 50;

    public bool hasScope = false; //isn't used in this script, but is used for the main camera
    public Vector3 defaultPos = new Vector3(0.3366699f, -0.2802322f, 0.4769165f); //used for pickups

    private int currentClip = 0;

    private Transform tip;
    private float rofCounter = 1;

	// Use this for initialization
	void Start () {
	    if(bullet == null)
        {
            Debug.LogError("No bullet given!");
            this.enabled = false;
        }

        //find end of gun - for the time being is just Tip
        tip = transform.Find("Tip");

        if (tip == null)
        {
            Debug.LogError("No Tip found!");
            this.enabled = false;
        }

        //on pickup, play pickup animation
        gun.animation.Play("Picking up");
	}

    private void pickedUp()
    {
        //called at the end of the pickup animation
        //set the clip size so we can shoot, an eligant way to stop shooting while picking up.
        currentClip = clipSize;
    }

    void OnGUI()
    {
        GUI.skin = MenuSkin.menuSkin;
        GUI.color = Color.red;

        GUI.Label(new Rect(Screen.width - 100, Screen.height - 50, 100, 50), (currentClip.ToString() + "/" + ammo.ToString()));
    }

	void FixedUpdate () {

        aimGun();

        //sets the rate of fire, but shotting ROF times every second (50 fixed updates)
        float rofInverse = ROF / 50f;

        rofCounter += rofInverse;

        //if shoot pressed
        if (Input.GetButton("Fire1") || (Input.GetAxis("Fire1") == 1))
        {
            //only shoots when it's 'charged up'
            //dont by shooting when rof counter has gotten higher than 1
            //shooting resets counter

            //if bullets in gun
            if (currentClip > 0)
            {
                //if can shoot
                if (rofCounter >= 1)
                {
                    shoot();

                    //set counter back to 0
                    rofCounter = 0;
                }
            }
            else
            {
                playReloadAnim();
            }
        }

        //if hit reload && fired bullets
        if (Input.GetButtonDown("Reload") && currentClip != clipSize)
            playReloadAnim();
	}

    void aimGun()
    {
        //making the gun aim at the centre of the screen
        //it a triangle, the angle the gun needs to turn is tan(the ditance to the target / distance from gun to camera)

        //if we are using the normal camera
        if (Camera.main)
        {
            Vector3 target;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0));
            RaycastHit hit;

            // bit shift the index of the layer to get a bit mask
            int layerMask = 1 << 8;
            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            layerMask = ~layerMask;

            //if we can hit something
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                print(hit.transform.gameObject.name);

                //make the gun aim at the point we can hit - the offset due to the tip's coords
                target = hit.point + new Vector3(0, transform.localPosition.y, 0);
                Debug.DrawLine(ray.origin, hit.point);
            }
            else
                //make it return to the centre of the screen, also makes it look at the camera funkily
                target = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 30));

            //smoothing of gun motion
            var targetRotation = Quaternion.LookRotation(target - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * 5 * Time.deltaTime);
        }
    }

    void shoot()
    {
        Rigidbody shootyBullet;
        //spawn bullet
        shootyBullet = Instantiate(bullet, tip.transform.position, transform.rotation) as Rigidbody;
        shootyBullet.velocity = transform.TransformDirection(Vector3.forward * speed);

        currentClip--;

        //play gun shot animation
        gun.animation.Play("Shooting");
    }

    public void playTrowingAnim()
    {
        gun.animation.Play("Throwing");
    }

    public void playSprintStartAnim()
    {
        gun.animation.Play("Sprint start");
    }
    
    public void playSprintMidAnim()
    {
        //queue the mid
        gun.animation.PlayQueued("Sprinting", QueueMode.CompleteOthers);
    }

    public void playSprintEndAnim()
    {
        gun.animation.Play("Sprint end");
    }

    public void playReloadAnim()
    {
        //if any ammo
        if (ammo > 0)
        {
            currentClip = 0;

            //play animation
            gun.animation.Play("Reloading");

            //reload is called at the end of the animation
        }
    }

    public void reloaded()
    {
        int ammoUsed = (clipSize - currentClip);
        //if enough ammo
        if (ammo > ammoUsed)
        {
            ammo -= ammoUsed;
            currentClip = clipSize;
        }
        else
        {
            //fill by ammo ammount
            currentClip = ammo;
            ammo = 0;
        }

        //return to idle animation
        gun.animation.Play("Idle");
    }

    public void changeStats(GunStats statDelta)
    {
        //replacing of gameobjects/rigidbodies
        if (statDelta.gun != null)
            gun = statDelta.gun;
        if (statDelta.gunCrossHair != null)
            gunCrossHair = statDelta.gunCrossHair;
        if (statDelta.bullet != null)
            bullet = statDelta.bullet;

        //updating stats
        ROF += statDelta.ROF;
        speed += statDelta.speed;
        damage += statDelta.damage;
        clipSize += statDelta.clipSize;
        ammo += statDelta.ammo;
        hasScope = statDelta.hasScope;

        if (statDelta.newPos)
            defaultPos = statDelta.defaultPos;
    }
}
