
namespace StardropTools
{
    public interface IDamageable
    {
        public int ApplyDamage(int damageAmount);
        public void Kill();
    }
}