using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void FOnHealthUpdate(int value);
    public event FOnHealthUpdate OnHealthUpdate;


    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        OnHealthUpdate?.Invoke(health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        OnHealthUpdate = null;
        Destroy(gameObject);
    }
}
