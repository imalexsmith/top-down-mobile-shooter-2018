using System.Collections.Generic;
using UnityEngine;


public class Player : Creature
{
    // ==============================================================================================
    [Header("Player")]
    public bool ReadInput;
    public bool UseMobileInput;
    public float FireDelay;
    public Transform FirePoint;
    public Bullet BulletPrefab;
    public GameObject GameOverPanel;

    private Joystick _mobileJoystick;
    private HealthView _healthView;
    private readonly List<Bullet> _bullets = new List<Bullet>();
    private float _lastFireTime = -1f;


    // ==============================================================================================
    public override bool DoDamage()
    {
        var result = base.DoDamage();

        if (result)
        {
            _healthView.CurrentHealth = CurrentHealth;
            _healthView.UpdateUI();
        }

        return result;
    }

    public override void Kill()
    {
        if (IsDead)
        {
            StopMovement();
            ReadInput = false;

            if (GameOverPanel != null)
                GameOverPanel.SetActive(true);
        }
    }

    public void Fire()
    {
        if (Time.time - _lastFireTime > FireDelay)
        {
            FireImmediately();
        }
    }

    public void FireImmediately()
    {
        if (BulletPrefab != null && FirePoint != null)
        {
            _lastFireTime = Time.time;

            Bullet newBullet = null;

            for (int i = 0; i < _bullets.Count; i++)
            {
                if (!_bullets[i].isActiveAndEnabled)
                {
                    newBullet = _bullets[i];
                    break;
                }
            }

            if (newBullet == null)
            {
                newBullet = Instantiate(BulletPrefab);
                _bullets.Add(newBullet);
            }

            newBullet.gameObject.SetActive(true);
            newBullet.transform.position = FirePoint.position;
            newBullet.transform.rotation = transform.rotation;
            newBullet.MoveDirection = (FirePoint.position - transform.position).normalized;
        }
    }

    protected virtual void Start()
    {
        _mobileJoystick = FindObjectOfType<Joystick>();
        _healthView = FindObjectOfType<HealthView>();

        _healthView.CurrentHealth = CurrentHealth;
        _healthView.UpdateUI();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // Index 0 - base anim layer, index 1 - player, index 2 - enemy
        Anim.SetLayerWeight(1, 1f);
    }

    protected virtual void Update()
    {
        if (ReadInput)
        {
            MoveDirection = UseMobileInput
                ? ReadMobileInput()
                : new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

            if (!UseMobileInput && Input.GetButton("Fire1"))
                Fire();
        }
    }

    private Vector2 ReadMobileInput()
    {
        return _mobileJoystick != null ? _mobileJoystick.Direction : Vector2.zero;
    }
}
