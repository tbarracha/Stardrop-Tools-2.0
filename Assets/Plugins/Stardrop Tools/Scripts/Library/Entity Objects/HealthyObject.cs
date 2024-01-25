
using UnityEngine;
using StardropTools;

/// <summary>
/// Base class for objects with Health (Damageable Objects)
/// </summary>
[RequireComponent(typeof(HealthComponent))]
public class HealthyObject : BaseTransform, IDamageable
{
    [Header("Health")]
    [SerializeField] protected HealthComponent healthComponent;

    Timer invulerabilityTimer;
    bool canTakeDamage;

    public int Health => healthComponent.Health;
    public bool IsDead => healthComponent.IsDead;

    public CustomEvent OnDamaged => healthComponent.OnDamaged;
    public CustomEvent OnHealed => healthComponent.OnHealed;

    public CustomEvent OnRevived => healthComponent.OnRevived;
    public CustomEvent OnDeath => healthComponent.OnDeath;

    public CustomEvent<int> OnHealthChanged => healthComponent.OnHealthChanged;


    public override void Initialize()
    {
        base.Initialize();

        canTakeDamage = true;
        OnDeath.AddListener(Death);
    }

    public void SetCanTakeDamage(bool value) => canTakeDamage = value;

    public virtual int ApplyDamage(int damageAmount) => healthComponent.ApplyDamage(damageAmount);

    public virtual int ApplyDamage(int damageAmount, float invulnerabilityTime)
    {
        if (canTakeDamage == false)
            return Health;

        invulerabilityTimer = new Timer(invulnerabilityTime).Play();
        invulerabilityTimer.OnTimerComplete.AddListener(() => canTakeDamage = true);

        healthComponent.ApplyDamage(damageAmount);
        canTakeDamage = false;

        return Health;
    }

    public virtual int ApplyHeal(int healAmount) => healthComponent.ApplyHeal(healAmount);

    public void Kill() => healthComponent.Kill();

    public void Revive() => healthComponent.Revive();

    protected virtual void Death() { }

    protected override void OnValidate()
    {
        base.OnValidate();

        if (healthComponent == null)
            healthComponent = GetComponent<HealthComponent>();
    }
}