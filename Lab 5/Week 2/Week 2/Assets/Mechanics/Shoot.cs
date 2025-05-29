using UnityEngine;

public class Shoot : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private Vector2 initShotVelocity = Vector2.zero;
    [SerializeField] private Transform spawnPointRight;
    [SerializeField] private Transform spawnPointLeft;

    [SerializeField] private Projectile projectilePrefab;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (initShotVelocity == Vector2.zero)
        {
            Debug.Log("Init shot velocity has not been set in the inspector, changing to default value");
            initShotVelocity.x = 7.0f;
        }

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
            Debug.Log($"Please set default spawn or projectile values on {gameObject.name}");
    }

    public void Fire()
    {
        Projectile curProjectile;
        if (!sr.flipX)
        {
            curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, Quaternion.identity);
            curProjectile.SetVelocity(initShotVelocity);
        }
        else
        {
            curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, Quaternion.identity);
            curProjectile.SetVelocity(-initShotVelocity);
        }
    }

}