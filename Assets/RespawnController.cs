using UnityEngine;

public class RespawnMenuController : MonoBehaviour
{
    public GameObject respawnMenu;     // panel to show
    public Health playerHealth;        // reference to player health

    void Start()
    {
        respawnMenu.SetActive(false);  // make sure it starts hidden
    }

    public void ShowMenu()
    {
        respawnMenu.SetActive(true);
    }

    public void RespawnPlayer()
    {
        respawnMenu.SetActive(false);

        playerHealth.Respawn();        // call Respawn() from your health script
    }
}
