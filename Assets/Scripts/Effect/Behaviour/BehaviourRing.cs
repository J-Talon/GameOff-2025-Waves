using UnityEngine;

namespace Effect.Behaviour
{
    public class BehaviourRing: ParticleBehaviour
    {

        public override void TickContext(params float[] context)
        {
            ParticleSystem.ShapeModule sm = particleSystem.shape;
            if (context == null || context.Length == 0) return;
            
            sm.radius = context[0];
        }

        public override bool IsFinished()
        {
            return false;
        }

    }
}