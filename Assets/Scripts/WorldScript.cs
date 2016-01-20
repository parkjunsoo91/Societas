using UnityEngine;
using System.Collections.Generic;
using System;

public class WorldScript : MonoBehaviour
{
    public List<GameObject> LandObjects { get; set; }
    public List<GameObject> AgentObjects { get; set; }

    void Start()
    {
        LandObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Land"));
        AgentObjects = new List<GameObject>();
    }

    void Update()
    {

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
        }
    }

}
