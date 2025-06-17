using UnityEngine;

public abstract class Pickups : MonoBehaviour
{
    public AudioClip pickupSound; // Sound to play when the pickup is collected
    //public float lifetime = 0.2f;

    //Function to be called when the player collides with the pickup
    abstract public void OnPickup(GameObject player);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the player collided with the pickup
        if (collision.CompareTag("Player"))
        {
            PlayerPickup(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if the player collided with the pickup
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerPickup(collision.gameObject);
        }
    }

    private void PlayerPickup(GameObject player)
    {
        if (pickupSound)
            GetComponent<AudioSource>().PlayOneShot(pickupSound);
        //Call the OnPickup function and pass the player object
        OnPickup(player);

        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        GetComponent<SpriteRenderer>().enabled = false;

        //Destroy the pickup object
        Destroy(gameObject, pickupSound.length);
    }
}