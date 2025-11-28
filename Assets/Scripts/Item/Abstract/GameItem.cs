using UnityEngine;

namespace Item
{
    public abstract class GameItem : MonoBehaviour
    {
        protected float frequency = 0;
        protected float amplitude = 0;
        public WeaponData data;
        public abstract void ItemTick();
        public abstract void Init();

        public virtual float GetTimeMillis()
        {
            return Time.fixedTime * 1000f;
        }
    }
}