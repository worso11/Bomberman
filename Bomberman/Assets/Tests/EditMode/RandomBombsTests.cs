using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

namespace Tests
{
    public class RandomBombsTests
    {

        GameObject bombPrefab = Resources.Load<GameObject>("Assets/Prefabs/Bomb.prefab");
        GameObject collectiblePrefab;
        GameObject destructiblePrefab;
        GameObject explosionPrefab;
        GameObject playerPrefab;


        [Test]
        public void BasicTest()
        {

            bool isActive = false;

            Assert.AreEqual(false, isActive);

        }

        // A Test behaves as an ordinary method
        [Test]
        public void NewTestScriptSimplePasses()
        {
            //playerPrefab = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
        }

        [Test]
        public void BombPower()
        {
            int power = 3;
            GameObject bomb = MonoBehaviour.Instantiate(bombPrefab);
            Bomb classBomb = bomb.GetComponent<Bomb>();
            classBomb.Boom(4);
            Assert.AreEqual(power, classBomb.GetPower());
        }
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
