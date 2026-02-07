using System;
using UnityEngine;
using static Player;

public class Shade : MonoBehaviour
{
    public static event Action  PlayerEnterShade;
    public static event Action PlayerExitShade;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
