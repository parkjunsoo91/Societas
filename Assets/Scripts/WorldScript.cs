using UnityEngine;
using System.Collections.Generic;

public class WorldScript : MonoBehaviour
{
    public GameObject LandPrefab;
    public List<GameObject> LandObjects { get; set; }
    public List<GameObject> AgentObjects { get; set; }

    void Start()
    {
        //LandObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Land"));
        LandObjects = new List<GameObject>();
        AgentObjects = new List<GameObject>();
        GenerateTerrain();
    }

    void Update()
    {

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
            var x = Random.value;
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

    public void ExecuteAITurns()
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

}
