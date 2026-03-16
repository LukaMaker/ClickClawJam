using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EmployeeWander : MonoBehaviour
{
    public Collider walkArea;
    
    public float movementSpeed = 2f;
    public float changeDirectionInterval = 3f;

    private Rigidbody rb;
    private Vector3 targetPosition;
    private float timer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.linearDamping = 2f;

        PickNewTargetPosition();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= changeDirectionInterval)
        {
            PickNewTargetPosition();
        }
    }

    private void FixedUpdate()
    {
        if (walkArea == null) return;

        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0;

        if (direction.sqrMagnitude > 0.01f)
        {
            rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime);
        }
        else
        {
            PickNewTargetPosition();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        PickNewTargetPosition();
    }

    private void PickNewTargetPosition()
    {
        if (walkArea == null) return;

        timer = 0f;
        Bounds bounds = walkArea.bounds;

        targetPosition = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            transform.position.y,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
