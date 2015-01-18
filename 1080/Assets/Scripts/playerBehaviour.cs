using UnityEngine;
using System.Collections;

public class playerBehaviour : MonoBehaviour {

    public int currentHealth = 1080;
    public int health = 1080;

	// Use this for initialization
	void Start () {
	
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

        //health boundry checking
        if (currentHealth < (health / 99))
            //hurt, cripple the neck
            print("ow");

        if (currentHealth <= 0)
        {
            //game over
            print("ded");
        }
    }

}
