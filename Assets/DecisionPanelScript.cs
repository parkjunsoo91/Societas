using UnityEngine;
using System.Collections;

public class DecisionPanelScript : MonoBehaviour {


    GameObject titleObject;
    GameObject descriptionObject;
    GameObject promptObject;
    GameObject buttonObject1;
    GameObject buttonObject2;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public enum DecisionPanelType
    {
        EqualTax,
        Conscription
    }

    public void SetContent(DecisionPanelType type)
    {

    }
}
