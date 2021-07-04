using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterHealth : Health, IDamageable<int>
{
    public SpriteRenderer spriteRenderer;
    
    public override void TakeDamage(int damage)
    {

        base.TakeDamage(damage);

        if (CheckIfDead())
        {
            OnDeath();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            TakeDamage(100);
            HitFeedBack();

            if (CheckIfDead())
            {
                OnDeath();
            }
        }
    }

    protected override void HitFeedBack()
    {
        base.HitFeedBack();

        this.gameObject.transform.DOShakePosition(0.15f, new Vector3(0.4f, 0.1f, 0), 10, 90);
        Tween colorTween = spriteRenderer.DOBlendableColor(Color.red, 0.2f);
        colorTween.OnComplete(() => spriteRenderer.DOBlendableColor(Color.white, 0.05f));

    }

}
