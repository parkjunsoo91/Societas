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
    public LandType landType { get; set; }
    public int Fertility { get; set; }

    public List<GameObject> AgentsInLand { get; set; }

    GameObject worldObject;    
    GameObject canvasObject;
    GameObject landUIObject;

    public bool mouseOver { get; private set; }
    static float slotDist = 0.9f;

    List<Vector3> slotPosition = new List<Vector3> {
        new Vector3(-slotDist, -slotDist, -0.1f),
        new Vector3(slotDist, -slotDist, -0.1f),
        new Vector3(-slotDist, slotDist, -0.1f),
        new Vector3(slotDist, slotDist, -0.1f) };
    
    void Start ()
    {
        worldObject = GameObject.Find("World");
        canvasObject = GameObject.Find("Canvas");

        LandName = landType.ToString(); //temporary name.
        InitLandUI();

        Fertility = 1;
        
        AgentsInLand = new List<GameObject>();
        foreach (var slot in slotPosition)
        {
            AgentsInLand.Add(null);
        }
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

    void Update () {
        var renderer = GetComponent<SpriteRenderer>();
        var size = renderer.sprite.bounds.size;

        for (var i = 0; i < AgentsInLand.Count; i++)
        {
            if (AgentsInLand[i] != null)
            {
                var person = AgentsInLand[i].GetComponent<AgentScript>();
                person.transform.position = transform.position + slotPosition[i];
            }
        }

    }

    public void PlacePerson(GameObject personObject)
    {
        for (var i=0; i< AgentsInLand.Count; i++)
        {
            if (AgentsInLand[i] == null)
            {
                AgentsInLand[i] = personObject;
            }
        }
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
        Debug.Log("Enter Land");
    }
    void OnMouseExit()
    {
        mouseOver = false;
        Debug.Log("Exit Land");
    }

#endregion

}
