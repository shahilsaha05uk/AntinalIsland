using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour, ISelectable
{
    [SerializeField] private int buyCost = 10;
    [SerializeField] private int sellPrice = 5;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float attackRate = 1f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private GameObject projectilePrefab;

    private float _attackCooldown = 0f; // Internal timer to track attack intervals
    private bool _active = false;

    [SerializeField] private Transform projectileSpawnPos;
    public UnityEvent selectEvent;
    public UnityEvent deselectEvent;

    private GridCell _occupiedCell;

    private void Update()
    {
        if (!_active) return;

        _attackCooldown -= Time.deltaTime;
        if (_attackCooldown <= 0f)
        {
            GameObject target = FindNearestEnemy();
            if (target != null)
            {
                Attack(target);
                _attackCooldown = attackRate;
            }
        }
    }

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= attackRange)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    private void Attack(GameObject target)
    {
        // Create a projectile and set it to track the target
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPos.transform.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.SetTarget(target, attackDamage);
    }

    public void SetOccupiedCell(GridCell cell)
    {
        _occupiedCell = cell;
    }

    public void Deselect()
    {
        SelectTowerEvent.Trigger(null);
        deselectEvent.Invoke();
    }

    public void Select()
    {
        SelectTowerEvent.Trigger(this);
        selectEvent.Invoke();
    }

    public void UpgradeTower()
    {
        // Logic for upgrading the tower
        Debug.Log($"Upgrade {this}");
    }

    public void SellTower()
    {
        ResourceManager.Instance.AddResources(sellPrice);
        _occupiedCell.SetOccupied(false);
        Deselect();
        Destroy(this.gameObject);
    }

    public int GetSellPrice()
    {
        return sellPrice;
    }

    public int GetBuyCost()
    {
        return buyCost;
    }

    public void SetActive(bool active)
    {
        _active = active;
    }

}
