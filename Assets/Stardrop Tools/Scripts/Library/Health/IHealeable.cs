
namespace StardropTools
{
    public interface IHealeable
    {
        public int ApplyHeal(int heal);
        void Death();
    }
}