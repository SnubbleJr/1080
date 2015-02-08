using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class RolsonMovementScript : MonoBehaviour {

    //rolsons movment is towards the player, applying a force, never seems to get there

    private EnemyBehaviour behaviour;
    private GameObject player;
    private float attackRange;

    // Use this for initialization
    void Start()
    {
        //hook up the script
        behaviour = GetComponent<EnemyBehaviour>();
        behaviour.enemyMovementDelegate += move;

        attackRange = behaviour.stats.attackRange;
        player = behaviour.getPlayer();
    }

    void OnDestroy()
    {
        behaviour.enemyMovementDelegate -= move;
    }

    // Update is called once per frame
    void Update()
    {
        if (withinRange(attackRange))
            attack();
    }

    bool withinRange(float range)
    {
        //if near within range
        return true;
    }

    void attack()
    {
        //time to attack!
        behaviour.attack();
    }

    //time in the air is consistant, the only unkown is the veclocity we need
    void move(float speed)
    {
        Vector3 target = player.transform.position;
        Vector3 origin = transform.position;
        Vector3 gravity = Physics.gravity;
        float airTime = 1f / speed;

        Vector3 force = new Vector3();

        force.x = (target.x - origin.x) / airTime;
        force.z = (target.z - origin.z) / airTime;

        rigidbody.AddForce(force, ForceMode.VelocityChange);
    }
}
