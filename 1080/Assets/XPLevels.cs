using UnityEngine;
using System.Collections;

[System.Serializable]
public class XPLevel
{
    public int xPNeeded;
    public ScriptableObject reward;
}

public class XPLevels : MonoBehaviour {

    public XPLevel[] xPLevels;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
