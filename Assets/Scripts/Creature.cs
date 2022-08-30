using UnityEngine;


[RequireComponent(typeof(Animator))]
public abstract class Creature : Moveable
{
    // ==============================================================================================
    [Header("Creature")]
    public int StartHealth = 1;

    [Header("Animation")]
    public string InjuredAnimName = "injured";
    public string DamageTriggerName = "do_damage";

    public int CurrentHealth { get; private set; }
    public bool IsDead { get { return CurrentHealth <= 0; } }
    public Animator Anim { get; private set; }


    // ==============================================================================================
    public virtual bool DoDamage()
    {
        var anim = Anim.GetCurrentAnimatorStateInfo(0);

        if (!anim.IsName(InjuredAnimName))
        {
            CurrentHealth--;
            Anim.SetTrigger(DamageTriggerName);
            return true;
        }

        return false;
    }

    public abstract void Kill();

    protected override void Awake()
    {
        base.Awake();

        Anim = GetComponent<Animator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        CurrentHealth = StartHealth;
    }
}
