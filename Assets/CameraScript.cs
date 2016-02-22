using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        focusedObject = null;
	}
	
	// Update is called once per frame
	void Update () {
        if (focusedObject != null)
        {
            transform.position = new Vector3(focusedObject.transform.position.x, transform.position.y, transform.position.z);
        }
	    if (Input.GetKey("up") || Input.GetKey("w"))
        {
            focusedObject = null;
            transform.Translate(0, 0.1f, 0);
        }
        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            focusedObject = null;
            transform.Translate(0, -0.1f, 0);
        }
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            focusedObject = null;
            transform.Translate(-0.1f, 0, 0);
        }
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            focusedObject = null;
            transform.Translate(0.1f, 0, 0);
        }
        if (Input.mouseScrollDelta.y > 0 && transform.position.z < -5)
        {
            transform.Translate(0, 0, 1f);
        }
        if (Input.mouseScrollDelta.y < 0 )
        {
            transform.Translate(0, 0, -1f);
        }
    }

    public void moveToObject(GameObject gameObject)
    {
        float distance = gameObject.transform.position.x - transform.position.x;
        transform.Translate(distance, 0, 0);
    }

    public void focusObject(GameObject gameObject)
    {
        focusedObject = gameObject;
    }

    GameObject focusedObject;
}
