using UnityEngine;


public class Aggression : MonoBehaviour
{
    // ==============================================================================================
    private Player _player;


    // ==============================================================================================
    protected virtual void Start()
    {
        _player = GetComponentInParent<Player>();
    }

    protected virtual void OnTriggerStay2D(Collider2D otherObject)
    {
        var enemy = otherObject.GetComponent<Enemy>();
        if (enemy != null && enemy.CanMove)
        {
            enemy.AggressionNow = true;
            enemy.NewTarget(_player.transform.position);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D otherObject)
    {
        var enemy = otherObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.AggressionNow = false;
            enemy.NewRandomTarget();
        }
    }
}
