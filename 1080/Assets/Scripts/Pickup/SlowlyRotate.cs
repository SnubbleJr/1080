using UnityEngine;
using System.Collections;

public class SlowlyRotate : MonoBehaviour {

    public bool rotateOnX;
    public float xSpeed;
    public bool rotateOnY;
    public float ySpeed;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (rotateOnX)
    		transform.Rotate(Time.deltaTime*xSpeed, 0, 0);
	    if (rotateOnY)
            transform.Rotate(0, Time.deltaTime*ySpeed, 0, Space.World);
	}
}
