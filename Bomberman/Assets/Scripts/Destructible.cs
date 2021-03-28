using UnityEngine;
using Random = UnityEngine.Random;

// Class responsible for destructible's behaviour
public class Destructible : MonoBehaviour
{
    public GameObject coll;
    
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
        if (Random.Range(0, 10) >= 7)
        {
            Instantiate(coll, gameObject.transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
