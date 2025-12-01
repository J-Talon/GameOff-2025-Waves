using Effect;
using UnityEngine;

namespace Item
{
    public class ItemPulse : GameItem, AttackVector
    {
        //  [SerializeField] private float attackPeriod = 1000;  //in millis
        // [SerializeField] private float expansionSpeed = 500; // time it takes to reach max range
        // [SerializeField] private float maxRange = 5; //units
        // [SerializeField] private float baseDamage = 1f;

        private float lastAttackTime;
        private CircleCollider2D coll;
        private bool inAnimation;
        private float range;

        private ParticleBehaviour behaviour = null;

        public override void Init()
        {
            lastAttackTime = 0;
            coll = gameObject.GetComponent<CircleCollider2D>();
            coll.isTrigger = true;
            inAnimation = false;
            coll.radius = 0;
        }

        private void Animate(float time)
        {
            float timePercentage = time / data.attackPeriod;
            if (timePercentage > 1)
            {
                inAnimation = false;
                range = 0;
                coll.radius = range;

                if (behaviour != null)
                    behaviour.Complete();
                return;
            }

            float scale = data.maxRange * timePercentage;
            range = scale;
            coll.radius = range;

            if (behaviour != null)
            {
                behaviour.UpdatePosition(transform.position);
                behaviour.TickContext(scale, range);
            }
        }

        public void Attack()
        {
            if (inAnimation)
                return;

            inAnimation = true;
            lastAttackTime = base.GetTimeMillis();
            behaviour = ParticleFactory.PlayParticle("ParticleRing", transform.position);

        }

        public override void ItemTick()
        {
            float time = base.GetTimeMillis();
            float attackAnimationTime = time - lastAttackTime;

            if (inAnimation)
            {
                Animate(attackAnimationTime);
                return;
            }
            if (attackAnimationTime < data.attackPeriod)
                return;

            if (data.damageMultiplier > 1 & playerData.currentEnergy > 10)
            {
                DecreaseEnergy();
                Attack();
            }

        }


        public void OnTriggerEnter2D(Collider2D other)
        {
            GameObject hit = other.gameObject;
            Enemy entity = hit.GetComponent<Enemy>();
            if (entity == null)
                return;

            //Debug.Log("damage: "+other.name);

            //you'd probably do the damage calculations here, or in the entity depending on what's easier
            entity.takeDamage(data.baseDamage, 1);
        }

        public void DecreaseEnergy()
        {
            EqualizerManager.RaiseEnergyChange(playerData, data.currentEnergyCost);
        }
        public void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(gameObject.transform.position, data.maxRange);
        }

    }
}