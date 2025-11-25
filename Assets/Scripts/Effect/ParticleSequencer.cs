using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Effect
{
    public class ParticleSequencer: MonoBehaviour
    {

        [SerializeField] private ParticleBehaviour[] queue;
        [SerializeField] private float period = 0.1f;
        private float[] currentContext = null;
        private ParticleBehaviour current = null;
        private int index = 0;
        
        

        public void ConstructSequence([CanBeNull] params ParticleBehaviour[] particles) {
            this.queue = particles;
        }

        public void StartSequence()
        {
            if (queue == null || queue.Length == 0)
            {
                Complete();
                return;
            }
            current = queue[index];
            current.UpdatePosition(transform.position);
            current.Start();
        }

        public void SetPosition(Vector2 newPos)
        {
            transform.position = newPos;
            if (current != null)
                current.UpdatePosition(newPos);
        }
        

        public bool Step()
        {
            if (current != null)
                current.Complete();

            index++;
            if (index >= queue.Length)
                return false;
            
            current = queue[index];
            current.UpdatePosition(transform.position);
            current.Start();
            return true;
        }


        public void ReceiveContext([CanBeNull] params float[] context)
        {
            if (current!= null)
                current.TickContext(currentContext);
            currentContext = context;
        }
        
        
        public void Complete()
        {
            foreach (ParticleBehaviour particle in queue)
            {
                if (particle == null) continue;
                particle.Finalize();
            }
            
            Destroy(gameObject);
        }

    }
}