using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private bool canElevate = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void setElevateDelay()
    {
        canElevate = true;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetAxis("Use") == 1f)
            switch (other.gameObject.tag)
            {
                case "ObjectToInteract":
                    Debug.Log("objectUse");
                    break;
                case "Elevator":
                    {
                        if (canElevate)
                        {
                            canElevate = false;
                            if (transform.position.y < 1)
                                transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
                            else if (transform.position.y < 3)
                                transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
                            Invoke("setElevateDelay", 1f);
                        }
                    }
                    break;
            }
    }
}
