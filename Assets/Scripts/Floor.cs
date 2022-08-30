using UnityEngine;


public class Floor : MonoBehaviour
{
    // ==============================================================================================
    protected virtual void OnTriggerExit2D(Collider2D otherObject)
    {
        var enemy = otherObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Kill();
            return;
        }

        var bullet = otherObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.gameObject.SetActive(false);
        }
    }
}
