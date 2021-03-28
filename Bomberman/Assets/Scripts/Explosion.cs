using UnityEngine;

// Class responsible for explosion's behaviour
public class Explosion : MonoBehaviour
{
    
    // Function called on start
    void Start()
    {
        Destroy(gameObject, 0.75f);
    }
}