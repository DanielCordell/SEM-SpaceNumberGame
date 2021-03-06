using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace Tests
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
        public IEnumerator Test_CanCreateAsteroidPrefab()
        {
            Assert.DoesNotThrow(() => GameObject.Instantiate(asteroidPrefab));
            yield return null;
        }

        [UnityTest]
        public IEnumerator Test_WhenTheAsteroidIsNotSelectedAndItIsClickedThenSelectedShouldBeTrue()
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
        public IEnumerator Test_WhenTheAsteroidIsSelectedAndItIsClickedThenSelectedShouldBeFalse()
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

        [UnityTest]
        public IEnumerator Test_WhenTheAsteroidIsSelectedTheCrosshairShouldBeVisible()
        {
            GameObject asteroidObject = GameObject.Instantiate(asteroidPrefab);
            yield return null;

            Asteroid asteroid = asteroidObject.GetComponent<Asteroid>();
            asteroid.Value = 2;
            asteroid.Selected = false;

            asteroid.OnMouseDown();
            var crosshair = asteroidObject.transform.Find("NoRotation/Crosshair").gameObject;
            Assert.AreEqual(crosshair.activeSelf, true);

            yield return null;
        }

        [UnityTest]
        public IEnumerator Test_WhenTheAsteroidIsDeselectedTheCrosshairShouldNotBeVisible()
        {
            GameObject asteroidObject = GameObject.Instantiate(asteroidPrefab);
            yield return null;

            Asteroid asteroid = asteroidObject.GetComponent<Asteroid>();
            asteroid.Value = 2;
            asteroid.Selected = true;

            asteroid.OnMouseDown();
            var crosshair = asteroidObject.transform.Find("NoRotation/Crosshair").gameObject;
            Assert.AreEqual(crosshair.activeSelf, false);

            yield return null;
        }
    }
}
