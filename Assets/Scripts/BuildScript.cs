using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildScript : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int cost;
    bool spaceOccupied;
    bool delete;
    GameObject moneyObject;
    [SerializeField] GameObject emptyMouse;
    int money;
    [SerializeField] LayerMask mask;
    [SerializeField] Collider otherObject;
    
    GameLoop gameLoop;
    
    // Start is called before the first frame update
    void Start()
    {
        moneyObject = GameObject.FindWithTag("Money");

        gameLoop = FindObjectOfType<GameLoop>();
    }

    private void OnTriggerEnter(Collider other)
    {
        spaceOccupied = true;
        otherObject = other;
    }

    private void OnTriggerExit(Collider other)
    {
        spaceOccupied = false;
        otherObject = null;


    }

    private void OnTriggerStay(Collider other)
    {
        spaceOccupied = true;
        //if (otherObject = null)
        //{
        //    otherObject = other;
        //}
        //if (other == null)
        //{
        //    spaceOccupied = false;
        //}

        if (delete == true)
        {
            other.gameObject.GetComponent<AutoDestroyScript>().SellBuilding();

            var obj = other.gameObject;
            
            if (obj.transform.CompareTag("Exchange")) gameLoop.RemoveExchangeCounter(obj);
            else if (obj.transform.CompareTag("Slot")) gameLoop.RemoveSlotMachine(obj);
            
            delete = false;
            spaceOccupied = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (delete == true && spaceOccupied == false)
        {
            delete = false;
        }
        if (spaceOccupied == true || moneyObject.GetComponent<MoneyScript>().moneyCount < cost)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            if (Input.GetMouseButtonDown(1) && otherObject.gameObject.tag != "Wall" && spaceOccupied == true)
            {
                delete = true;
            }
        }
        else
        {
            Ray rayray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(rayray, out hit, 10000, mask);
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            if (Input.GetMouseButtonDown(0) && hit.collider.gameObject.layer == 6 && moneyObject.GetComponent<MoneyScript>().moneyCount > cost)
            {
                var obj = Instantiate(prefab, gameObject.transform.position, gameObject.transform.rotation);
                if (obj.transform.CompareTag("Exchange")) gameLoop.AddExchangeCounter(obj);
                else if (obj.transform.CompareTag("Slot")) gameLoop.AddSlotMachine(obj);
                moneyObject.GetComponent<MoneyScript>().moneyCount -= cost;
                gameObject.SetActive(false);
                emptyMouse.SetActive(true);
            }
        }

        
        

    }

    
}
