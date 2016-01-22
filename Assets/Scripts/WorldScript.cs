using UnityEngine;
using System.Collections.Generic;
using System;

public class WorldScript : MonoBehaviour
{
    public GameObject LandPrefab;
    public GameObject PanelPrefab;

    public List<GameObject> LandObjects { get; set; }
    public List<GameObject> AgentObjects { get; set; }

    GameObject canvasObject;
    GameObject panelObject;

    enum GameState
    {
        NatureTurn,
        PlayerTurn,
        AITurn
    }
    GameState gameState;

    void Start()
    {
        canvasObject = GameObject.Find("Canvas");
        //LandObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Land"));
        LandObjects = new List<GameObject>();
        AgentObjects = new List<GameObject>();
        GenerateTerrain();
        InitPanelUI();
        gameState = GameState.NatureTurn;
    }
    void InitPanelUI()
    {
        panelObject = Instantiate(PanelPrefab);
        panelObject.transform.SetParent(canvasObject.transform);
    }
    void GenerateTerrain()
    {
        for (int i = 0; i < 64; i++)
        {
            var landObject = Instantiate(LandPrefab);
            landObject.name = "Land" + i;
            landObject.transform.position = new Vector3(i, 0, 0);
            LandObjects.Add(landObject);

            var land = landObject.GetComponent<LandScript>();
            var x = UnityEngine.Random.value;
            if (x < 0.5)
            {
                land.landType = LandScript.LandType.FarmLand;
            }
            else if (x < 0.8)
            {
                land.landType = LandScript.LandType.WoodLand;
            }
            else if (x < 0.9)
            {
                land.landType = LandScript.LandType.Sea;
            }
            else
            {
                land.landType = LandScript.LandType.City;
            }
        }
    }


    void Update()
    {

    }

    public void SetPanelTarget(GameObject gameobject)
    {
        panelObject.GetComponent<InfoPanelScript>().setTargetObject(gameobject);
    }

    void ExecuteNatureTurn()
    {
        //display UI "nature turn"
        //randomly trigger a natural event.
    }

    void ExecutePlayerTurn()
    {
        //뭐가있을까.
        //세금징수
        //세금 추가징수.
        //인간 클릭: 
    }

    void ExecuteAITurns()
    {
        //TODO: sort the world agents by their status

        foreach (var agentObject in AgentObjects)
        {
            //TODO: transform camera to focus on current agent.
            //TODO: show UI to indicate the current agent.
            var agent = agentObject.GetComponent<AgentScript>();
            Debug.Log("executing turn for ", agentObject);
            Debug.Log(string.Format("executing turn for {0}", agent.name));
            agent.ExecuteTurn();
            //TODO: ask for input for next agent's turn.
        }
    }


    void OnGUI()
    {
        var mousePos = Input.mousePosition;
        string mousePosText = "(" + mousePos.x + ", " + mousePos.y + ", " + mousePos.z + ")";
        var mouseWPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - Camera.main.transform.position);
        string mouseWPosText = "(" + Math.Round(mouseWPos.x, 1) + ", " + Math.Round(mouseWPos.y, 1) + ", " + Math.Round(mouseWPos.z, 1) + ")";

        GUI.Label(new Rect(0, 0, 100, 20), mousePosText);
        GUI.Label(new Rect(0, 20, 100, 20), mouseWPosText);

        if (GUI.Button(new Rect(0, 60, 100, 20), "End Turn"))
        {
            //TODO: block user input

            ExecuteAITurns();
        }
    }

}
