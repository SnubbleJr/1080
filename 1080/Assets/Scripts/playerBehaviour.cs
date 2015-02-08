using UnityEngine;
using System.Collections;

public class playerBehaviour : MonoBehaviour {

    public int currentHealth = 1080;
    public int health = 1080;
    public float sprintMultiplier = 1.5f;

    //static so head scripts can find it
    public static bool goneLimp = false;

    public bool canGunRun = false;
    public bool canTurret = false;

    private GameObject playerCamera;

    private CharacterMotor characterMotor;
    private ArrayList gunScripts = new ArrayList();
    
	// Use this for initialization
    void Start()
    {
        characterMotor = this.GetComponent<CharacterMotor>();

        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}

    void OnGUI()
    {
        GUI.skin = MenuSkin.menuSkin;
        GUI.color = Color.red;

        GUI.Label(new Rect(50, Screen.height - 50, 100, 50), (currentHealth.ToString() + "/" + health.ToString()));
    }

    void ApplyDamage(int damage)
    {
        currentHealth -= damage;

        //play damage sound

        //flash screen red

        //show damage arrow
    }

	// Update is called once per frame
    void Update()
    {
        //check for sprinting
        sprintManager();

        throwManager();

        checkHealth();
    }

    private void checkHealth()
    {
        //health boundry checking
        if (currentHealth < (health / 99) &! goneLimp)
        {
            //hurt, cripple the neck
            print("ow");
            BroadcastMessage("goLimp");

            goneLimp = true;
        }

        //heal check
        if (currentHealth > (health / 99) && goneLimp)
        {
            //healed enough, fix the head
            print("healz");
            BroadcastMessage("goHard");

            goneLimp = false;
        }

        if (currentHealth <= 0)
        {
            //game over
            print("ded");
            Destroy(this.gameObject);
        }
    }

    private void sprintManager()
    {
        //managhes all of the sprint state

        //starting of the sprint
        if (Input.GetButtonDown("Sprint"))
        {
            //don't do any gun play with we don't have a gun
            foreach(Gunscript gunScript in gunScripts)
            {
                //if we can't sprint with gun, throw it
                //play gun annimation to throw, that will then send a thrown message
                //pickupmanager should handle it
                if (!canGunRun)
                {
                    gunScript.playTrowingAnim();
                }
                //else, we can sprint with a gun
                else
                {
                    //play the start sprint animation
                    gunScript.playSprintStartAnim();

                    //queue up the looping mid sprint animation
                    gunScript.playSprintMidAnim();
                }
            }

            //clear out the gunscripts if no turret
            if (!canTurret)
            {
                gunScripts.Clear();
            }

            //here we actually set the speed and fov to change

            characterMotor.movement.maxForwardSpeed *= sprintMultiplier;
            characterMotor.movement.maxSidewaysSpeed *= sprintMultiplier;
            characterMotor.movement.maxBackwardsSpeed *= sprintMultiplier;

            playerCamera.camera.fieldOfView *= sprintMultiplier;

            //bobble screen, make the charcter rotate 90 degrees while running

        }


        //mid
        if (Input.GetButton("Sprint"))
        {
            //Throw the gun if we picked it back up while sprinting (assuming we can't gun run
            if (!canGunRun)
            {
                throwGun();
            }
        }

        //end
        if (Input.GetButtonUp("Sprint"))
        {

            //stop the gun animation, if gun is there
            foreach (Gunscript gunScript in gunScripts)
            {
                gunScript.playSprintEndAnim();
            }

            //revert everything

            characterMotor.movement.maxForwardSpeed /= sprintMultiplier;
            characterMotor.movement.maxSidewaysSpeed /= sprintMultiplier;
            characterMotor.movement.maxBackwardsSpeed /= sprintMultiplier;

            playerCamera.camera.fieldOfView /= sprintMultiplier;

        }
    }

    private void throwManager()
    {
        if (Input.GetButtonDown("Melee"))
        {
            throwGun();
        }
    }

    //throw the guns and clear the scripts if not turrets
    private void throwGun()
    {
        foreach (Gunscript gunScript in gunScripts)
        {
            gunScript.playTrowingAnim();
        }

        //clear out the gunscripts as we're not using them no more
        gunScripts.Clear();

    }

    //set our gun, given to us by pickup manager
    public void addGunScripts(Gunscript gScript)
    {
        gunScripts.Add(gScript);
    }

    public void setGunRun(bool value)
    {
        canGunRun = value;
    }

    public void SetCanTurret(bool value)
    {
        canTurret = value;
    }

    public bool getCanTurret()
    {
        return canTurret;
    }

    public void changeStats (PlayerUpgrade statDelta)
    {
        characterMotor.movement.maxForwardSpeed += statDelta.runSpeed;
        characterMotor.movement.maxSidewaysSpeed += statDelta.runSpeed;
        characterMotor.movement.maxBackwardsSpeed += statDelta.runSpeed;

        characterMotor.jumping.baseHeight += statDelta.jumpHeight;
        characterMotor.jumping.extraHeight += statDelta.jumpHeight;

        currentHealth += statDelta.healthCapacityIncrease;
        health += statDelta.healthCapacityIncrease;

        canGunRun = statDelta.canGunRun;
        canTurret = statDelta.canTurret;
    }
}
