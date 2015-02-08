using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AOEAttackScript))]
public class BlobDamageScript : MonoBehaviour {

    //blobs attack is on contact with the ground, and a slight AOE

    //calls AOEattackscript

    private EnemyBehaviour behaviour;
    private AOEAttackScript attackScript;

    private Color color = new Color(1, 0, 1);

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
