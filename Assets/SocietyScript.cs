using UnityEngine;
using System.Collections.Generic;

public class SocietyScript : MonoBehaviour {

    public List<GameObject> Lands { get; set; }
    public List<GameObject> People { get; set; }

    public GameObject LandPrefab;
    public GameObject PersonPrefab;


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
        var land = (GameObject)Instantiate(LandPrefab, new Vector3(nextLandX, nextLandY, 0), transform.rotation);
        nextLandX += 1;
        Lands.Add(land);
        return land;
    }
    public GameObject NewPerson()
    {
        var person = (GameObject)Instantiate(PersonPrefab, transform.position, transform.rotation);
        People.Add(person);

        var p = person.GetComponent<PersonScript>();
        p.MoveTo(Lands[0]);
        return person;
    }

}
