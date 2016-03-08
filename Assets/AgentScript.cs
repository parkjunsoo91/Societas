using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentScript : MonoBehaviour {

    public GameObject RisingTextPrefab;
    public GameObject AgentUIPrefab;

    public int Id;
    public string AgentName;
    public Profession AgentProfession;
    public int Wealth;
    public int Happiness;

    private GameObject currentLand;
    public GameObject CurrentLand
    {
        get
        {
            return currentLand;
        }
        set
        {
            if (currentLand != null)
            {
                currentLand.GetComponent<LandScript>().AgentsInLand.Remove(gameObject);
            }
            value.GetComponent<LandScript>().AgentsInLand.Add(gameObject);
            currentLand = value;
        }
    }
    public List<GameObject> OwnedLands;
    public void TakeOwnership(GameObject landObject)
    {
        var land = landObject.GetComponent<LandScript>();
        var oldOwnerObject = land.LandOwner;
        if (oldOwnerObject != null)
        {
            oldOwnerObject.GetComponent<AgentScript>().OwnedLands.Remove(landObject);
        }
        OwnedLands.Add(landObject);
        land.LandOwner = gameObject;
        land.LandName = AgentName + "'s " + land.landType.ToString();
    }

    public enum Profession
    {
        Unemployed,
        Farmer,
        Artisan,
        Merchant,
        Knight
    }

    GameObject worldObject;
    WorldScript world;
    GameObject canvasObject;
    GameObject agentUIObject;
    GameObject highlightObject;
    Animator animator;

    bool mouseOver;
    public bool selected = false;

    void Start()
    {
        worldObject = GameObject.Find("World");
        world = worldObject.GetComponent<WorldScript>();
        canvasObject = GameObject.Find("Canvas");

        highlightObject = transform.Find("Highlight").gameObject;
        highlightObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

        OwnedLands = new List<GameObject>();

        //AgentProfession = Profession.Unemployed;
        AgentName = "Another " + AgentProfession.ToString();
        InitAgentUI();

        animator = GetComponent<Animator>();

        SetAgentSprite();

    }
    void InitAgentUI()
    {
        agentUIObject = Instantiate(AgentUIPrefab);
        agentUIObject.transform.SetParent(canvasObject.transform);
        agentUIObject.name = gameObject.name + "UI";
        var agentUI = agentUIObject.GetComponent<AgentUIScript>();
        agentUI.setTargetObject(gameObject);
    }
    void SetAgentSprite()
    {
        var renderer = GetComponent<SpriteRenderer>();

        switch (AgentProfession)
        {
            case Profession.Unemployed:
                renderer.sprite = Resources.Load("farmer", typeof(Sprite)) as Sprite;
                animator.SetTrigger("Farmer");
                break;
            case Profession.Farmer:
                renderer.sprite = Resources.Load("farmer", typeof(Sprite)) as Sprite;
                animator.SetTrigger("Farmer");
                break;
            case Profession.Artisan:
                renderer.sprite = Resources.Load("artisan", typeof(Sprite)) as Sprite;
                animator.SetTrigger("Artisan");
                break;
            case Profession.Merchant:
                animator.SetTrigger("Merchant");
                renderer.sprite = Resources.Load("merchant", typeof(Sprite)) as Sprite;
                break;
            case Profession.Knight:
                animator.SetTrigger("Farmer");
                renderer.sprite = Resources.Load("soldier", typeof(Sprite)) as Sprite;
                break;
            default:
                break;
        }
    }


    void Update()
    {
        //if (CurrentLand != null)
        //{
        //    transform.position = CurrentLand.transform.position + new Vector3(0, 0.5f, 0f);
        //}
        if (world.selectedObject == gameObject)
        {
            highlightObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            highlightObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        }

    }

    public IEnumerator ExecuteTurn()
    {
        //AI behavior:
        var currentLand = CurrentLand.GetComponent<LandScript>();
        switch (AgentProfession)
        {
            case Profession.Unemployed:
                
                if (currentLand.LandOwner == gameObject)
                {
                    //do something with his land
                }
                else
                {
                    //search for empty land
                    foreach (var landObject in world.LandObjects)
                    {
                        var land = landObject.GetComponent<LandScript>();
                        if (land.LandOwner == null)
                        {
                            MoveTo(landObject);

                        }
                    }
                }
                
                break;
                
            case Profession.Farmer:
                if (currentLand.landType == LandScript.LandType.FarmLand)
                {
                    //harvest action
                    int crops = currentLand.CropsPlanted;
                    if (crops > 0)
                    {
                        int rng = Random.Range(-2, 1);
                        int yield = crops * 3 + rng;
                        Wealth += yield;
                        SayMessage("Harvest +" + yield);
                        world.AddFood(yield);
                        currentLand.CropsPlanted = 0;
                        yield return new WaitForSeconds(1);
                    }
                    //start farming action
                    if (Wealth >= 1)
                    {
                        crops = Wealth >= 3 ? 2 : (Wealth >= 1 ? 1 : 0);
                        Wealth -= crops;
                        currentLand.CropsPlanted = crops;
                        SayMessage("Farming Investment -" + crops);
                        yield return new WaitForSeconds(1);
                    }
                }
                break;
            case Profession.Artisan:
                if (true) //TODO: check previous investment
                {
                    int yield = Random.Range(1, 3);
                    Wealth += yield;
                    SayMessage("production +" + yield);
                    yield return new WaitForSeconds(1);
                }
                if (Wealth >= 1)
                {
                    Wealth -= 1;
                    SayMessage("Crafting Investment -1");
                    yield return new WaitForSeconds(1);
                }
                break;
            case Profession.Merchant:
                if (true) //TODO: check previous investment
                {
                    int yield = Random.Range(1, 3);
                    Wealth += yield;
                    SayMessage("margin +" + yield);
                    yield return new WaitForSeconds(1);
                }
                if (Wealth >= 1)
                {
                    Wealth -= 1;
                    SayMessage("Trade Investment -1");
                    yield return new WaitForSeconds(1);
                }
                break;
            case Profession.Knight:
                
                break;
            default:
                break;
        }
    }

    int GetProperty()
    {
        return Wealth;
    }


    public void MoveTo(GameObject landObject)
    {
        //get out of currentland
        CurrentLand = landObject;
    }

    public void SayMessage(string message)
    {
        var textObject = Instantiate(RisingTextPrefab);
        textObject.transform.SetParent(canvasObject.transform);
        var risingMessage = textObject.GetComponent<RisingMessageScript>();
        risingMessage.setTargetObject(gameObject);
        risingMessage.setMessage(message);
    }

    void ShowPanel()
    {
        worldObject.GetComponent<WorldScript>().SetPanelTarget(gameObject);
    }

    void ShowHighlight()
    {

    }

#region mouse interaction

    void OnMouseDown()
    {
    }

    void OnMouseUpAsButton()
    {
        if (worldObject.GetComponent<WorldScript>().selectedObject == gameObject)
        {
            worldObject.GetComponent<WorldScript>().selectedObject = null;
        }
        else
        {
            worldObject.GetComponent<WorldScript>().selectedObject = gameObject;
        }
        
    }

    void OnMouseEnter()
    {
        ShowPanel();
        //Debug.Log("Enter Person");
        mouseOver = true;
    }
    void OnMouseExit()
    {
        //Debug.Log("Exit Person");
        mouseOver = false;
    }

    void OnMouseUp()
    {
        //Debug.Log("mouseup has been called!");

    }

    #endregion

}
