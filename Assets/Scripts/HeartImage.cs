using UnityEngine;


[RequireComponent(typeof(Animator))]
public class HeartImage : MonoBehaviour
{
    // ==============================================================================================
    public string HeartBlinkTriggerName = "blink";

    public Animator Anim { get; private set; }


    // ==============================================================================================
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public void Blink()
    {
        Anim.SetTrigger(HeartBlinkTriggerName);
    }

    protected void Awake()
    {
        Anim = GetComponent<Animator>();
    }
}
