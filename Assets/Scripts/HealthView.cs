using UnityEngine;


public class HealthView : MonoBehaviour
{
    // ==============================================================================================
    public int CurrentHealth;
    public HeartImage HeartImagePrefab;


    // ==============================================================================================
    public void UpdateUI()
    {
        if (CurrentHealth >= 0 && HeartImagePrefab != null)
        {
            var visibleHealth = transform.childCount;
            var delta = CurrentHealth - visibleHealth;

            if (visibleHealth < CurrentHealth)
            {
                for (int i = 0; i < delta; i++)
                {
                    var newHeart = Instantiate(HeartImagePrefab, transform);
                    newHeart.gameObject.SetActive(true);
                }
            }

            if (visibleHealth > CurrentHealth)
            {
                for (int i = visibleHealth - 1; i >= CurrentHealth; i--)
                {
                    var heartGO = transform.GetChild(i);
                    if (heartGO != null)
                    {
                        var heart = heartGO.GetComponent<HeartImage>();
                        if (heart != null)
                            heart.Blink();
                        else
                            Destroy(heartGO.gameObject);
                    }
                }
            }
        }
    }
}
