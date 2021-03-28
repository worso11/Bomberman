using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;

// Class responsible for destructible's behaviour in multiplayer
// Have to be in main asset folder!
public class DestructibleMulti : NetworkBehaviour
{
    [SyncVar]
    public GameObject col;
    [SyncVar] private int random;
    
    // Function called on collision (in this case with explosion)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Soda"))
        {
            DestroyBlock();
        }
    }

    // Function responsible for destroying the block and spawning collectible
    private void DestroyBlock()
    {
        if (!isServer) { return; }
        if (Random.Range(0, 10) >= 0)
        {
            random = Random.Range(0, 3);
            Debug.Log("Power spawned: " + random); 
            GameObject coll = Instantiate(col, gameObject.transform.position, Quaternion.identity);
            coll.GetComponent<CollectibleMulti>().version = random;
            NetworkServer.Spawn(coll, connectionToClient);
        }
        Destroy(gameObject);
    }
}
