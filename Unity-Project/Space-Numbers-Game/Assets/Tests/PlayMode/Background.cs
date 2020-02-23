using System.Collections;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Background
    {
        GameObject starFieldPrefab;
        [UnitySetUp]
        public void Setup()
        {
        }

        [UnityTest]
        public IEnumerator CanCreatePrefab()
        {
            SceneManager.LoadScene("SpaceScene");
            GameObject starFieldPrefab = Resources.Load<GameObject>("Level/Prefabs/Starfield");
            Assert.DoesNotThrow(() => GameObject.Instantiate(starFieldPrefab));
            yield return null;
        }
    }
}
