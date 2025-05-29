using UnityEngine;

public abstract class Pickups : MonoBehaviour
{
    public float lifetime = 0.2f;

    //Function to be called when the player collides with the pickup
    abstract public void OnPickup(GameObject player);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if the player collided with the pickup
        if (collision.CompareTag("Player"))
        {
            //Call the OnPickup function and pass the player object
            OnPickup(collision.gameObject);
            //Destroy the pickup object
            Destroy(gameObject, lifetime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if the player collided with the pickup
        if (collision.gameObject.CompareTag("Player"))
        {
            //Call the OnPickup function and pass the player object
            OnPickup(collision.gameObject);
            //Destroy the pickup object
            Destroy(gameObject, lifetime);
        }
    }
}