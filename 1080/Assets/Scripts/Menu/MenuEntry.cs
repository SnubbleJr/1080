using UnityEngine;
using System.Collections;

public class MenuEntry : MonoBehaviour {

    public Color highlightedColor = Color.red;
    public bool greyedOut = false;
    public Texture2D selectedTexture;

    private bool selected = false;
    private Color defaultColor;

    void Start()
    {
        defaultColor = guiText.color;
    }

    //get the reset select evenr from Menu Script
    void OnEnable()
    {
        MenuScript.optionSelected += selectionCheck;
        MenuScript.resetSelect += resetSelected;
        selected = false;
    }

    void OnDisable()
    {
        MenuScript.optionSelected -= selectionCheck;
        MenuScript.resetSelect -= resetSelected;
        selected = false;
    }

	// Update is called once per frame
	void Update () {

        //dirst, see if applicable, else it's greyed out
        if (!greyedOut)
        {
            //check if selected
            if (selected)
            {
                //selected code
                guiText.color = highlightedColor;
            }
            else
            {
                //unselected code
                guiText.color = defaultColor;
            }
        }
        else
        {
            guiText.color = Color.grey;
        }
    }
    
    void OnGUI()
    {
        if (selected)
        {
            Vector3 coOrds = Camera.main.ViewportToScreenPoint(transform.position);

            //because we rotated it, we have to use y,x not x,y, make sure anchor is top left
            GUIUtility.RotateAroundPivot(90, new Vector2(30, 30));
            GUI.contentColor = highlightedColor;
            GUI.Label(new Rect((Screen.height - coOrds.y), (coOrds.x - Screen.width)*0.8f, 50, 50), selectedTexture);
        }
    }

    void selectionCheck()
    {
        //if we are selected
        if (selected)
        {
            //send a message to a script in the object to get moving
            this.SendMessage("goTime");
        }
    }

    //resetselected is just a speical case of setselected
    void resetSelected()
    {
        setSelected(false);
    }

    //sets the selectiong marker for the entry
    public void setSelected(bool val)
    {
        selected = val;
    }
}
