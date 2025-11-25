using UnityEngine;

public interface IBehaviour
{
    void Register(Enemy enemy);
    void DeRegister(Enemy enemy);
    void tick(Vector3 playerPosition);
}
