using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RisingMessageScript : MonoBehaviour {

    GameObject targetObject = null;
    float lifeTime = 0f;

    void Start()
    {
    }

    void Update()
    {
        if (targetObject == null)
            return;
        if (lifeTime > 2)
        {
            Destroy(gameObject);
        }
        lifeTime += Time.deltaTime;
        var pos = Camera.main.WorldToScreenPoint(targetObject.transform.position);
        transform.position = pos + new Vector3(0, 20 + lifeTime * 10, 0);
    }

    public void setTargetObject(GameObject targetObject)
    {
        this.targetObject = targetObject; 
    }
    public void setMessage(string message)
    {
        GetComponent<Text>().text = message;
    }
}
