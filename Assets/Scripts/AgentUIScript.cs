using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AgentUIScript : MonoBehaviour {

    GameObject targetObject = null;

    void Start()
    {
    }

    void Update()
    {
        if (targetObject == null)
            return;
        var pos = Camera.main.WorldToScreenPoint(targetObject.transform.position);
        transform.position = pos;
    }
    public void setTargetObject(GameObject targetObject)
    {
        this.targetObject = targetObject;
        FillContent();
    }
    void FillContent()
    {
        if (targetObject == null)
        {
            Debug.Log("targetObject has not been assigned");
            return;
        }
        var agent = targetObject.GetComponent<AgentScript>();
        if (agent == null)
        {
            return;
        }

        foreach (var textComponent in GetComponentsInChildren<Text>())
        {
            switch (textComponent.name)
            {
                case "AgentName":
                    textComponent.text = agent.AgentName;
                    break;
                default:
                    break;
            }
        }

    }
}
