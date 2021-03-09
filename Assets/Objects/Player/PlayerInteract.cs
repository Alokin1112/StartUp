using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private bool canUse = true;
    public float roomHeight = 2f;
    public GameManager gameManager;
    private GameObject nearTo;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Use") == 1f && canUse && nearTo)
        {
            canUse = false;
            switch (nearTo.tag)
            {
                case "ObjectToInteract":
                    Debug.Log("objectUse");
                    break;
                case "Elevator":
                    {
                        nearTo.GetComponent<Elevator>().UseElevator(roomHeight);
                    }
                    break;
                case "Ammo":
                    {
                        gameManager.AddAmmo(1, nearTo);
                        nearTo = null;
                    }
                    break;
                case "ControlPanel":
                    {
                        gameManager.switchMove();
                        Debug.Log("Charging");
                        gameManager.Invoke("switchMove", 3f);
                    }
                    break;
                default:

                    break;
            }
            Invoke("setDelay", 1f);
        }
    }
    private void setDelay()
    {
        canUse = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Elevator":
            case "Ammo":
            case "ControlPanel":
                {
                    nearTo = other.gameObject;
                }
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == nearTo)
            nearTo = null;
    }
}
