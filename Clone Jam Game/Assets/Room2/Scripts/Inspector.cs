using UnityEngine;

public class Inspector : MonoBehaviour
{
    public GameObject Player;
    public bool playerIsSafe;

    void Start()
    {
        Shade.PlayerEnterShade += PlayerUnderShade;
        Shade.PlayerExitShade += PlayerExitShade;
        ToilateDoor.PlayerOnTheToilate += PlayerOnTheToilate;
        ToilateDoor.PlayerExitTheToilate += PlayerExitTheToilate;
}

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayerExitShade() {
        if (playerIsSafe) {
            playerIsSafe = false;
        }
    }

    public void PlayerUnderShade() {
        if (!playerIsSafe){
            playerIsSafe = true;
        }
    }

    public void PlayerOnTheToilate() {
        playerIsSafe = true;
    }

    public void PlayerExitTheToilate() {
        playerIsSafe = false; 
    }

}
