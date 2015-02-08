using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BlobMovmentScript : MonoBehaviour {

    //blobs movment is parabolic, launching very high into the air

    private EnemyBehaviour behaviour;
    private float distToGround;
    private bool inAir = false;

	// Use this for initialization
	void Start () 
    {
        //hook up the script
        behaviour = GetComponent<EnemyBehaviour>();
        behaviour.enemyMovementDelegate += move;

        // get the distance to ground
        distToGround = collider.bounds.extents.y;
	}
	
    void OnDestroy()
    {
        behaviour.enemyMovementDelegate -= move;
    }

	// Update is called once per frame
	void Update () 
    {
        print(inAir);    
        if (isGrounded() && inAir)
            landed();
	}

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    void landed()
    {
        inAir = false;

        //time to attack!
        behaviour.attack(); 
    }

    //time in the air is consistant, the only unkown is the veclocity we need
    void move(float speed)
    {
        if (!inAir)
        {
            inAir = true;

            Vector3 target = behaviour.getPlayer().transform.position;
            Vector3 origin = transform.position;
            Vector3 gravity = Physics.gravity;
            float airTime = 1f / speed;

            Vector3 force = new Vector3();

            force.x = (target.x - origin.x) / airTime;
            force.z = (target.z - origin.z) / airTime;
            force.y = ((target.y + (-0.5f * gravity.y) * airTime * airTime) - origin.y) / airTime;

            rigidbody.velocity = force;
        }
    }
}

