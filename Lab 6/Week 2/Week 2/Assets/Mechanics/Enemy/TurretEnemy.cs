using UnityEngine;

[RequireComponent(typeof(Shoot))]
public class TurretEnemy : Enemy
{
    [SerializeField] private float projectileFireRate = 2.0f;
    private float timeSinceLastFire = 0;
    private PlayerController playerRef;

    private void Awake()
    {
        GameManager.Instance.OnPlayerControllerCreated += SetPlayerRef;
    }

    private PlayerController SetPlayerRef(PlayerController playerInstance)
    {
        playerRef = playerInstance;
        return playerInstance;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        if (projectileFireRate <= 0)
            projectileFireRate = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Idle"))
            CheckFire();
    }

    void CheckFire()
    {
        if (Time.time >= timeSinceLastFire + projectileFireRate)
        {
            anim.SetTrigger("Fire");
            timeSinceLastFire = Time.time;
        }
    }
}