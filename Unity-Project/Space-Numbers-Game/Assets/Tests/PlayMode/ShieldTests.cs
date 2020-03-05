using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using System.Collections.Generic;
using System;

namespace Tests
{
    public class ShieldTests
    {
        ShieldStateHandler shieldStateHandler;
        [UnitySetUp]
        public IEnumerator UnitySetup()
        {
            SceneManager.LoadScene("SpaceScene");
            yield return null;
            shieldStateHandler = GameObject.FindObjectOfType<ShieldStateHandler>();
        }

        [Test]
        public void Test_ShieldStateHandlerExists()
        {
            Assert.IsNotNull(shieldStateHandler);
        }

        [Test]
        public void Test_ShieldIsSetupCorrectly()
        {
            TestInitialised();
        }

        [Test]
        public void Test_AddCountWrongIncrementsCorrectly()
        {
            int before = shieldStateHandler.CountWrong;
            shieldStateHandler.AddCountWrong();
            int afterGetValue = shieldStateHandler.CountWrong;
            Assert.AreEqual(before + 1, afterGetValue);
        }

        [UnityTest]
        public IEnumerator Test_LoadsCorrectShieldAfterOneWrong()
        {
            shieldStateHandler.AddCountWrong();
            yield return null;
            Assert.AreEqual(shieldStateHandler.GetHealthTextureName(), "healthBar1");
            Assert.AreEqual(shieldStateHandler.GetShieldTextureName(), "shields1");
            yield return null;
        }

        [UnityTest]
        public IEnumerator Test_LoadsCorrectShieldAfterTwoWrong()
        {
            shieldStateHandler.AddCountWrong();
            shieldStateHandler.AddCountWrong();
            yield return null;
            Assert.AreEqual(shieldStateHandler.GetHealthTextureName(), "healthBar2");
            Assert.AreEqual(shieldStateHandler.GetShieldTextureName(), "shields2");
            yield return null;
        }

        [UnityTest]
        public IEnumerator Test_LoadsCorrectShieldAfterThreeWrong()
        {
            shieldStateHandler.AddCountWrong();
            shieldStateHandler.AddCountWrong();
            shieldStateHandler.AddCountWrong();
            yield return null;
            Assert.IsNull(shieldStateHandler.GetHealthTextureName());
            Assert.AreEqual(shieldStateHandler.GetShieldTextureName(), "shields3");
            yield return null;
        }

        [UnityTest]
        public IEnumerator Test_InitialiseShieldStateResetsCorrectly()
        {
            shieldStateHandler.AddCountWrong();
            shieldStateHandler.AddCountWrong();
            shieldStateHandler.AddCountWrong();
            yield return null;
            shieldStateHandler.InitialiseShieldState();
            yield return null;
            TestInitialised();
            yield return null;
        }

        public void TestInitialised()
        {
            Assert.AreEqual(shieldStateHandler.CountWrong, 0);
            Assert.IsNotNull(shieldStateHandler.Shields);
            Assert.IsNotNull(shieldStateHandler.HealthBars);

            Assert.AreEqual(shieldStateHandler.GetHealthTextureName(), "healthBarFull");
            Assert.AreEqual(shieldStateHandler.GetShieldTextureName(), "shieldsFull");
        }
    }
}
