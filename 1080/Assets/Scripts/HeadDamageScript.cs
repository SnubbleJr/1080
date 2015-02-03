using UnityEngine;
using System.Collections;

public class HeadDamageScript : MonoBehaviour {

    //used to make the head go limb if too much damage hass occured
    //also makes neck wobble when limp

    ConfigurableJoint joint;
    CharacterController cc;

	// Use this for initialization
	void Start () 
    {
        joint = GetComponent<ConfigurableJoint>();
        
        cc = transform.root.GetComponent<CharacterController>();
        
        goHard();
	}

    void FixedUpdate()
    {
        //movement based wobble

        if (playerBehaviour.goneLimp)
        {
            rigidbody.AddTorque(cc.velocity * -5);
        }
    }
	
    public void goLimp()
    {
        joint.angularXMotion = ConfigurableJointMotion.Limited;
        joint.angularYMotion = ConfigurableJointMotion.Limited;
    }

    public void goHard()
    {
        joint.angularXMotion = ConfigurableJointMotion.Locked;
        joint.angularYMotion = ConfigurableJointMotion.Locked;
    }
}
