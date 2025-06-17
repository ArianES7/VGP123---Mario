using UnityEngine;

public class GroundCheck
{
    private bool isGrounded;
    public bool IsGrounded => isGrounded;

    private LayerMask isGroundLayer;
    private Collider2D collider;
    private Rigidbody2D rb;
    private float groundCheckRadius;

    private Vector2 groundCheckPos => new Vector2(collider.bounds.min.x + collider.bounds.extents.x, collider.bounds.min.y);
    public GroundCheck(LayerMask isGroundLayer, Collider2D collider, Rigidbody2D rb, ref float groundCheckRadius)
    {
        this.isGroundLayer = isGroundLayer;
        this.collider = collider;
        this.rb = rb;
        this.groundCheckRadius = groundCheckRadius;
    }

    public bool CheckIsGrounded()
    {
        if (!isGrounded && rb.linearVelocityY <= 0)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, isGroundLayer);
        }
        else if (isGrounded) isGrounded = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, isGroundLayer);

        return isGrounded;
    }
}