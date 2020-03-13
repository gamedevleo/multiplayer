using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public GameObject playerPref;
    public static NetworkManager instance;
    public  int myConnectionID;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        NetworkConfig.InitNetwork();
        NetworkConfig.ConnectToServer();
    }

    private void OnApplicationQuit()
    {
        NetworkConfig.DisconnectFromServer();
    }

    public void InstantiateNetworkPlayer(int connectionID, bool isMyPlayer)
    {
        GameObject go = Instantiate(playerPref);
        go.name = "Player: " + connectionID;

        if (isMyPlayer)
            go.AddComponent<InputManager>();

        GameManager.instance.playerList.Add(connectionID, go);
    }
}
