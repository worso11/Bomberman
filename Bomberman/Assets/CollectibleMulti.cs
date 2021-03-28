using System.Collections.Generic;
using Mirror;
using UnityEngine;

// Bonuses:
// - Speed
// - Number of bombs
// - Power of bombs

// Class responsible for collectible's behaviour in multiplayer
// Have to be in main asset folder!
public class CollectibleMulti : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public List<Sprite> graphicsArr;
    [SyncVar(hook = nameof(SpawnCol))] public int version;
    
    // Function to change the sprite of the collectible
    private void SpawnCol(int oldVersion, int i)
    {
        version = i;
        if (!spriteRenderer) { spriteRenderer = GetComponent<SpriteRenderer>(); }

        spriteRenderer.sprite = graphicsArr[version];
        Debug.Log("Power spawned: " + version); 
    }
    
    // Function called on collision (in this case with player)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        other.SendMessage("GetBonus", version);
        Destroy(gameObject, 0.1f);
    }
}
