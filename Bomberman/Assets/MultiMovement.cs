using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

// Class responsible for player's movement behaviour in multiplayer
// Have to be in main asset folder!
public class MultiMovement : NetworkBehaviour
{
    public float moveHorizontal;
    public float moveVertical;
    public Sprite secondP;
    public GameObject bomb;
    
    private float speed = 3.0f;
    private Rigidbody2D player;
    private Vector2 move;
    private Vector3 position; 
    [SyncVar] private ushort power = 2;
    private ushort availableBombs = 1;

    
    // Client's function called on start
    [Client]
    void Start() 
    {
        
        player = GetComponent<Rigidbody2D>();
        if (!hasAuthority)
        {
            GetComponent<SpriteRenderer>().sprite = secondP;
        }
    }

    // Client's function called once per frame responsible for movement of player and his actions
    [Client]
    void Update()
    {
        if (!hasAuthority || !Network.done) { return; }
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        move = new Vector2(moveHorizontal, moveVertical);
        
        if (Input.GetKeyDown(KeyCode.Space) && availableBombs > 0 && bomb != null)
        {
            availableBombs -= 1;
            Debug.Log("Power: " + power);
            CmdSpawn(position, power);
            Invoke(nameof(IncrementBombs), 3f);
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            BoxCollider2D col =gameObject.GetComponent<BoxCollider2D>();
            col.enabled = !col.enabled;
        }
    }

    // Client's function called once per fixed frame-rate frame
    [Client]
    private void FixedUpdate()
    {
        if (!hasAuthority || !Network.done) { return; }
        position = player.position + move * (speed * Time.fixedDeltaTime);
        player.MovePosition(position);
    }

    // Function called on collision (in this case with explosion)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Soda"))
        {
            Debug.Log("Player is dead :c");
            
            if(isServer) { Network.singleton.StopHost(); }
            else { Network.singleton.StopClient(); }

            if(SceneManager.GetActiveScene().name == "MultiGame" && hasAuthority) { SceneManager.LoadScene("LostMulti"); }
            else { SceneManager.LoadScene("WonMulti"); }
        }
    }

    // Function to increase number of bombs by 1
    private void IncrementBombs()
    {
        availableBombs += 1;
    }
    
    // Client's function responsible for granting a bonus to the player
    [Client]
    public void GetBonus(int value)
    {
        if (!hasAuthority || !Network.done) { return; }
        switch (value)
        {
            case 0:
                Debug.Log("bonus speed");
                speed += 0.2f;
                break;
            case 1:
                Debug.Log("bonus bomb");
                availableBombs += 1;
                break;
            case 2:
                Debug.Log("bonus power");
                power += 1;
                break;
            default:
                Debug.Log("somethings wrong, i can feel it");
                break;
        }
    }

    // Command for spawning bomb on other clients
    [Command]
    public void CmdSpawn(Vector3 pos, int pow)
    {
        GameObject newBomb =  Instantiate(bomb, new Vector3(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)), Quaternion.identity);
        newBomb.GetComponent<BombMulti>().Boom(pow);
        NetworkServer.Spawn(newBomb, connectionToClient);
    }
}
