public interface ICanShoot
{
    // How long the player has to wait before he can shoot this weapon again
    float Cooldown { get; set; }
    bool Available { get; set; }
    void Shoot();
    void Enable();
    void Disable();
    void GetAmmoState(out int currentAmmo, out int maxAmmo);
}