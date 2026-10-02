using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public string playerName = "Player";
    public int hp = 100;
    public float moveSpeed = 5f;
    public float jumpPower = 8f;
    public Vector3 startPosition;
    public Transform visual;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    public float fallLimit = -10f;
    
    private bool isGrounded = false;
   public Vector2 airScale = new Vector2(0.8f, 1.2f);
   private float facing = 1f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = startPosition;
        Debug.Log(playerName + " 시작. 체력 " + hp);
    }
    void OnMove(InputValue value)
    {
    moveInput = value.Get<Vector2>();
    if (moveInput.x > 0) { facing = 1f; }
    else if (moveInput.x < 0) { facing = -1f; }
    }
    void Update()
    {
        transform.Translate(Vector3.right * moveInput.x * moveSpeed * Time.deltaTime);
 
        if (transform.position.y < fallLimit)
        {
          transform.position = startPosition;
          rb.linearVelocity = Vector2.zero;
          Debug.Log("낙사");
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.CompareTag("Ground"))
      {
        isGrounded = true;
        Debug.Log("착지");
      }
    }
 
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
          isGrounded = false;
          Debug.Log("공중");
        }
    }
    void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
          Debug.Log("점프!");
          rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        }    if (isGrounded)
    {
      visual.localScale = new Vector3(facing, 1f, 1f);
    }
    else
    {
      visual.localScale = new Vector3(
        facing * airScale.x, airScale.y, 1f);
    }

    }
  
}
