using UnityEngine;
using Random = UnityEngine.Random;

// Bonuses:
// - Speed
// - Number of bombs
// - Power of bombs

// Class responsible for collectible's behaviour
public class Collectible : MonoBehaviour
{
    public Sprite[] graphicsArr;
    private int version;

    // Function called on start
    public void Start()
    {
        version = Random.Range(0, graphicsArr.Length);
        gameObject.GetComponent<SpriteRenderer>().sprite = graphicsArr[version];
    }

    // Function called on collision (in this case with player)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        other.SendMessage("GetBonus", version);
        Destroy(gameObject, 0.1f);
    }
}
