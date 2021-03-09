using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public bool isToUp = true;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    public void UseElevator(float roomHeight)
    {
        Vector3 playerPos = player.transform.position;
        if (isToUp)
        {
            player.transform.position = new Vector3(playerPos.x, playerPos.y + roomHeight, playerPos.z);
        }
        else
        {
            player.transform.position = new Vector3(playerPos.x, playerPos.y - roomHeight, playerPos.z);
        }
    }
}
