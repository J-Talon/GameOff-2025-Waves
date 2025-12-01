using UnityEngine;

public class EnergyPowerup : PowerupBehaviour
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
        player.GainEnergy(200);
    }
}