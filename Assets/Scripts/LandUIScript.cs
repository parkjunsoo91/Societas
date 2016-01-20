using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LandUIScript : MonoBehaviour {

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
        if(targetObject == null)
        {
            Debug.Log("targetObject has not been assigned");
            return;
        }
        var land = targetObject.GetComponent<LandScript>();
        if (land == null)
        {
            return;
        }
        
        foreach (var textComponent in GetComponentsInChildren<Text>())
        {
            switch (textComponent.name)
            {
                case "LandName":
                    textComponent.text = land.LandName;
                    break;
                default:
                    break;
            }
        }
        
    }
}
