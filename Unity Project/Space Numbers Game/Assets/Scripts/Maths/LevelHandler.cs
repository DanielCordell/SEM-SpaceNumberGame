using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    Level currentLevel;
    public const int MAX_LEVEL = 15;

    public GameObject asteroid;
    public int extraAsteroids;

    // Start is called before the first frame update
    void Start()
    {
        // Current demo code to generate a level
        currentLevel = GenerateLevel(1);
        SetupLevel(ref currentLevel);
    }

    void SetupLevel(ref Level level)
    {
        if (level == null) throw new ArgumentNullException("Level is Null!");
        int numberOfRealAsteroids = level.GetNumberOfGaps();
        int numberOfAsteroids = numberOfRealAsteroids + extraAsteroids;
        Debug.Log("Generating " + numberOfAsteroids + " asteroids.\nReal: " + numberOfRealAsteroids + " Fake: " + extraAsteroids);

        GameObject[] positionObjects = GameObject.FindGameObjectsWithTag("AsteroidSpawnPos");

        if (positionObjects == null)
        {
            throw new NullReferenceException("No objects tagged with AsteroidSpawnPos found, check scene hierarchy.");
        }
        if (positionObjects.Length < numberOfAsteroids)
        {
            throw new ArgumentException("Too many asteroids (" + numberOfAsteroids + "), or not enough positionObjects (" + positionObjects.Length + ")!");
        }
        // Shuffle order of objects to get a visually interesting pattern each time.
        System.Random rand = new System.Random();
        positionObjects = positionObjects.OrderBy(positionObject => rand.Next()).ToArray();

        // First, generate the real asteroids:
        int[] valuesOfGaps = level.GetValuesOfGaps();
        for (int i = 0; i < valuesOfGaps.Length; i++)
        {
            Debug.Log("Real: " + valuesOfGaps[i]);
            SetupAsteroid(valuesOfGaps[i], rand, positionObjects[i]);
        }

        List<int> prevRandomNumbers = new List<int>();
        // Then generate the extra asteroids with garbage!
        for (int i = 0; i < extraAsteroids; i++)
        {
            int number;

            int max = Math.Max(numberOfAsteroids, 10);
            do
            {
                number = rand.Next(0, max);
            } while (valuesOfGaps.Contains(number) || prevRandomNumbers.Contains(number));
            
            Debug.Log("Fake: " + number);
            SetupAsteroid(number, rand, positionObjects[valuesOfGaps.Length + i]);
            prevRandomNumbers.Add(number);
        }
    }

    void SetupAsteroid(int number, System.Random rand, GameObject positionObject)
    {
        GameObject a = Instantiate(asteroid, positionObject.GetComponent<Transform>().position, Quaternion.identity);
        a.transform.Rotate(0, 0, rand.Next(360));
        a.transform.Find("Canvas/Text").GetComponent<UnityEngine.UI.Text>().text = number.ToString();

    }

    Level GenerateLevel(int level)
    {
        switch (level)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
            case 15: return new Level(1, new List<int>[] { new List<int> { 1, 2, 3 }, new List<int> { 0, 5, 6 }, new List<int> { 4, 5, 6 }, new List<int> { 4, 5, 6 } }, new Operator[] { Operator.Add, Operator.Divide, Operator.Divide, Operator.Equals }, new List<bool> { true, false, true, false, true });
        }
        return null;
    }



    // Update is called once per frame
    void Update()
    {
    }
}
