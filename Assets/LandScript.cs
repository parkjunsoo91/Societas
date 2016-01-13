using UnityEngine;
using System.Collections;

public class LandScript : MonoBehaviour {

    public int Fertility { get; set; }
    bool mouseOver;

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
        string text = "Land 1";
        text += "\nFertility: " + Fertility;
        text += "\nTerrain: Plains";


        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 boxSize = new Vector2(150, 200);
        GUI.Box(new Rect(pos.x + 10, pos.y - boxSize.y - 30, boxSize.x, boxSize.y), text);
    }

}
