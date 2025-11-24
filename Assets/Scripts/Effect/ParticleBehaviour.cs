using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Effect
{
    public abstract class ParticleBehaviour: MonoBehaviour
    {
        [SerializeField] protected ParticleSystem particleSystem;
        private ParticleSystem.MainModule main;
        protected bool complete = false;

        public void Start()
        {
            particleSystem = GetComponent<ParticleSystem>();
            main = particleSystem.main;
            main.playOnAwake = false;
        }

        public void Play()
        {
            particleSystem.Play();
        }


        public void Complete()
        {
            particleSystem.Stop(true,  ParticleSystemStopBehavior.StopEmitting);
            complete = true;
            
                // you might actually want this in the play method
                //so that you can control when the thing completes
            StartCoroutine(WaitForCompletion());
        }

        public IEnumerator WaitForCompletion()
        {
            while (particleSystem.particleCount > 0)
            {
                yield return new WaitForSeconds(particleSystem.main.duration);
            }

            Finalize();
            yield break;
        }

        public void Finalize()
        {
            GameObject.Destroy(gameObject);
        }


        public abstract void TickContext([CanBeNull] params float[] context);

        public virtual void UpdatePosition(Vector2 position)
        {
            gameObject.transform.position = position;
        }
        

        public abstract bool IsFinished();


    }
}