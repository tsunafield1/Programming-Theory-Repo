using UnityEngine;

public class Bird : Animal
{
    [SerializeField] private float flyForce;
    public float FlyForce
    {
        get { return flyForce; }
        set
        {
            if (value > 0.0f)
            {
                flyForce = value;
            }
        }
    }
    private float lastSpacePressTime;
    private float doubleTapTime = 0.25f;
    private bool isFly;

    public override void Move()
    {
        Fly();
        base.Move();
    }

    protected override void Jump()
    {
        // Can jump only when not flying
        if (!isFly)
        {
            base.Jump();
        }
    }

    // Control flying
    private void Fly()
    {
        if (isJump && Input.GetKeyDown(KeyCode.Space))
        {
            float timeSinceLastSpace = Time.time - lastSpacePressTime;
            lastSpacePressTime = Time.time;

            if (!isFly)
            {
                // Enable flying
                isFly = true;
                animalRb.useGravity = false;
            }
            else if (timeSinceLastSpace <= doubleTapTime)
            {
                // Disable flying
                isFly = false;
                animalRb.useGravity = true;
                return;
            }
        }

        if (isFly)
        {
            // Fly upward and downward based on input
            if (Input.GetKey(KeyCode.Space))
            {
                animalRb.AddForce(Vector3.up * flyForce);
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                animalRb.AddForce(Vector3.down * flyForce);
            }
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
            isFly = false;
            animalRb.useGravity = true;
            animalRb.linearVelocity = new Vector3(animalRb.linearVelocity.x, 0, animalRb.linearVelocity.z);
        }
    }
}
