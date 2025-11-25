using UnityEngine;

namespace Effect.Behaviour
{
    public class BehaviourRing: ParticleBehaviour
    {

        public override void TickContext(params float[] context)
        {
            ParticleSystem.MainModule main = particleSystem.main;
            if (context == null || context.Length < 2) return;

            ParticleSystem.MinMaxCurve curve = main.startSpeed;
            curve.constant = context[0];
            
            ParticleSystem.ShapeModule shape = particleSystem.shape;
            shape.radius = context[1];
        }

        public override bool IsFinished()
        {
            return false;
        }

    }
}