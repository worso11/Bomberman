using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    public GameObject bomb;
    public GameObject explosion;
    private Vector2 position;
    private int power;
    private bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        position = bomb.transform.position;
        Invoke(nameof(Collision), 0.5f);
        
    }
    
    private void Explode()
    {
        exploded = true;
        Destroy(gameObject, .2f * power);
        Instantiate(explosion, position, Quaternion.identity);
        StartCoroutine(CreateExplosion(Vector2.right));
        StartCoroutine(CreateExplosion(Vector2.left));
        StartCoroutine(CreateExplosion(Vector2.up));
        StartCoroutine(CreateExplosion(Vector2.down));
    }

    private IEnumerator CreateExplosion(Vector2 direction)
    {
        int layer = 1 << 9;
        for (int p = 1; p < power; p++)
        {
            if (Physics2D.Raycast(position, direction, p, layer))
            {
                Destroy(gameObject);
                break;
            }
            
            if (Physics2D.Raycast(position, direction, p, layer << 1))
            {
                Instantiate(explosion, position + (direction*p), Quaternion.identity);
                break;
            }
            Instantiate(explosion, position + (direction*p), Quaternion.identity);
            yield return new WaitForSeconds(.15f);
        }
    }
    private void Collision()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void Boom(int pow)
    {
        power = pow;
        Invoke(nameof(Explode), 3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Soda") && exploded==false)
        {
            Debug.Log("Chain Reaction");
            CancelInvoke(nameof(Explode));
            Explode();
        }
    }
}
