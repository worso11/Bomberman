using UnityEngine;
using System.Collections;
using Mirror;

// Class responsible for bomb's behaviour in multiplayer
// Have to be in main asset folder!
public class BombMulti : NetworkBehaviour
{
    [SyncVar]
    public GameObject bomb;
    public GameObject explosion;
    
    private Vector2 position;
    private int power;
    private bool exploded = false;

    // Function called on start
    void Start()
    {
        position = bomb.transform.position;
        Invoke(nameof(Collision), 0.5f);
        
    }
    
    // Function instantiating explosion at bomb's position
    private void Explode()
    {
        if (!isServer) { return; }
        Debug.Log("Wszedlem");
        exploded = true;
        Collision();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, .2f * power);
        CmdExplode(position);
        StartCoroutine(CreateExplosion(Vector2.right));
        StartCoroutine(CreateExplosion(Vector2.left));
        StartCoroutine(CreateExplosion(Vector2.up));
        StartCoroutine(CreateExplosion(Vector2.down));
    }
    
    // Function to spawn initial explosion on all clients
    public void CmdExplode(Vector3 pos)
    {
        GameObject newExplode =  Instantiate(explosion, pos, Quaternion.identity);
        NetworkServer.Spawn(newExplode, connectionToClient);
    }

    // Function creating further explosion based on bomb's power
    private IEnumerator CreateExplosion(Vector2 direction)
    {
        int layer = 1 << 9;
        for (int p = 1; p < power; p++)
        {
            Vector3 pos = position + (direction * p);
            if (Physics2D.Raycast(position, direction, p, layer))
            {
                break;
            }
            
            if (Physics2D.Raycast(position, direction, p, layer << 1))
            {
                CmdExplosion(pos);
                break;
            }
            CmdExplosion(pos);
            yield return new WaitForSeconds(.15f);
        }
    }
    
    // Function to spawn further explosions on all clients
    // Probably have to be kept as a separate function from initial explosion
    public void CmdExplosion(Vector3 pos)
    {
        GameObject newExplosion =  Instantiate(explosion, pos, Quaternion.identity);
        NetworkServer.Spawn(newExplosion, connectionToClient);
    }
    
    // Function to enable collision on object
    private void Collision()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    
    // Function overriding power value and invoking explosion
    public void Boom(int pow)
    {
        power = pow;
        Debug.Log("Power: " + power);
        //if (!hasAuthority) { Debug.Log("Blad2"); return; }
        Invoke(nameof(Explode), 3f);
        
    }

    // Function creating chain reaction of explosions
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Soda") && exploded==false)
        {
            CancelInvoke(nameof(Explode));
            Explode();
        }
    }

    // Function returning value of bomb's power (redundant in multi)
    public int GetPower()
    {
        return power;
    }
}
