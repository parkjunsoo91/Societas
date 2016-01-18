using UnityEngine;
using System.Collections.Generic;

public class AgentScript : MonoBehaviour {

    public int Id { get; set; }
    
    Profession profession;
    public int Wealth { get; set; }

    public GameObject CurrentLand { get; set; }

    public enum Profession
    {
        Unemployed,
        Farmer,
        Artisan,
        Merchant,
        Knight
    }

    bool mouseOver;
    bool dragging = false;


    void Start()
    {
        profession = Profession.Unemployed;
    }

    void Update()
    {
        int production = CurrentLand.GetComponent<LandScript>().Fertility;
        if (dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - Camera.main.transform.position) + new Vector3(0, 0, +0.1f);
        }
        else if (CurrentLand != null)
        {
            transform.position = CurrentLand.transform.position + new Vector3(0, 0, -0.1f);
        }
    }

    public void ExecuteTurn()
    {
        //AI behavior:
        //move phase: position changes
        
        //purchase/sales phase: land&slaves ownership management
        if (Wealth > 10)
        {
            //look for a land for sale. if available, buy it.
        }

        if (Wealth > 9)
        {
            //look for a slave for sale. if available, buy it.
        }

        //status change phase: status changes
        if (getProperty() > 20)
        {
            //look for a status change
        }

        //production phase : wealth changes
        switch (profession)
        {
            case Profession.Unemployed:
                break;
            case Profession.Farmer:
                if (CurrentLand.GetComponent<LandScript>().landType == LandScript.LandType.FarmLand)
                {
                    Wealth += 1;
                }
                break;
            case Profession.Artisan:

                break;
            case Profession.Merchant:
                break;
            case Profession.Knight:
                break;
            default:
                break;
        }
    }

    int getProperty()
    {
        return Wealth;
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

        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 boxSize = new Vector2(200, 200);
        
        GUI.Box(new Rect(pos.x + 10, Screen.height - pos.y - 10, boxSize.x, boxSize.y), text);
    }


}
