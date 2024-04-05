using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject target;
    private int damage;
    private float speed = 10f; // Speed at which the projectile will move towards the target

    public void SetTarget(GameObject newTarget, int newDamage)
    {
        target = newTarget;
        damage = newDamage;
        transform.LookAt(target.transform);
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // Destroy the projectile if the target is null
            return;
        }

        Vector3 targetPosition = target.transform.position;
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distanceThisFrame = speed * Time.deltaTime;
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        // Check if the projectile will hit the target this frame
        if (distanceToTarget <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Move the projectile towards the target
        transform.Translate(direction * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        Health enemyHealth = target.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }

        Destroy(gameObject); // Destroy the projectile after hitting the target
    }
}