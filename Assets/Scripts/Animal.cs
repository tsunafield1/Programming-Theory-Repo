using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public abstract class Animal : MonoBehaviour
{
    [SerializeField] protected float speed;
    public float Speed
    {
        get { return speed; }
        set
        {
            if (value > 0.0f)
            {
                speed = value;
            }
        }
    }
    [SerializeField] protected float jumpForce;
    public float JumpForce
    {
        get { return jumpForce; }
        set
        {
            if (value > 0.0f)
            {
                jumpForce = value;
            }
        }
    }
    protected bool isJump;
    protected Rigidbody animalRb;

    protected void Start()
    {
        animalRb = GetComponent<Rigidbody>();
    }

    // Check and move object every behaviour
    public virtual void Move()
    {
        Walk();
        Jump();
    }

    // Control moving in x and z axis
    protected virtual void Walk()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.position += Vector3.forward * verticalInput * speed * Time.deltaTime;
        transform.position += Vector3.right * horizontalInput * speed * Time.deltaTime; ;
    }

    // Jump and set jump-state when press space key
    protected virtual void Jump()
    {
        if (!isJump && Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
            animalRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Reset jump-state when on ground
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }
    }
}
