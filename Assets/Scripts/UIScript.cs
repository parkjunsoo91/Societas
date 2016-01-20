using UnityEngine;
using System.Collections;
using System;

public class UIScript : MonoBehaviour {

    private WorldScript world;

	void Start ()
    {
        world = GameObject.Find("World").GetComponent<WorldScript>();
	}
	
	void Update ()
    {

	}

    void OnGUI()
    {
        var mousePos = Input.mousePosition;
        string mousePosText = "(" + mousePos.x + ", " + mousePos.y + ", " + mousePos.z + ")";
        var mouseWPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - Camera.main.transform.position);
        string mouseWPosText = "(" + Math.Round(mouseWPos.x, 1) + ", " + Math.Round(mouseWPos.y, 1) + ", " + Math.Round(mouseWPos.z, 1) + ")";

        GUI.Label(new Rect(0, 0, 100, 20), mousePosText);
        GUI.Label(new Rect(0, 20, 100, 20), mouseWPosText);

        if (GUI.Button(new Rect(0, 60, 100, 20), "End Turn"))
        {
            //TODO: block user input

            world.ExecuteAITurns();
        }
    }
}
