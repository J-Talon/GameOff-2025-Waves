using UnityEngine;

public interface IBehaviour
{
    void Register(GameEntity gameEntity);
    void DeRegister(GameEntity gameEntity);
    void tick(Vector3 playerPosition = default);
}
