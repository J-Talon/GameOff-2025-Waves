using System;
using System.Collections.Generic;
using UnityEngine;

namespace Effect
{
    public static class ParticleFactory
    {
        private static Dictionary<string, GameObject> particles = new Dictionary<string, GameObject>();
        private static GameObject sequencer;
        
        //todo add prefab


        public static void LoadResources()
        {
            GameObject[] particleSystems = Resources.LoadAll<GameObject>("Prefab/Effect/Raw");
            foreach (GameObject o in particleSystems)
                particles.Add(o.name, o);
        }
        
        


        //play particle for the alloted time (if looping) or if nonlooping until end of duration
        public static void PlayParticle(string particleName)
        {
            
            // 2 ways to do this:
            // either respect the infinite particle playing
            // or set a duration based on particle lifetime
        }



        //seq is an array of strings you wish to have sequence in
        public static ParticleSequencer ConstructSequence(params string[] seq)
        {
            GameObject prefab = GameObject.Instantiate(sequencer);
            ParticleSequencer sequence = prefab.GetComponent<ParticleSequencer>();
            
            List<ParticleBehaviour> behaviours = new  List<ParticleBehaviour>();
            foreach (string s in seq)
            {
                particles.TryGetValue(s, out GameObject o);
                if (o == null)
                    throw new NullReferenceException("Could not load particle: "+s);

                ParticleBehaviour behaviour = o.GetComponent<ParticleBehaviour>();
                if (behaviour == null)
                    throw new NullReferenceException("Particle "+s+" has no particle behaviour script component");
                GameObject instance = GameObject.Instantiate(o);
                ParticleBehaviour instanceBehaviour = instance.GetComponent<ParticleBehaviour>();
                
                behaviours.Add(instanceBehaviour);
            }

            sequence.ConstructSequence(behaviours.ToArray());
            return sequence;
        }



    }
}