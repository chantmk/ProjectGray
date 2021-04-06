namespace Players.IWeapon
{
    public interface IWeapon
    {
        int Weaponname { get; }
        float MaxFireCooldown { get;  }
        
        void Fire();
    }
}