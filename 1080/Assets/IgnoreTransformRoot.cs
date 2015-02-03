using UnityEngine;
using System.Collections;

public class IgnoreTransformRoot : MonoBehaviour {

    void Start()
    {
        CharacterController cc = transform.root.GetComponent<CharacterController>();

        Physics.IgnoreCollision(rigidbody.collider, cc.collider);
    }
	
}
