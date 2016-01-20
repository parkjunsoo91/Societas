using UnityEngine;
using System.Collections.Generic;

public class AgentScript : MonoBehaviour {

    public GameObject RisingTextPrefab;
    public GameObject PanelPrefab;
    public GameObject AgentUIPrefab;

    public int Id { get; set; }
    public string AgentName { get; set; }
    public Profession AgentProfession { get; set; }
    public int Wealth { get; set; }
    public int Happiness { get; set; }

    public GameObject CurrentLand { get; set; }

    public enum Profession
    {
        Unemployed,
        Farmer,
        Artisan,
        Merchant,
        Knight
    }

    GameObject canvasObject;
    GameObject agentUIObject;
    bool mouseOver;
    bool dragging = false;

    void Start()
    {
        canvasObject = GameObject.Find("Canvas");
        AgentProfession = Profession.Unemployed;
        AgentName = "Another " + AgentProfession.ToString();
        InitAgentUI();
    }
    void InitAgentUI()
    {
        agentUIObject = Instantiate(AgentUIPrefab);
        agentUIObject.transform.SetParent(canvasObject.transform);
        agentUIObject.name = gameObject.name + "UI";
        var agentUI = agentUIObject.GetComponent<AgentUIScript>();
        agentUI.setTargetObject(gameObject);
    }


    void Update()
    {
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
        if (GetProperty() > 20)
        {
            //look for a status change
        }

        //production phase : wealth changes
        switch (AgentProfession)
        {
            case Profession.Unemployed:
                break;
            case Profession.Farmer:
                if (CurrentLand.GetComponent<LandScript>().landType == LandScript.LandType.FarmLand)
                {
                    SayMessage("Wealth +1");
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
    int GetProperty()
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

    void SayMessage(string message)
    {
        var textObject = Instantiate(RisingTextPrefab);
        textObject.transform.SetParent(canvasObject.transform);
        var risingMessage = textObject.GetComponent<RisingMessageScript>();
        risingMessage.setTargetObject(gameObject);
        risingMessage.setMessage(message);
    }

    void ShowPanel()
    {
        var panelObject = Instantiate(PanelPrefab);
        panelObject.transform.SetParent(canvasObject.transform);
        var panel = panelObject.GetComponent<InfoPanelScript>();
        panel.setTargetObject(gameObject);
    }

#region mouse interaction

    void OnMouseDown()
    {
        dragging = true;
    }

    void OnMouseUpAsButton()
    {
        ShowPanel();
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

    void OnMouseUp()
    {
        dragging = false;
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

    #endregion

}
