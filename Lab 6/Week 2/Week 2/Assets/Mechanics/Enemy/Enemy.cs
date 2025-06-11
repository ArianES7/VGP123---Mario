using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public abstract class Enemy : MonoBehaviour
{
    //private, protected, public
    //private - Private varibles are accessable only by the class that declares them.
    //protected - Private but also accessable by subclasses
    //public - Public variables are are party, everyone is invited to modify and change these values

    public AudioClip deathSound;

    protected SpriteRenderer sr;
    protected Animator anim;
    protected int health;
    [SerializeField] protected int maxHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (maxHealth <= 0) maxHealth = 5;

        health = maxHealth;
    }

    public virtual void TakeDamage(int DamageValue, DamageType damageType = DamageType.Default)
    {
        health -= DamageValue;

        if (health <= 0)
        {
            anim.SetTrigger("Death");
            GetComponent<AudioSource>().PlayOneShot(deathSound);

            if (transform.parent != null)
                Destroy(transform.parent.gameObject, 0.5f);
            else
                Destroy(gameObject, 0.5f);
        }
    }
}

public enum DamageType
{
    Default,
    JumpedOn
}