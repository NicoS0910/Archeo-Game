using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    public float damage = 3;
    Vector2 rightAttackOffset;
    public Harvesting tool;

    private void Start()
    {
        rightAttackOffset = transform.localPosition;
    }

    public void AttackRight()
    {
        print("Attack Right");
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }
    public void AttackLeft()
    {
        print("Attack Left");
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }
    public void AttackDown()
    {
        print("Attack Down");
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * 0, rightAttackOffset.y * 2);
    }
    public void AttackUp()
    {
        print("Attack Up");
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * 0, rightAttackOffset.y * -2);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyHitbox")
        {
            // Deal damage to enemy
            Enemy enemy = other.GetComponentInParent<Enemy>(); // Sucht den Enemy-Komponenten im Parent

            if (enemy != null)
            {
                enemy.Health -= damage;
            }
        }
        if (tool != null)
        {
            tool.OnSwordHit(other);
        }
    }
}
