using UnityEngine;
using System.Collections.Generic;

public class LandScript : MonoBehaviour {

    public enum LandType
    {
        FarmLand,
        WoodLand,
        Sea,
        City
    }
    public LandType landType { get; set; }

    public int Id { get; set; }
    public int Fertility { get; set; }
    public bool mouseOver { get; private set; }
    public List<GameObject> AgentsInLand { get; set; }
    static float slotDist = 0.9f;
    List<Vector3> slotPosition = new List<Vector3> {
        new Vector3(-slotDist, -slotDist, -0.1f),
        new Vector3(slotDist, -slotDist, -0.1f),
        new Vector3(-slotDist, slotDist, -0.1f),
        new Vector3(slotDist, slotDist, -0.1f) };
    
    void Start ()
    {
        Fertility = 1;
        AgentsInLand = new List<GameObject>();
        foreach (var slot in slotPosition)
        {
            AgentsInLand.Add(null);
        }
	}
	
	void Update () {
        var renderer = GetComponent<SpriteRenderer>();
        var size = renderer.sprite.bounds.size;
        var collider = GetComponent<BoxCollider2D>();
        collider.size = size;

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

    void OnMouseOver()
    {
        //Debug.Log("Over Land");
    }
    void OnMouseEnter()
    {
        mouseOver = true;
        Debug.Log("Enter Land");
    }
    void OnMouseExit()
    {
        mouseOver = false;
        Debug.Log("Exit Land");
    }

    void OnGUI()
    {
        if (!mouseOver)
            return;
        string text = "Land " + Id;
        text += "\nFertility: " + Fertility;
        text += "\nTerrain: Plains";


        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 boxSize = new Vector2(200, 200);
        GUI.Box(new Rect(pos.x + 20, Screen.height - pos.y - 20, boxSize.x, boxSize.y), text);
    }

}
