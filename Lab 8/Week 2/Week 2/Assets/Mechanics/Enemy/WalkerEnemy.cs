using System.IO;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody2D))]
public class WalkerEnemy : Enemy
{
    private Rigidbody2D rb;
    [SerializeField] private float xVel = 0;
    public float speed = 1.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        if (xVel <= 0) xVel = 3;
    }

    public override void TakeDamage(int DamageValue, DamageType damageType = DamageType.Default)
    {
        if (damageType == DamageType.JumpedOn)
        {
            anim.SetTrigger("Squish");
            Destroy(transform.parent.gameObject, 0.5f);
            return;
        }

        base.TakeDamage(DamageValue, damageType);
    }

    private void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Walk"))
        {
            Vector2 move = transform.position;
            Vector2 movementDir = move * Vector2.right * (speed * Time.fixedDeltaTime);
            rb.MovePosition(rb.position + movementDir);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Barrier"))
        {
            anim.SetTrigger("Turn");
            sr.flipX = !sr.flipX;
        }
    }
}