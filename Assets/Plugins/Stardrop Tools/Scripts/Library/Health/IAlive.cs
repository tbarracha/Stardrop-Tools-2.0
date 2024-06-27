
namespace StardropTools
{
    /// <summary>
    /// Interface that implements methods related to the HealthContainer class
    /// </summary>
    public interface IAlive : IHealeable, IDamageable
    {
        public int Health { get; }
    }
}