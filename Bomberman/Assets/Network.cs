using Mirror;
using UnityEngine;

// Class overriding Mirror's NetworkManager
// Have to be in main asset folder!
public class Network : NetworkManager
{
    private GameObject p;
    private NetworkConnection con;
    public static bool done = true;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(numPlayers);
        }
        
        if (numPlayers == 1 && done)
        {
            done = false;
        }
        
        if (numPlayers >= 2 && !done)
        {
            done = true;
        }
    }

    // Function to add player when he connects
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        con = conn;
        Transform startPos = GetStartPosition();
        GameObject player = startPos != null
            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab);
        p = player;
        NetworkServer.AddPlayerForConnection(con, p);
    }
}
