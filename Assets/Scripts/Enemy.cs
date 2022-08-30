using System.Collections;
using UnityEngine;


public class Enemy : Creature
{
    // ==============================================================================================
    [Header("Enemy")]
    public Vector2 MinTargetPoint;
    public Vector2 MaxTargetPoint;
    public float HitPlayerStopTime;
    public bool AggressionNow;


    // ==============================================================================================
    public override void Kill()
    {
        var scoreView = FindObjectOfType<ScoreView>();
        if (scoreView != null)
        {
            scoreView.CurrentScoreCount++;
            scoreView.UpdateUI();
        }

        gameObject.SetActive(false);
    }

    public void NewTarget(Vector3 target)
    {
        MoveDirection = (target - transform.position).normalized;
    }

    public void NewRandomTarget()
    {
        var targetPoint = new Vector3(Random.Range(MinTargetPoint.x, MaxTargetPoint.x),
            Random.Range(MinTargetPoint.y, MaxTargetPoint.y), 0f);

        NewTarget(targetPoint);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // Index 0 - base anim layer, index 1 - player, index 2 - enemy
        Anim.SetLayerWeight(2, 1f);
    }

    protected virtual void OnCollisionEnter2D(Collision2D otherObject)
    {
        if (!IsDead)
        {
            if (!AggressionNow)
            {
                var enemy = otherObject.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    NewRandomTarget();
                    return;
                }
            }

            var bullet = otherObject.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                DoDamage();
                bullet.gameObject.SetActive(false);
                return;
            }

            var player = otherObject.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.DoDamage();
                CanMove = false;
                StartCoroutine(StartMoveDelayed(HitPlayerStopTime));
            }
        }
    }

    private IEnumerator StartMoveDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);

        CanMove = true;
        NewRandomTarget();
    }
}
