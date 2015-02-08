using UnityEngine;
using System.Collections;

public class RolsonDeathScript : MonoBehaviour {

    //rolsons currently just disapear on death

    private EnemyBehaviour behaviour;

    // Use this for initialization
    void Start()
    {
        //hook up the movement script
        behaviour = GetComponent<EnemyBehaviour>();
        behaviour.enemyDeathDelegate += die;
    }

    void OnDestroy()
    {
        behaviour.enemyDeathDelegate -= die;
    }

    void die()
    {
        Destroy(this.gameObject);
    }
}
