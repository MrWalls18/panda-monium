using Photon.Pun;
public interface IDamageable
{
    void TakeDamage(int damage, Photon.Realtime.Player shooter);
}