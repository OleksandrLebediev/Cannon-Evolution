public interface ICannonShootingControl
{
    public void StartShooting(bool multiple = true, int numberShots = 0);
    public void StopShooting();
}

