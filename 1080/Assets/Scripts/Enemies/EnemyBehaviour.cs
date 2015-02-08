using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnemyStats
{
    public int health = 100;
    public Rigidbody projectile;
    public float attackDamage = 0;
    public float attackFrequency = 0;
    public float attackRange = 0;
    public float moveSpeed = 0;
    public float moveFrequency = 0;
}

public class EnemyBehaviour : MonoBehaviour {

    //the base eneymey behaviour, just ccalls move and attack at intervals
    //also fires of dead message
    //delegates are subscribed from other scripts

    public int currentHealth;

    public EnemyStats stats;

    public delegate void moveDelegate(float speed);
    public delegate void attackDelegate(float damage, float range);
    public delegate void deathDelegate();

    public moveDelegate enemyMovementDelegate;
    public attackDelegate enemyAttackDelegate;
    public deathDelegate enemyDeathDelegate;

    private GameObject player;

	// Use this for initialization
	void Awake () 
    {
        //set to full health
        currentHealth = stats.health;

        //find player
        player = GameObject.FindGameObjectWithTag("Player");
        
        //call the approriate delegate based on frequency

        //if the frequency is negative, then the script isn't called this way - it's called via the alternate function
        //e.g. blob script that attacks on contact

        if (stats.attackFrequency >= 0)
            InvokeRepeating("attack", Random.Range(0f, 1f), stats.attackFrequency);

        if (stats.moveFrequency >= 0)
            InvokeRepeating("move", Random.Range(0f, 1f), stats.moveFrequency);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (checkAlive() == false)
            enemyDeathDelegate();
	}

    bool checkAlive()
    {
        return (currentHealth > 0);
    }

    //simple script that just calls the attack delegate, can be called externally
    public void attack()
    {
        enemyAttackDelegate(stats.attackDamage, stats.attackRange);
    }

    //simple script that just calls the move delegate, can be called externally
    public void move()
    {
        enemyMovementDelegate(stats.moveSpeed);
    }

    public GameObject getPlayer()
    {
        return player;
    }
}
