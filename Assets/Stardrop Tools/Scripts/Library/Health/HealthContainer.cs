
using UnityEngine;

namespace StardropTools
{
    public class HealthContainer : MonoBehaviour
    {
        [SerializeField] int startHealth;
        [SerializeField] int maxHealth;
        [SerializeField] int health;
        [SerializeField] float percent;
        [SerializeField] bool isDead;

        public int Health => health;
        public float PercentHealth => percent;
        public bool IsDead => isDead;
        

        public BaseEvent<int> OnDamaged = new BaseEvent<int>();
        public BaseEvent<int> OnHealed = new BaseEvent<int>();

        public BaseEvent<int> OnHealthChanged = new BaseEvent<int>();
        public BaseEvent OnDeath = new BaseEvent();


        #region Constructors

        public HealthContainer(int startHealth, int maxHealth)
        {
            this.startHealth = startHealth;
            this.maxHealth = maxHealth;
            health = startHealth;
        }

        public HealthContainer(int startHealth, int maxHealth, int health)
        {
            this.startHealth = startHealth;
            this.maxHealth = maxHealth;
            this.health = health;
        }

        #endregion // Constructos


        public int ApplyDamage(int damageAmount)
        {
            if (isDead)
                return 0;

            health = Mathf.Clamp(health - damageAmount, 0, maxHealth);

            if (health == 0 && isDead == false)
            {
                isDead = true;
                OnDeath?.Invoke();
            }

            OnDamaged?.Invoke(damageAmount);
            OnHealthChanged?.Invoke(health);
            return health;
        }

        /// <summary>
        /// Value from 0 to 1
        /// </summary>
        public int ApplyDamagePercent(float percent, bool fromMaxHealth)
        {
            if (isDead)
                return 0;

            int damage = fromMaxHealth ? Mathf.CeilToInt(percent * maxHealth) : Mathf.CeilToInt(percent * health);
            return ApplyDamage(damage);
        }



        public int ApplyHeal(int healAmount)
        {
            if (isDead)
                return 0;

            health = Mathf.Clamp(health + healAmount, 0, maxHealth);

            if (health > 0 && isDead == true)
                isDead = false;

            OnHealed?.Invoke(healAmount);
            OnHealthChanged?.Invoke(health);
            return health;
        }

        public int ApplyHealPercent(float percent, bool fromMaxHealth)
        {
            if (isDead)
                return 0;

            int heal = fromMaxHealth ? Mathf.CeilToInt(percent * maxHealth) : Mathf.CeilToInt(percent * health);
            return ApplyHeal(heal);
        }



        public void Revive()
        {
            isDead = false;
            health = maxHealth;

            OnHealthChanged?.Invoke(health);
        }

        public void Revive(int reviveHealth)
        {
            isDead = false;
            health = reviveHealth;

            OnHealthChanged?.Invoke(health);
        }

        public void Revive(float percentMaxHealth)
        {
            isDead = false;
            health = Mathf.CeilToInt(percentMaxHealth * maxHealth);

            OnHealthChanged?.Invoke(health);
        }
    }
}