using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Tests123
    {
        // A Test behaves as an ordinary method
        [Test]
        public void NewTestScriptSimplePasses()
        {
            Assert.True(true);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame (e.g. wait for processing).
        [UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses()
        {
            Assert.True(true);
            yield return null;
        }
    }
}
