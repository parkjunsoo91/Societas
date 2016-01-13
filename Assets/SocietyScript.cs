using UnityEngine;
using System.Collections.Generic;
using System;

public class SocietyScript : MonoBehaviour {

    public List<GameObject> Lands { get; set; }
    public List<GameObject> People { get; set; }

    public GameObject LandPrefab;
    public GameObject PersonPrefab;

    int nextPersonId = 0;
    int nextLandId = 0;
    int nextLandX;
    int nextLandY;

    // Use this for initialization
    void Start () {
        Lands = new List<GameObject>();
        People = new List<GameObject>();
        NewLand();
        NewPerson();
        NewLand();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject NewLand()
    {
        var landObject = (GameObject)Instantiate(LandPrefab, new Vector3(nextLandX, nextLandY, 0), transform.rotation);
        
        var land = landObject.GetComponent<LandScript>();
        land.Id = nextLandId++;

        nextLandX += 1;
        Lands.Add(landObject);

        return landObject;
    }
    public GameObject NewPerson()
    {
        var personObject = (GameObject)Instantiate(PersonPrefab, transform.position, transform.rotation);
        var person = personObject.GetComponent<PersonScript>();
        person.Id = nextPersonId++;
        People.Add(personObject);
        person.MoveTo(Lands[0]);
        return personObject;
    }

    void OnGUI()
    {
        var mousePos = Input.mousePosition;
        string mousePosText = "(" +mousePos.x+ ", " + mousePos.y+ ", " + mousePos.z + ")";
        var mouseWPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - Camera.main.transform.position);
        string mouseWPosText = "(" + Math.Round(mouseWPos.x,1) + ", " + Math.Round(mouseWPos.y, 1) + ", " + Math.Round(mouseWPos.z, 1) + ")";
        GUI.Label(new Rect(0, 0, 80, 20), mousePosText);
        GUI.Label(new Rect(0, 20, 80, 20), mouseWPosText);
        if (GUI.Button(new Rect(20, 40, 80, 20), "Add Land"))
        {
            NewLand();
        }
        if (GUI.Button(new Rect(20, 60, 80, 20), "Add Person"))
        {
            NewPerson();
        }
    }

}
