using UnityEngine;
using System.Collections;

public class InfoPanelScript : MonoBehaviour {

    GameObject targetObject = null;
    float lifeTime = 0f;

    void Start()
    {
    }

    void Update()
    {
        lifeTime += Time.deltaTime;
        if (targetObject == null)
            return;
        if (lifeTime > 3)
        {
            Destroy(gameObject);
        }
        var pos = Camera.main.WorldToScreenPoint(targetObject.transform.position);
        transform.position = pos + new Vector3(10, 30, 0);
    }

    public void setTargetObject(GameObject targetObject)
    {
        this.targetObject = targetObject;
    }
    public void setMessage(string message)
    {
    }
}
