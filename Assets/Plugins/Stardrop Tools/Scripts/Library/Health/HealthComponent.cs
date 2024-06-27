
using StardropTools.Values;
using UnityEngine;

namespace StardropTools
{
    public class HealthComponent : BaseComponent, IAlive
    {
        [SerializeField] int maxHealth = 5;
        [SerializeField] int health = 5;

        [NaughtyAttributes.ShowNativeProperty]
        public int Health => health;

        [NaughtyAttributes.ShowNativeProperty]
        public float HealthPercentage => (maxHealth > 0) ? (float)health / maxHealth : 0f;

        public int MaxHealth => maxHealth;

        public bool IsAlive => health > 0;

        [NaughtyAttributes.ShowNativeProperty]
        public bool IsDead => health <= 0;

        public EventCallback<int> OnHealthChanged { get; private set; }
        public EventCallback<float> OnHealthPercentChanged { get; private set; }

        public EventCallback OnDeath { get; private set; }
        public EventCallback OnRevived { get; private set; }

        public override void Initialize()
        {
            base.Initialize();

            OnHealthChanged = new EventCallback<int>();
            OnHealthPercentChanged = new EventCallback<float>();

            OnDeath = new EventCallback();
            OnRevived = new EventCallback();

            Revive();
        }

        public void SetHealthValue(int health)
        {
            if (this.health == health)
                return;

            this.health = health;
            HealthChanged();
        }

        public void SetMaxHealth(int maxHealth)
        {
            if (this.maxHealth == maxHealth)
                return;

            this.maxHealth = maxHealth;
            HealthChanged();
        }

        public void SetHealthAndMaxHealth(int targetHealth)
        {
            Initialize();

            this.health = targetHealth;
            this.maxHealth = targetHealth;

            HealthChanged();
        }

        public int ApplyDamage(int damageAmount)
        {
            if (IsDead)
                return 0;

            int nextHealth = Health - damageAmount;
            if (nextHealth > 0)
            {
                health -= Mathf.Max(0, damageAmount);
                HealthChanged();
            }
            else
                Kill();

            return health;
        }

        public int ApplyDamageCurrentHealthPercent(float percentOfCurrentHealth)
        {
            int damageAmount = Mathf.FloorToInt(percentOfCurrentHealth * health);
            return ApplyDamage(damageAmount);
        }

        public int ApplyDamageMaxHealthPercent(float percentOfMaxHealth)
        {
            int damageAmount = Mathf.FloorToInt(percentOfMaxHealth * maxHealth);
            return ApplyDamage(damageAmount);
        }

        public int ApplyHeal(int healAmount)
        {
            if (IsDead)
                return 0;

            health += Mathf.Clamp(healAmount, 0, maxHealth);
            HealthChanged();

            return health;
        }

        public int ApplyHealCurrentHealthPercent(float percentOfCurrentHealth)
        {
            int healAmount = Mathf.FloorToInt(percentOfCurrentHealth * health);
            return ApplyHeal(healAmount);
        }

        public int ApplyHealMaxHealthPercent(float percentOfMaxHealth)
        {
            int healAmount = Mathf.FloorToInt(percentOfMaxHealth * maxHealth);
            return ApplyHeal(healAmount);
        }

        [NaughtyAttributes.Button("Kill")]
        public void Kill()
        {
            if (IsDead)
                return;

            health = 0;

            HealthChanged();
            OnDeath?.Invoke();
        }

        [NaughtyAttributes.Button("Revive")]
        public void Revive()
        {
            if (IsAlive)
                return;

            health = maxHealth;

            HealthChanged();
            OnRevived?.Invoke();
        }

        public void Revive(int reviveHealth)
        {
            if (IsAlive)
                return;

            health = reviveHealth;

            HealthChanged();
            OnRevived?.Invoke();
        }

        public void FullRevive()
        {
            health = maxHealth;

            HealthChanged();
            OnRevived?.Invoke();
        }

        private void HealthChanged()
        {
            OnHealthChanged?.Invoke(health);
            OnHealthPercentChanged?.Invoke(HealthPercentage);
        }
    }
}
