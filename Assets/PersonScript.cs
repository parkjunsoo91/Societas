using UnityEngine;
using System.Collections.Generic;

public class PersonScript : MonoBehaviour {

    int foodOwned = 0;
    int yearlyFoodSpent = 1;
    public GameObject currentLand { get; set; }
    List<GameObject> ownedLands;
    GameObject superior;
    List<GameObject> subordinates;
    bool mouseOver;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        int production = currentLand.GetComponent<LandScript>().Fertility;
        if (currentLand != null)
        {
            transform.position = currentLand.transform.position + new Vector3(0,0,-1);
        }
    }

    public void MoveTo(GameObject landObject)
    {
        var land = landObject.GetComponent<LandScript>();
        if (land == null)
        {
            return;
        }
        currentLand = landObject;
    }

    void OnMouseOver()
    {
        //Debug.Log("Over Person");
    }
    void OnMouseEnter()
    {
        Debug.Log("Enter Person");
        mouseOver = true;
    }
    void OnMouseExit()
    {
        Debug.Log("Exit Person");
        mouseOver = false;
    }

    void OnGUI()
    {
        if (!mouseOver)
            return;
        string text = "Person 1";
        text += "\nYearly Food Consumed: " + yearlyFoodSpent;
        text += "\nFood Owned: " + foodOwned;

        
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 boxSize = new Vector2(150, 200);
        GUI.Box(new Rect(pos.x, pos.y - boxSize.y - 20, boxSize.x, boxSize.y), text);
    }
}
