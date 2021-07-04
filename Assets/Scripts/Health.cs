using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int health;

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
    }

    protected virtual void HitFeedBack()
    {
        Debug.Log("Hit feedback is playing");
    }

    protected virtual void OnDeath()
    {
        Debug.Log("YOU ARE DEAD! ");
    }

    protected bool CheckIfDead()
    {
        if (health <= 0)
        {
            health = 0;
            return true;
        }
        return false;
    }
}
