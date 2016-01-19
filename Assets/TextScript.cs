using UnityEngine;
using System.Collections;

public class TextScript : MonoBehaviour {

    GameObject agentObject;
    
	// Use this for initialization
	void Start () {
        agentObject = GameObject.Find("Agent");

    }

    // Update is called once per frame
    void Update () {
        var pos = Camera.main.WorldToScreenPoint(agentObject.transform.position);
        transform.position = pos;
        
	}
}
