using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class WorldScript : MonoBehaviour
{
    //world will hold all agents and lands as list
    public List<GameObject> LandObjects { get; set; }
    public List<GameObject> AgentObjects { get; set; }

    //world also serves as game manager, and keeps track of selected object
    public GameObject selectedObject;

    public GameObject LandPrefab;
    public GameObject AgentPrefab;
    public GameObject PanelPrefab;
    public GameObject TurnNoticePrefab;
    
    GameObject canvasObject;
    GameObject panelObject;

    enum GameState
    {
        NatureTurn,
        PlayerTurn,
        AITurn
    }

    int population;
    int food;
    GameObject resourceTextObject;

    GameState gameState;
    int agentCounter = 0;
    int landCounter = 0;

    void Start()
    {
        canvasObject = GameObject.Find("Canvas");
        resourceTextObject = GameObject.Find("ResourceText");
        LandObjects = new List<GameObject>();
        AgentObjects = new List<GameObject>();
        GenerateTerrain();
        GenerateAgents();
        InitPanelUI();
        gameState = GameState.NatureTurn;
        ExecuteNatureTurn();
    }
    void InitPanelUI()
    {
        panelObject = Instantiate(PanelPrefab);
        panelObject.transform.SetParent(canvasObject.transform);
    }
    void GenerateTerrain()
    {
        CreateLand(LandScript.LandType.City);
        CreateLand(LandScript.LandType.City);
        CreateLand(LandScript.LandType.City);
        CreateLand(LandScript.LandType.FarmLand);
        CreateLand(LandScript.LandType.FarmLand);
        CreateLand(LandScript.LandType.FarmLand);
        CreateLand(LandScript.LandType.FarmLand);
    }
    public GameObject CreateLand(LandScript.LandType type)
    {
        var landObject = Instantiate(LandPrefab);
        landObject.name = "Land " + landCounter;
        landObject.transform.position = new Vector3(landCounter * 1.6f, 0f, 0);
        LandObjects.Add(landObject);

        var land = landObject.GetComponent<LandScript>();
        land.SetType(type);
        landCounter++;
        return landObject;
    }

    void GenerateAgents()
    {
        CreateAgent(AgentScript.Profession.Knight, 2, LandObjects[0], true);
        CreateAgent(AgentScript.Profession.Artisan, 2, LandObjects[1], true);
        CreateAgent(AgentScript.Profession.Merchant, 2, LandObjects[2], true);
        CreateAgent(AgentScript.Profession.Farmer, 2, LandObjects[3], true);
        CreateAgent(AgentScript.Profession.Farmer, 2, LandObjects[3], true);
        CreateAgent(AgentScript.Profession.Farmer, 2, LandObjects[4], true);
        CreateAgent(AgentScript.Profession.Farmer, 2, LandObjects[5], true);
        CreateAgent(AgentScript.Profession.Farmer, 2, LandObjects[6], true);
    }
    public GameObject CreateAgent(AgentScript.Profession profession, int wealth, GameObject land, bool owns)
    {
        var agentObject = Instantiate(AgentPrefab);
        agentObject.name = "Agent " + agentCounter++;
        var agent = agentObject.GetComponent<AgentScript>();
        agent.Id = agentCounter - 1;
        agent.CurrentLand = land;
        if (owns)
        {
            agent.TakeOwnership(land);
        }
        agent.Wealth = wealth;
        agent.AgentProfession = profession;
        AgentObjects.Add(agentObject);
        RefreshResources(); 
        return agentObject;
    }

    public void AddFood(int x)
    {
        if (x < 0 && food < x * -1)
        {
            food = 0;
            RefreshResources();
        }
        else
        {
            food = food + x;
            RefreshResources();
        }
    }

    public void RefreshResources()
    {
        population = AgentObjects.Count;
        resourceTextObject.GetComponent<Text>().text = "Population: " + population + "\nFood Production: " + food;
    }

    void Update()
    {
        switch (gameState)
        {
            case GameState.NatureTurn:
                break;
            case GameState.PlayerTurn:

                break;
            case GameState.AITurn:
                break;
            default:
                break;
        }

    }

    public void SetPanelTarget(GameObject gameobject)
    {
        panelObject.GetComponent<InfoPanelScript>().setTargetObject(gameobject);
    }

    void ExecuteNatureTurn()
    {
        StartCoroutine(NatureBehavior());
    }

    IEnumerator NatureBehavior()
    {
        yield return new WaitForSeconds(1);
        var turnNotice = Instantiate(TurnNoticePrefab);
        turnNotice.transform.SetParent(canvasObject.transform);
        turnNotice.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -100);

        foreach (var agentObject in AgentObjects)
        {

            var agent = agentObject.GetComponent<AgentScript>();
            agent.Wealth -= 1;
            agent.SayMessage("consumption -1");
            AddFood(-1);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(1);

        if (food > population * 2)
        {
            foreach (var agentObject in AgentObjects)
            {
                var agent = agentObject.GetComponent<AgentScript>();
                if (agent.AgentProfession == AgentScript.Profession.Farmer)
                {
                    if (agent.Wealth >= 3)
                    {
                        int inheritedWealth = agent.Wealth / 3;
                        CreateAgent(AgentScript.Profession.Unemployed, inheritedWealth, agent.CurrentLand, false);
                        break;
                    }
                }
            }

        }

        Destroy(turnNotice);
        gameState = GameState.PlayerTurn;
    }


    void ExecuteAITurns()
    {
        StartCoroutine(AIBehavior());
    }

    IEnumerator AIBehavior()
    {
        //TODO: sort the world agents by their status
        var turnNotice = Instantiate(TurnNoticePrefab);
        turnNotice.transform.SetParent(canvasObject.transform);
        turnNotice.GetComponent<Text>().text = "AI Turn!";
        turnNotice.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -100);

        foreach (var agentObject in AgentObjects)
        {
            turnNotice.GetComponent<Text>().text = agentObject.GetComponent<AgentScript>().AgentName + "'s Turn!";
            Camera.main.GetComponent<CameraScript>().focusObject(agentObject);

            var agent = agentObject.GetComponent<AgentScript>();            
            yield return StartCoroutine(agent.ExecuteTurn());
        }
        Destroy(turnNotice);

        gameState = GameState.NatureTurn;
        ExecuteNatureTurn();
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
            if (gameState == GameState.PlayerTurn)
            {
                gameState = GameState.AITurn;
                ExecuteAITurns();

            }
        }
        if (GUI.Button(new Rect(0, 80, 100, 20), "Discover new land"))
        {
            CreateLand(LandScript.LandType.FarmLand);
        }
    }

}
