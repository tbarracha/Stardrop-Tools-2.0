
namespace StardropTools
{
    public interface IHealth
    {
        public int ApplyDamage(int damage);
        public int ApplyHeal(int heal);

        void Death();
        void Revive();
    }
}