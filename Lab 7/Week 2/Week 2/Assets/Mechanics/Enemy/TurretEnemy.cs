using UnityEngine;

[RequireComponent(typeof(Shoot))]
public class TurretEnemy : Enemy
{
    [SerializeField] private float projectileFireRate = 2.0f;
    private float timeSinceLastFire = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        if (projectileFireRate <= 0)
            projectileFireRate = 2.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Idle"))
            CheckFire();
    }

    private void CheckFire()
    {
        if (Time.time >= timeSinceLastFire + projectileFireRate)
        {
            anim.SetTrigger("Fire");
            timeSinceLastFire = Time.time;
        }
    }
}
