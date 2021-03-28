using UnityEngine;
using UnityEngine.SceneManagement;

// Class responsible for player's movement behaviour
public class Movement : MonoBehaviour {
    
    public float moveHorizontal;
    public float moveVertical;
    public GameObject bomb;
    public bool playerTwo; 
    
    private float speed = 3.0f;
    private Rigidbody2D player;
    private Vector2 move;
    private Vector3 position; 
    private ushort power = 2;
    private ushort availableBombs = 1;


    // Function called on start
    void Start() {
        player = GetComponent<Rigidbody2D>();
    }

    // Function called once per frame responsible for movement of player and his actions
    void Update() {
        if (!playerTwo)
        {
            moveHorizontal = Input.GetAxisRaw("Horizontal");
            moveVertical = Input.GetAxisRaw("Vertical");
            move = new Vector2(moveHorizontal, moveVertical);
            
            if (Input.GetKeyDown(KeyCode.Space) && availableBombs > 0 && bomb != null)
            {
                availableBombs -= 1;
                GameObject newBomb =  Instantiate(bomb, new Vector3(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)), Quaternion.identity);
                newBomb.GetComponent<Bomb>().Boom(power);
                Invoke(nameof(IncrementBombs), 3f);
            }
        }
        else
        {
            moveHorizontal = Input.GetAxisRaw("Horizontal2");
            moveVertical = Input.GetAxisRaw("Vertical2");
            move = new Vector2(moveHorizontal, moveVertical);
            
            if (Input.GetKeyDown(KeyCode.Return) && availableBombs > 0 && bomb != null)
            {
                availableBombs -= 1;
                GameObject newBomb =  Instantiate(bomb, new Vector3(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)), Quaternion.identity);
                newBomb.GetComponent<Bomb>().Boom(power);
                Invoke(nameof(IncrementBombs), 3f);
            }
        }
    }

    // Function called once per fixed frame-rate frame
    private void FixedUpdate()
    {
        position = player.position + move * (speed * Time.fixedDeltaTime);
        player.MovePosition(position);
    }

    // Function called on collision (in this case with explosion)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Soda"))
        {
            Debug.Log("Player is dead :c");
            if(SceneManager.GetActiveScene().name == "Game" && playerTwo) {SceneManager.LoadScene("LostSingleMenu2"); }
            else if(SceneManager.GetActiveScene().name == "Game" && !playerTwo) {SceneManager.LoadScene("LostSingleMenu"); }
            else if (SceneManager.GetActiveScene().name == "RainOfBombs")
            {
                SceneManager.LoadScene("LostEndlessMenu");
            }
        }
    }

    // Function to increase number of bombs by 1
    private void IncrementBombs()
    {
        availableBombs += 1;
    }

    // Function responsible for granting a bonus to the player
    public void GetBonus(int value)
    {
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
}
