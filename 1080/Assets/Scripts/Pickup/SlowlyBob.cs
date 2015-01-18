using UnityEngine;
using System.Collections;

public class SlowlyBob : MonoBehaviour {

    public float amplitude, speed;

    private float y0;

	// Use this for initialization
	void Start () {
        y0 = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3 (transform.position.x, (y0 + amplitude * Mathf.Sin(speed * Time.time)), transform.position.z);
	}
}
