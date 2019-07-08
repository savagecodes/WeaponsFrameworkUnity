namespace SavageCodes.Frameworks.Weapons
{
    public interface IDamageable
    {
        void TakeDamage(float damage, IWeaponCarrier instigator);
    }
}
