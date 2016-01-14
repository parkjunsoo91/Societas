using UnityEngine;
using System.Collections.Generic;

public class LandScript : MonoBehaviour {

    public int Id { get; set; }
    public int Fertility { get; set; }
    public bool mouseOver { get; private set; }
    public List<GameObject> Persons { get; set; }
    List<Vector3> positions = new List<Vector3> { new Vector3(0, 0, -0.1f), new Vector3(-0.5f, -0.5f, -0.1f), new Vector3(0f, -0.5f, -0.1f), new Vector3(0.5f, -0.5f, -0.1f) };

    // Use this for initialization
    void Start () {
        Fertility = 1;
	}
	
	// Update is called once per frame
	void Update () {
        var renderer = GetComponent<SpriteRenderer>();
        var size = renderer.sprite.bounds.size;
        var collider = GetComponent<BoxCollider2D>();
        collider.size = size;
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
