using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {
    
    public float moveHorizontal;
    public float moveVertical;
    private float speed = 3.0f;

    private Rigidbody2D player;
    public GameObject playerBody;
    public GameObject bomb;
    private Vector2 move;
    private Vector3 position; 
    private ushort power = 2;
    private ushort availableBombs = 1;

    
    // Start is called before the first frame update
    void Start() {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
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

        if (Input.GetKeyDown(KeyCode.F)) {
            BoxCollider2D col =gameObject.GetComponent<BoxCollider2D>();
            col.enabled = !col.enabled;
        }
    }

    private void FixedUpdate()
    {
        //player.velocity = move * speed;
        //position = playerBody.transform.position;
        position = player.position + move * (speed * Time.fixedDeltaTime);
        player.MovePosition(position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Soda"))
        {
            Debug.Log("Player is dead :c");
            if(SceneManager.GetActiveScene().name == "Game") {SceneManager.LoadScene("LostSingleMenu"); }
            else if(SceneManager.GetActiveScene().name == "RainOfBombs") { SceneManager.LoadScene("LostEndlessMenu"); }
            else if(SceneManager.GetActiveScene().name == "MultiGame") { SceneManager.LoadScene("LostSingleMenu"); }
        }
        else if (other.CompareTag("Food"))
        {
            power += 1;
            speed += 0.2f;
            availableBombs += 1;
        }
    }

    private void IncrementBombs()
    {
        availableBombs += 1;
    }

}
