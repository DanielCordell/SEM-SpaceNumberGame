using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace AsteroidTests
{
    public class AsteroidTests
    {
        GameObject asteroidPrefab;

        [OneTimeSetUp]
        public void Setup()
        {
            asteroidPrefab = Resources.Load<GameObject>("Level/Prefabs/Asteroid");
            SceneManager.LoadScene("SpaceScene");
        }

        [UnityTest]
        public IEnumerator CanCreatePrefab()
        {
            Assert.DoesNotThrow(() => GameObject.Instantiate(asteroidPrefab));
            yield return null;
        }

        [UnityTest]
        public IEnumerator WhenTheAsteroidIsNotSelectedAndItIsClickedThenSelectedShouldBeTrue()
        {
            GameObject asteroidObject = GameObject.Instantiate(asteroidPrefab);
            yield return null;

            Asteroid asteroid = asteroidObject.GetComponent<Asteroid>();
            asteroid.Value = 2;
            asteroid.Selected = false;

            asteroid.OnMouseDown();
            Assert.AreEqual(asteroid.Selected, true);
            
            yield return null;
        }

        [UnityTest]
        public IEnumerator WhenTheAsteroidIsSelectedAndItIsClickedThenSelectedShouldBeFalse()
        {
            GameObject asteroidObject = GameObject.Instantiate(asteroidPrefab);
            yield return null;

            Asteroid asteroid = asteroidObject.GetComponent<Asteroid>();
            asteroid.Value = 2;
            asteroid.Selected = true;

            asteroid.OnMouseDown();
            Assert.AreEqual(asteroid.Selected, false);

            yield return null;
        }
    }
}
