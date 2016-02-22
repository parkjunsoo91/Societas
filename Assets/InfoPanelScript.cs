using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoPanelScript : MonoBehaviour {

    GameObject targetObject = null;
    Vector3 offsetVector = new Vector3(0, 0, 0);

    void Start()
    {
    }

    void Update()
    {
        if (targetObject == null)
        {
            transform.position = new Vector3(-100, -100, 0);
            return;
        }
        var pos = Camera.main.WorldToScreenPoint(targetObject.transform.position);
        transform.position = pos + offsetVector;
    }

    public void setTargetObject(GameObject targetObject)
    {
        this.targetObject = targetObject;
        FillContent();
    }
    void FillContent()
    {
        if (targetObject == null)
            return;
        var agent = targetObject.GetComponent<AgentScript>();
        if (agent != null)
        {
            offsetVector = new Vector3(0, 100, 0); 
            foreach (var textScript in GetComponentsInChildren<Text>())
            {
                switch (textScript.name)
                {
                    case "NameText":
                        textScript.text = agent.AgentName;
                        break;
                    case "TypeText":
                        textScript.text = agent.AgentProfession.ToString();
                        break;
                    case "Field1":
                        textScript.text = "Wealth : ";
                        break;
                    case "Value1":
                        textScript.text = agent.Wealth.ToString();
                        break;
                    case "Field2":
                        textScript.text = "Happiness : ";
                        break;
                    case "Value2":
                        textScript.text = agent.Happiness.ToString();
                        break;
                    case "Field3":
                        textScript.text = "";
                        break;
                    case "Value3":
                        textScript.text = "";
                        break;
                    default:
                        break;
                }
            }
            return;
        }
        var land = targetObject.GetComponent<LandScript>();
        if (land != null)
        {
            offsetVector = new Vector3(0, -200, 0);
            foreach (var textScript in GetComponentsInChildren<Text>())
            {
                switch (textScript.name)
                {
                    case "NameText":
                        textScript.text = land.LandName;
                        break;
                    case "TypeText":
                        textScript.text = land.landType.ToString();
                        break;
                    case "Field1":
                        textScript.text = "Used as : ";
                        break;
                    case "Value1":
                        textScript.text = land.landType.ToString();
                        break;
                    case "Field2":
                        textScript.text = "Fertility : ";
                        break;
                    case "Value2":
                        textScript.text = land.Fertility.ToString();
                        break;
                    case "Field3":
                        textScript.text = "";
                        break;
                    case "Value3":
                        textScript.text = "";
                        break;
                    default:
                        break;
                }
            }
            return;
        }

    }
}
