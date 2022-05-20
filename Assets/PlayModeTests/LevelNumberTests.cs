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

    int[] levels3Order = { 0, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3 };
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

        Assert.AreEqual(levels3Order[0], dm.GetCurrentLevelNumber());

        for(int i = 1; i < levels3Order.Length; i++)
        {
            Assert.AreEqual(levels3Order[i], dm.GetNextLevelNumberAndUpdateLevel());
        }
    }

    int[] levels5Order = { 0, 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 1, 2, 3, 4, 5 };
    [UnityTest]
    public IEnumerator LevelLoopingWith6TotalLevels()
    {
        yield return null;

        GameObject gameObject = new GameObject();

        gameObject.AddComponent<DataManager>();

        DataManager dm = gameObject.GetComponent<DataManager>();
        dm.ResetData();
        dm.SetTotalNumberOfLevels(6);

        Assert.AreEqual(levels3Order[0], dm.GetCurrentLevelNumber());

        for (int i = 1; i < levels5Order.Length; i++)
        {
            Assert.AreEqual(levels5Order[i], dm.GetNextLevelNumberAndUpdateLevel());
        }
    }

    int[] levels4TotalOrder = { 0, 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3 };
    [UnityTest]
    public IEnumerator GetCurrentLevel4TotalLevels()
    {
        yield return null;

        GameObject gameObject = new GameObject();

        gameObject.AddComponent<DataManager>();

        DataManager dm = gameObject.GetComponent<DataManager>();
        dm.ResetData();
        dm.SetTotalNumberOfLevels(4);

        for(int i = 0; i < levels4TotalOrder.Length; i++)
        {
            Assert.AreEqual(levels4TotalOrder[i], dm.GetCurrentLevelNumber());
            dm.UpdateLevelByOne();
        }
    }
}
