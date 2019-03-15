using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public enum PlayerTeam { none, human, badger}
    [SerializeField] public PlayerTeam Team { get; set; }
    
    public float Health { get; set; }
    [SerializeField] int playerNumber;
    [SerializeField] int joystick;

    [SerializeField] GameObject prefab;


    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CreatePlayer(1, 1, PlayerTeam.human, prefab, new Vector3(0, 0, 0));
        }
    }

    public static GameObject CreatePlayer(int playerNum, int joystickNum, PlayerTeam playerTeam, GameObject playerPrefab, Vector3 position)
    {
        GameObject player = Instantiate(playerPrefab, position, Quaternion.Euler(0, 90, 0));
        PlayerScript playerScript = player.GetComponent<PlayerScript>();
        playerScript.playerNumber = playerNum;
        playerScript.joystick = joystickNum;
        playerScript.Team = playerTeam;

        return player;
    }
}
