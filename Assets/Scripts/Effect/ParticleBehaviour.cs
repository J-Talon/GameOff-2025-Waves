using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Effect
{
    public class ParticleBehaviour: MonoBehaviour
    {
        protected ParticleSystem particleSystem;
        [CanBeNull] protected ParticleSequencer sequencer = null;
        private ParticleSystem.MainModule main;
        protected bool complete = false;


        private void Init()
        {
            particleSystem = GetComponent<ParticleSystem>();
            main = particleSystem.main;
            main.playOnAwake = false;
        }

        public void Start()
        {
            if (particleSystem == null)
                Init();
        }

        public void Play()
        {
            if (particleSystem == null)
                Init();
            particleSystem.Play();
            StartCoroutine(WaitForCompletion());
        }

        private IEnumerator WaitForCompletion()
        {
            while (particleSystem.isPlaying && !complete)
            {
                yield return new WaitForSeconds(particleSystem.main.duration);
            }

            if (sequencer != null)
                sequencer.Step();
            

            while (particleSystem.particleCount > 0)
            {
                yield return new WaitForSeconds(particleSystem.main.startLifetime.constantMax);
            }
            
            Finalize();
        }


        public void Complete()
        {
            particleSystem.Stop(true,  ParticleSystemStopBehavior.StopEmitting);
            complete = true;
        }
        

        public void Finalize()
        {
            GameObject.Destroy(gameObject);
        }


        public virtual void TickContext([CanBeNull] params float[] context)
        {
            
        }

        public virtual void UpdatePosition(Vector2 position)
        {
            gameObject.transform.position = position;
        }


        public virtual bool IsFinished()
        {
            return complete;
        }


    }
}