
namespace StardropTools
{
    public interface IHealeable
    {
        public int ApplyHeal(int healAmount);
        public void Revive();
        public void FullRevive();
    }
}