using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey("up") || Input.GetKey("w"))
        {
            transform.Translate(0, 0.1f, 0);
        }
        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            transform.Translate(0, -0.1f, 0);
        }
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            transform.Translate(-0.1f, 0, 0);
        }
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            transform.Translate(0.1f, 0, 0);
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            transform.Translate(0, 0, -1f);
        }
        if (Input.mouseScrollDelta.y < 0 && transform.position.z < -5)
        {
            transform.Translate(0, 0, 1f);
        }
    }
}
