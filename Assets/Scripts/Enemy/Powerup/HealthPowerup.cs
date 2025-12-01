using UnityEngine;

public class HealthPowerup : PowerupBehaviour
{
    private GameEntity powerup;
    public void Register(GameEntity powerup)
    {
        this.powerup = powerup;
    }

    public void DeRegister(GameEntity powerup)
    {
        this.powerup = null;
    }
    public void tick(Player player)
    {
        player.GainHealth(20);
    }
}