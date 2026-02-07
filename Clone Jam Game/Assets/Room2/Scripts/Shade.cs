using System;
using UnityEngine;
using static Player;

public class Shade : MonoBehaviour
{
    public static event Action  PlayerEnterShade;
    public static event Action PlayerExitShade;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") { 
            PlayerEnterShade?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") { 
            PlayerExitShade?.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerEnterShade?.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerExitShade?.Invoke();
        }
    }
}
