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
        if (Input.GetAxis("Use") == 1f && canUse)
        {
            canUse = false;
            switch (nearTo.tag)
            {
                case "ObjectToInteract":
                    Debug.Log("objectUse");
                    break;
                case "Elevator":
                    {
                        if (transform.position.y < 1)
                            transform.position = new Vector3(transform.position.x, transform.position.y + roomHeight, transform.position.z);
                        else if (transform.position.y < 3)
                            transform.position = new Vector3(transform.position.x, transform.position.y - roomHeight, transform.position.z);
                    }
                    break;
                case "Ammo":
                    {
                        gameManager.AddAmmo(1, nearTo);
                        nearTo = null;
                    }
                    break;
            }
            Invoke("setDelay", 1f);
        }
    }
    private void setDelay()
    {
        Debug.Log("zzz");
        canUse = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Elevator":
                nearTo = other.gameObject;
                break;
            case "Ammo":
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
