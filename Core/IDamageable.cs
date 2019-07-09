namespace SavageCodes.Frameworks.Weapons
{
    public interface IDamageable
    {
        float HP { get; }
        float MaxHP { get; }
        void TakeDamage(float damage, IWeaponCarrier instigator);
    }
}
