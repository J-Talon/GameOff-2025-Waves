using System;
using System.Collections.Generic;
using UnityEngine;

namespace Effect
{
    public static class ParticleFactory
    {
        private static Dictionary<string, GameObject> particles = new Dictionary<string, GameObject>();
        private static GameObject sequencer;


        public static void LoadResources()
        {
            GameObject[] particleSystems = Resources.LoadAll<GameObject>("Prefab/Effect/Raw");
            foreach (GameObject o in particleSystems)
            {
                if (o.GetComponent<ParticleBehaviour>() != null)
                {
                    Debug.Log("Particle factory: Adding "+o.name);
                    particles.Add(o.name, o);
                }
                else
                    Debug.LogError("Particle factory: ParticleBehaviour not found for particle "+o.name);
                
            }
            sequencer = Resources.Load<GameObject>("Prefab/Effect/Sequencer");
        }
        


        //play particle for the alloted time (if looping) or if nonlooping until end of duration
        public static ParticleBehaviour PlayParticle(string particleName, Vector2 position)
        {
            GameObject system = particles.TryGetValue(particleName, out GameObject go) ? go : null;
            if (system == null)
                throw new NullReferenceException($"Particle {particleName} not found");
            
            GameObject instance = GameObject.Instantiate(system, position, Quaternion.identity);
            ParticleBehaviour behaviour = instance.GetComponent<ParticleBehaviour>();
            behaviour.Play();
            return behaviour;
        }



        //seq is an array of strings you wish to have sequence in
        public static ParticleSequencer ConstructSequence(Vector2 position, params string[] seq)
        {
            GameObject prefab = GameObject.Instantiate(sequencer,position,Quaternion.identity);
            ParticleSequencer sequence = prefab.GetComponent<ParticleSequencer>();
            
            List<ParticleBehaviour> behaviours = new  List<ParticleBehaviour>();
            foreach (string s in seq)
            {
                particles.TryGetValue(s, out GameObject o);
                if (o == null)
                    throw new NullReferenceException("Could not load particle: "+s);

                
                GameObject instance = GameObject.Instantiate(o,position,Quaternion.identity);
                ParticleBehaviour instanceBehaviour = instance.GetComponent<ParticleBehaviour>();
                
                if (instanceBehaviour == null)
                    throw new NullReferenceException("Particle "+s+" has no particle behaviour script component");
                
                behaviours.Add(instanceBehaviour);
            }

            sequence.ConstructSequence(behaviours.ToArray());
            return sequence;
        }



    }
}