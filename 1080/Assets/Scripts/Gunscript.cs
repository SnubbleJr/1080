using UnityEngine;
using System.Collections;

public class Gunscript : MonoBehaviour {

    public GameObject gun;
    public GameObject gunCrossHair;
    public Rigidbody Bullet;
    public int ROF = 1;
    public float speed = 1f;
    public int clipSize = 5;
    public int ammo = 50;

    private int currentClip;

    private Transform tip;
    private float rofCounter = 1;

	// Use this for initialization
	void Start () {
	    if(Bullet == null)
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

        currentClip = clipSize;
	}

    void OnGUI()
    {
        GUI.skin = MenuSkin.menuSkin;
        GUI.color = Color.red;

        GUI.Label(new Rect(Screen.width - 100, Screen.height - 50, 100, 50), (currentClip.ToString() + "/" + ammo.ToString()));
    }

	void FixedUpdate () {

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
                //make it return to the cedntre of the screen, also makes it look at the camera funkily
                target = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 30));

            //smoothing of gun motion
            var targetRotation = Quaternion.LookRotation(target - transform.position);
       
            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * 5 * Time.deltaTime);
        }

        //sets the rate of fire, but shotting ROF times every second (50 fixed updates)
        float rofInverse = ROF/50f;

        rofCounter += rofInverse;

        //only shoots when it's 'charged up'
        //dont by shooting when rof counter has gotten higher than 1
        //shooting resets counter
        
        //if shoot
        if (Input.GetButton("Fire1") || (Input.GetAxis("Fire1") == 1))
        {
            //if bullets in gun
            if(currentClip > 0)
            {
                //if can shoot
                if (rofCounter >= 1)
                {
                    Rigidbody bullet;
                    //spawn bullet
                    bullet = Instantiate(Bullet, tip.transform.position, transform.rotation) as Rigidbody;
                    bullet.velocity = transform.TransformDirection(Vector3.forward * speed);

                    //set counter back to 0
                    rofCounter = 0;

                    currentClip--;
                }
            }
            else
            {
                reloadAnim();
            }
        }

        //if hit reload && fired bullets
        if (Input.GetButtonDown("Reload") && currentClip != clipSize)
            reloadAnim();
	}

    void reloadAnim()
    {
        //if any ammo
        if (ammo > 0)
        {
            currentClip = 0;

            //play animation
            gun.animation.Play("Reload");

            //reload is called at the end of the animation
        }
    }

    public void reload()
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
    }
}
