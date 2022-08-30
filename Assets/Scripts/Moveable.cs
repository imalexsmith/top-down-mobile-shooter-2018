using UnityEngine;


[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public abstract class Moveable : MonoBehaviour
{
    // ==============================================================================================
    [Header("Moveable")]
    public bool CanMove = true;
    public float MoveSpeed = 1f;
    public Vector2 MoveDirection = Vector2.zero;

    public Collider2D Col { get; private set; }
    public Rigidbody2D Rgdb { get; private set; }


    // ==============================================================================================
    public virtual void StopMovement()
    {
        MoveDirection = Vector2.zero;
        CanMove = false;
    }

    protected virtual void Awake()
    {
        Col = GetComponent<Collider2D>();
        Rgdb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        CanMove = true;
    }

    protected virtual void FixedUpdate()
    {
        if (CanMove)
            Movement();
    }

    protected virtual void Movement()
    {
        if (MoveDirection != Vector2.zero)
        {
            var delta = MoveDirection * Time.smoothDeltaTime * MoveSpeed;

            var oldPos = Rgdb.position;
            var newPos = oldPos + delta;

            var rotSign = (newPos.y < oldPos.y) ? -1.0f : 1.0f;
            var rotAngle = Vector2.Angle(Vector2.right, delta) * rotSign;

            Rgdb.position = newPos;
            Rgdb.rotation = rotAngle;
        }
    }
}
