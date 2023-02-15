using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellMouseScript : MonoBehaviour
{
    bool delete;
    bool spaceOccupied;
    Collider otherObject;

    GameLoop gameLoop;
    // Start is called before the first frame update
    void Start()
    {
        gameLoop = FindObjectOfType<GameLoop>();
    }

    // Update is called once per frame
    void Update()
    {
        if (delete == true && spaceOccupied == false)
        {
            delete = false;
        }

        if (spaceOccupied == true /*|| MoneyScript.moneyCount < cost*/)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            if (Input.GetMouseButtonDown(0) && otherObject.gameObject.tag != "Wall" && otherObject.gameObject.layer == 3 && spaceOccupied == true)
            {
                delete = true;
            }
        }

        if (delete == true)
        {
            otherObject.gameObject.GetComponent<AutoDestroyScript>().SellBuilding();

            var obj = otherObject.gameObject;

            if (obj.transform.CompareTag("Exchange")) gameLoop.RemoveExchangeCounter(obj);
            else if (obj.transform.CompareTag("Slot")) gameLoop.RemoveSlotMachine(obj);

            gameLoop.RemovePlacedObject(obj);

            delete = false;
            spaceOccupied = false;
        }
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


    }


}
