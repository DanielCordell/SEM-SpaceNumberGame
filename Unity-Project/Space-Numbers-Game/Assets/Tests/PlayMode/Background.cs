using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using System.Collections.Generic;

namespace Tests
{
    public class Background
    {
        GameObject starFieldPrefab;
        [OneTimeSetUp]
        public void Setup()
        {
            starFieldPrefab = Resources.Load<GameObject>("Level/Prefabs/Starfield");
            SceneManager.LoadScene("SpaceScene");
        }

        [UnityTest]
        public IEnumerator CanCreatePrefab()
        {
            Assert.DoesNotThrow(() => GameObject.Instantiate(starFieldPrefab));
            yield return null;
        }

        [UnityTest]
        public IEnumerator StarsAreCreated()
        {
            GameObject starfieldObject = GameObject.Instantiate(starFieldPrefab);
            ParticleSystem ps = starfieldObject.GetComponent<ParticleSystem>();
            Starfield starfield = starfieldObject.GetComponent<Starfield>();
            Assert.AreEqual(starfield.starCount, ps.particleCount);
            yield return null;
        }

        [UnityTest]
        public IEnumerator StarsAreMoving()
        {
            GameObject starfieldObject = GameObject.Instantiate(starFieldPrefab);
            ParticleSystem ps = starfieldObject.GetComponent<ParticleSystem>();
            Starfield starfield = starfieldObject.GetComponent<Starfield>();
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[starfield.starCount];
            ps.GetParticles(particles);
            List<Vector3> pos = particles.Select(it => it.position * 1).ToList();
            yield return new WaitForSecondsRealtime(0.2f);
            ps.GetParticles(particles);
            List<Vector3> pos2 = particles.Select(it => it.position * 1).ToList();
            Assert.True(pos.Select((p, i) => new { p, i }).All(it => it.p.magnitude != pos2[it.i].magnitude));
        }
    }
}
