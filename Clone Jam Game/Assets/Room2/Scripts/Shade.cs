using System;
using Unity.VisualScripting;
using UnityEngine;

public class Shade : MonoBehaviour
{
    public GameObject player;
    public Inspector inspector;
    public float shadowTreshHold;

    private bool playerInside;

    private void Update()
    {
        if (player.transform.position.x - transform.position.x <= 1)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= shadowTreshHold)
            {
                PlayerUnderShade();
            }
            else
            {
                PlayerExitShade();
            }
        }
        else {
            PlayerExitShade();
        }
    }

    public void PlayerExitShade()
    {
        if (playerInside)
        {
            playerInside = false;
            inspector.ExitShade(); 
        }
    }

    public void PlayerUnderShade()
    {
        if (!playerInside)
        {
            playerInside = true;
            inspector.EnterShade();
        }
    }
}
