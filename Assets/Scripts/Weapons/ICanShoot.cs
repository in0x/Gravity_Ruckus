public interface ICanShoot
{
    // How long the player has to wait before he can shoot this weapon again
    float Cooldown { get; set; }
    void Shoot();
    void Enable();
    void Disable();
}