using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LevelNumberTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void LevelNumberTestsSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    int[] levels3Order = { 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3 };
    // Test checks level order and level looping behavior
    [UnityTest]
    public IEnumerator LevelLoopingWith4TotalLevels()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        GameObject gameObject = new GameObject();

        gameObject.AddComponent<DataManager>();

        DataManager dm = gameObject.GetComponent<DataManager>();
        dm.ResetData();
        dm.SetTotalNumberOfLevels(4);

        for(int i = 0; i < levels3Order.Length; i++)
        {
            Assert.AreEqual(levels3Order[i], dm.GetNextLevelNumberAndUpdateLevel());
        }
    }

    int[] levels5Order = { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 1, 2, 3, 4, 5 };
    [UnityTest]
    public IEnumerator LevelLoopingWith6TotalLevels()
    {
        yield return null;

        GameObject gameObject = new GameObject();

        gameObject.AddComponent<DataManager>();

        DataManager dm = gameObject.GetComponent<DataManager>();
        dm.ResetData();
        dm.SetTotalNumberOfLevels(6);

        for (int i = 0; i < levels5Order.Length; i++)
        {
            Assert.AreEqual(levels5Order[i], dm.GetNextLevelNumberAndUpdateLevel());
        }
    }
}
