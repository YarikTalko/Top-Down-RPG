using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;

    private float damage = 3;

    Vector2 rightAttackOffsset;

    private void Start()
    {
        rightAttackOffsset = transform.position;
    }

    public void AttackRight()
    {
        swordCollider.enabled = true;

        transform.localPosition = rightAttackOffsset;
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;

        transform.localPosition = new Vector2(rightAttackOffsset.x * -1, rightAttackOffsset.y);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        { 
            Enemy enemy = collider.GetComponent<Enemy>();

            if (enemy != null) 
            {
                enemy.Health -= damage;
            }
        }
    }
}
