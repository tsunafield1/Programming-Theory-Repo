using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public abstract class Animal : MonoBehaviour // ABSTRACTION
{
    [SerializeField] protected float speed;
    public float Speed // ENCAPSULATION
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
    public float JumpForce // ENCAPSULATION
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

    protected void Start() // ABSTRACTION
    {
        animalRb = GetComponent<Rigidbody>();
    }

    // Check and move object every behaviour
    public virtual void Move() // ABSTRACTION
    {
        Walk();
        Jump();
    }

    // Control moving in x and z axis
    protected virtual void Walk() // ABSTRACTION
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.position += Vector3.forward * verticalInput * speed * Time.deltaTime;
        transform.position += Vector3.right * horizontalInput * speed * Time.deltaTime; ;
    }

    // Jump and set jump-state when press space key
    protected virtual void Jump() // ABSTRACTION
    {
        if (!isJump && Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
            animalRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Reset jump-state when on ground
    protected virtual void OnCollisionEnter(Collision collision) // ABSTRACTION
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }
    }
}
