using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ComboTestSCRIPT
{
    // A Test behaves as an ordinary method
    [Test]
    public void ComboTestSCRIPTSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ComboTestSCRIPTWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    private TestPlayerComboFinder playerScript;

    [SetUp]
    public void Setup()
    {
        GameObject gameObject = new GameObject();
        playerScript = gameObject.AddComponent<TestPlayerComboFinder>();
    }

    [Test]
    public void SingleDiceCombos()
    {
        Dictionary<int, int> diceValues = new Dictionary<int, int>(){
            {1,5},
            {2,1},
            {3,4},
            {4,2},
            {5,4},
            {6,2},
        };
        Dictionary<string, int> result = playerScript.FindAllCombos(diceValues);
        Assert.That(result, Is.EquivalentTo(new Dictionary<string, int> { { "5", 50 }, { "1", 100 }, { "15", 150 } }));
    }

    [Test]
    public void ThreeOfAKind()
    {
        Dictionary<int, int> diceValues = new Dictionary<int, int>(){
            {1,3},
            {2,2},
            {3,4},
            {4,6},
            {5,6},
            {6,6},
        };
        Dictionary<string, int> result = playerScript.FindAllCombos(diceValues);
        Assert.That(result, Is.EquivalentTo(new Dictionary<string, int> { { "666", 600 } }));
    }

    [Test]
    public void SequenceWithExtraDice()
    {
        Dictionary<int, int> diceValues = new Dictionary<int, int>(){
            {1,5},
            {2,2},
            {3,4},
            {4,6},
            {5,6},
            {6,6},
        };
        Dictionary<string, int> result = playerScript.FindAllCombos(diceValues);
        Assert.That(result, Is.EquivalentTo(new Dictionary<string, int> { { "5", 50 }, { "666", 600 }, { "5666", 650 } }));
    }

    [Test]
    public void SixOfAKind()
    {
        Dictionary<int, int> diceValues = new Dictionary<int, int>(){
            {1,1},
            {2,1},
            {3,1},
            {4,1},
            {5,1},
            {6,1},
        };
        Dictionary<string, int> result = playerScript.FindAllCombos(diceValues);
        Assert.That(result, Is.EquivalentTo(new Dictionary<string, int> { { "111111", 8000 } }));
    }

    [Test]
    public void FullStraight()
    {
        Dictionary<int, int> diceValues = new Dictionary<int, int>(){
            {1,5},
            {2,1},
            {3,2},
            {4,4},
            {5,3},
            {6,6},
        };
        Dictionary<string, int> result = playerScript.FindAllCombos(diceValues);
        Assert.That(result, Is.EquivalentTo(new Dictionary<string, int> { { "123456", 1500 } }));
    }

    [Test]
    public void StraightWithExtraDice()
    {
        Dictionary<int, int> diceValues = new Dictionary<int, int>(){
            {1,1},
            {2,2},
            {3,3},
            {4,4},
            {5,5},
            {6,1},
        };
        Dictionary<string, int> result = playerScript.FindAllCombos(diceValues);
        Assert.That(result, Is.EquivalentTo(new Dictionary<string, int> { { "112345", 600 } }));
    }

    [Test]
    public void MultipleOnesAndFives()
    {
        Dictionary<int, int> diceValues = new Dictionary<int, int>(){
            {1,1},
            {2,1},
            {3,1},
            {4,5},
            {5,5},
            {6,5},
        };
        Dictionary<string, int> result = playerScript.FindAllCombos(diceValues);
        Assert.That(result, Is.EquivalentTo(new Dictionary<string, int> { { "111555", 1500 } }));
    }

    [Test]
    public void FourOfAKindWithExtraOneAndFive()
    {
        Dictionary<int, int> diceValues = new Dictionary<int, int>(){
            {1,4},
            {2,1},
            {3,4},
            {4,5},
            {5,4},
            {6,4},
        };
        Dictionary<string, int> result = playerScript.FindAllCombos(diceValues);
        Assert.That(result, Is.EquivalentTo(new Dictionary<string, int> { { "144445", 950 } }));
    }

    [Test]
    public void FourOfAKindWithExtraFives()
    {
        Dictionary<int, int> diceValues = new Dictionary<int, int>(){
            {1,5},
            {2,4},
            {3,4},
            {4,5},
            {5,4},
            {6,4},
        };
        Dictionary<string, int> result = playerScript.FindAllCombos(diceValues);
        Assert.That(result, Is.EquivalentTo(new Dictionary<string, int> { { "444455", 900 } }));
    }
    
    [Test]
    public void FiveOfAKindWithExtraFive()
    {
        Dictionary<int, int> diceValues = new Dictionary<int, int>(){
            {1,4},
            {2,4},
            {3,4},
            {4,4},
            {5,4},
            {6,5},
        };
        Dictionary<string, int> result = playerScript.FindAllCombos(diceValues);
        Assert.That(result, Is.EquivalentTo(new Dictionary<string, int> { { "444445", 1650 } }));
    }
}
