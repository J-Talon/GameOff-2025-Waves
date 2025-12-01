using UnityEngine;

public interface PowerupBehaviour
{
    void Register(GameEntity gameEntity);
    void DeRegister(GameEntity gameEntity);
    void tick(Player player);
}
