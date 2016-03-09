using UnityEngine;
using System.Collections.Generic;

public class LandScript : MonoBehaviour {

    public GameObject LandUIPrefab;
    public GameObject PanelPrefab;

    public enum LandType
    {
        FarmLand,
        WoodLand,
        Sea,
        City
    }

    public int Id { get; set; }
    public string LandName { get; set; }
    public LandType landType { get; private set; }
    public int Fertility { get; set; }
    int cropsPlanted = 0;
    public int CropsPlanted
    {
        get { return cropsPlanted; }
        set
        {
            //Todo: modify crop spites on the land
            cropsPlanted = value;
        }
    }
    public List<GameObject> agentsInLand;
    public List<GameObject> AgentsInLand
    {
        get
        {
            if (agentsInLand == null)
                return new List<GameObject>();
            return agentsInLand;
        }
        set
        {
            agentsInLand = value;
        }
    }
    
    public GameObject LandOwner = null;

    GameObject worldObject;    
    GameObject canvasObject;
    GameObject landUIObject;

    public bool mouseOver { get; private set; }
    
    void Start ()
    {
        worldObject = GameObject.Find("World");
        canvasObject = GameObject.Find("Canvas");

        LandName = landType.ToString(); //temporary name.
        InitLandUI();

        Fertility = 1;
        /*
        foreach (var slot in slotPosition)
        {
            AgentsInLand.Add(null);
        }
        */
	}

    void InitLandUI()
    {
        landUIObject = Instantiate(LandUIPrefab);
        landUIObject.transform.SetParent(canvasObject.transform);
        landUIObject.name = gameObject.name + "UI";
        var landUI = landUIObject.GetComponent<LandUIScript>();
        landUI.setTargetObject(gameObject);
    }

    void ShowPanel()
    {
        worldObject.GetComponent<WorldScript>().SetPanelTarget(gameObject);
    }

    void HidePanel()
    {
        worldObject.GetComponent<WorldScript>().SetPanelTarget(null);
    }

    void Update () {
        var renderer = GetComponent<SpriteRenderer>();
        var size = renderer.sprite.bounds.size;
        /*
        for (var i = 0; i < AgentsInLand.Count; i++)
        {
            if (AgentsInLand[i] != null)
            {
                var person = AgentsInLand[i].GetComponent<AgentScript>();
                person.transform.position = transform.position + slotPosition[i];
            }
        }
        */
        int index = 0;
        foreach (var agentObject in AgentsInLand)
        {
            switch (index)
            {
                case 0:
                    agentObject.transform.position = transform.position + new Vector3(0, 0.5f, 0f);
                    break;
                case 1:
                    agentObject.transform.position = transform.position + new Vector3(0.4f, 0.1f, 0f);
                    break;
                case 2:
                    agentObject.transform.position = transform.position + new Vector3(-0.4f, 0.1f, 0f);
                    break;
                case 3:
                    agentObject.transform.position = transform.position + new Vector3(0.4f, 0.9f, 0f);
                    break;
                default:
                    agentObject.transform.position = transform.position + new Vector3(-0.4f, 0.9f, 0f);
                    break;
            }
            index++;
        }

    }


    public void SetType(LandType type)
    {
        switch (type)
        {
            case LandType.FarmLand:
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                break;
            case LandType.WoodLand:
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                break;
            case LandType.Sea:
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                break;
            case LandType.City:
                GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.4f, 0.5f, 1);
                break;
            default:
                break;
        }
        landType = type;
    }

    #region mouse interface

    void OnMouseDown()
    {
    }
    void OnMouseUpAsButton()
    {
        
    }

    void OnMouseOver()
    {
        //Debug.Log("Over Land");
    }
    void OnMouseEnter()
    {
        ShowPanel();
        mouseOver = true;
        //Debug.Log("Enter Land");
    }
    void OnMouseExit()
    {
        HidePanel();
        mouseOver = false;
        //Debug.Log("Exit Land");
    }

#endregion

}
