using UnityEngine;
using System.Collections;

public class MenuSkin : MonoBehaviour {

    public GUISkin skin;
    public static GUISkin menuSkin;

    void Start()
    {
        menuSkin = skin;
    }
}
