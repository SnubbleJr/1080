using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AOEAttackScript))]
public class RolsonDamageScript : MonoBehaviour {

    //rolsons attack when close enough to the player, givin an AOE

    //calls AOEattackscript

    private EnemyBehaviour behaviour;
    private AOEAttackScript attackScript;

    private Color color = Color.red;

    // Use this for initialization
    void Start()
    {
        //hook up the scripts
        behaviour = GetComponent<EnemyBehaviour>();
        behaviour.enemyAttackDelegate += attack;

        attackScript = GetComponent<AOEAttackScript>();
    }

    void OnDestroy()
    {
        behaviour.enemyAttackDelegate -= attack;
    }

    void attack(float damage, float range)
    {
        attackScript.aoeAttack(transform, damage, range, color);
    }
}
