using UnityEngine;
using System.Collections.Generic;

public class PersonScript : MonoBehaviour {

    public int Id { get; set; }
    int foodOwned = 0;
    int yearlyFoodSpent = 1;
    public GameObject CurrentLand { get; set; }
    List<GameObject> ownedLands;
    GameObject superior;
    List<GameObject> subordinates;
    bool mouseOver;
    bool dragging = false;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        int production = CurrentLand.GetComponent<LandScript>().Fertility;
        if (dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - Camera.main.transform.position) + new Vector3(0, 0, +0.1f);
        }
        else if (CurrentLand != null)
        {
            transform.position = CurrentLand.transform.position + new Vector3 (0,0, -0.1f);
        }
    }

    public void MoveTo(GameObject landObject)
    {
        var land = landObject.GetComponent<LandScript>();
        if (land == null)
        {
            return;
        }
        CurrentLand = landObject;
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

    void OnMouseDrag()
    {
        dragging = true;
    }
    void OnMouseUp()
    {
        Debug.Log("mouseup has been called!");
        var society = GameObject.Find("Society").GetComponent<SocietyScript>();
        foreach (var land in society.Lands)
        {
            if (land.GetComponent<LandScript>().mouseOver)
            {
                MoveTo(land);
            }
        }
        dragging = false;
    }

    void OnGUI()
    {
        if (!mouseOver)
            return;
        string text = "Person " + Id;
        text += "\nYearly Food Consumed: " + yearlyFoodSpent;
        text += "\nFood Owned: " + foodOwned;

        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 boxSize = new Vector2(150, 200);
        
        GUI.Box(new Rect(pos.x + 10, Screen.height - pos.y - 10, boxSize.x, boxSize.y), text);
    }
}
