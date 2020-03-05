using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using NCalc;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    Level currentLevel;
    int countOnCurrentLevel;

    public const int MAX_LEVEL = 15;

    public GameObject AsteroidPrefab;
    public Question QuestionText;
    public int NumberOfExtraAsteroids;
    public Text levelNum;

    Fire spaceship;
    ShieldStateHandler shield;
    Timer timer;
    SceneHandler sceneHandler;

    bool shouldUpdate;

    

    // Start is called before the first frame update
    void Start()
    {
        if (!CurrentLevel.init)
        {
            CurrentLevel.init = true;
            CurrentLevel.Difficulty = Difficulty.Easy;
            CurrentLevel.LevelNo = 1;
            CurrentLevel.NoBlanks = 1;
            CurrentLevel.NoNumbers = 2;
            CurrentLevel.NoQuestions = 5;
            CurrentLevel.Numbers = new List<int> { 1, 2, 3, 4, 5 };
            CurrentLevel.Operators = new List<Operator> { Operator.Add };
        }

        currentLevel = GenerateLevel();
        countOnCurrentLevel = 0;
        SetupLevel(ref currentLevel);
        spaceship = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<Fire>();
        shield = GameObject.FindGameObjectWithTag("Shields").GetComponent<ShieldStateHandler>();
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        shouldUpdate = false;
    }

    void SetupLevel(ref Level level)
    {
        if (level == null) throw new ArgumentNullException("Level is Null!");

        levelNum.text = level.levelNum.ToString();

        int numberOfRealAsteroids = level.GetNumberOfGaps();
        int numberOfAsteroids = numberOfRealAsteroids + NumberOfExtraAsteroids;
        Debug.Log("Generating " + numberOfAsteroids + " asteroids.\nReal: " + numberOfRealAsteroids + " Fake: " + NumberOfExtraAsteroids);

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
        for (int i = 0; i < NumberOfExtraAsteroids; i++)
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

        QuestionText.SetQuestion(currentLevel.statementString, GetVisible());
    }

    void SetupAsteroid(int number, System.Random rand, GameObject positionObject)
    {
        GameObject a = Instantiate(AsteroidPrefab, positionObject.GetComponent<Transform>().position, Quaternion.identity);
        a.transform.Rotate(0, 0, rand.Next(360));
        a.transform.Find("NoRotation/Canvas/Text").GetComponent<UnityEngine.UI.Text>().text = number.ToString();

    }

    public int[] GetQuestionTextNumbers()
    {
        return currentLevel.questionNumbers;
    }

    public Operator[] GetOperatorsUsed()
    {
        return currentLevel.operatorsUsed;
    }

    public List<bool> GetVisible()
    {
        return currentLevel.visible;
    }

    public bool ValidateAnswer()
    {
        string expression = QuestionText.GetExpression();
        int? answer = QuestionText.GetAnswer();

        if (answer == null) return false;
        int intAnswer = answer.GetValueOrDefault();
        
        try {
            int calcAnswer = Convert.ToInt32(new Expression(expression).Evaluate());
            return calcAnswer == answer;
        }
        catch (Exception e) {
            Debug.Log("Exception thrown when validating answer, assuming invalid!");
            Debug.Log(e.Message);
            return false;
        }
    }

    public bool AreAllGapsFilled()
    {
        return QuestionText.AreAllGapsFilled();
    }

    Level GenerateLevel()
    {
        // Number Ranges
        // Allows for extendability in the future with different numbers for each question to test specific things e.g. 5 times tables, dividing by 3, etc.
        List<int>[] numberRanges = new List<int>[CurrentLevel.NoNumbers];
        Debug.Log("Number Ranges Len: " + numberRanges.Length);
        for (int i = 0; i < CurrentLevel.NoNumbers; ++i)
        {
            numberRanges[i] = CurrentLevel.Numbers.ToList();
        }

        List<Operator> selectedOperators = CurrentLevel.Operators;

        // Operators
        Operator[] operators = new Operator[CurrentLevel.NoNumbers];
        Debug.Log("Operators Len: " + operators.Length);
        // Shuffling operators
        System.Random rand = new System.Random();
        selectedOperators = selectedOperators.OrderBy(it => rand.Next()).ToList();

        // Selecting a random operator by shuffling the list and iterating through it
        // Once list has been expended, reshuffle and continue
        int selectedIndex = 0;
        for (int i = 0; i < operators.Length - 1; ++i)
        {
            if (selectedIndex == 4)
            {
                selectedIndex = 0;
                selectedOperators = selectedOperators.OrderBy(it => rand.Next()).ToList();
            }
            operators[i] = selectedOperators[selectedIndex++];
        }
        operators[operators.Length - 1] = Operator.Equals;


        // Selecting visible randomly
        bool[] visible = new bool[CurrentLevel.NoNumbers + 1].Select(it => true).ToArray();
        Debug.Log("visible Len: " + visible.Length);
        Debug.Log("Number of blanks: " + CurrentLevel.NoBlanks);
        for (int i = 0; i < CurrentLevel.NoBlanks; ++i)
        {
            int randomIndex = -1;
            do
            {
                randomIndex = rand.Next(0, visible.Length);
            } while (randomIndex == -1 || !visible[randomIndex]);
            visible[randomIndex] = false;
        }


        return new Level(CurrentLevel.LevelNo, numberRanges, operators, visible.ToList());
    }

    public void UpdateCurrentLevel()
    {
        ResetScene();
        countOnCurrentLevel++;
        if (countOnCurrentLevel == 5)
        {
            countOnCurrentLevel = 0;
            int levelNo = currentLevel.levelNum;
            if (levelNo >= ConfigData.NumberOfLevels)
            {
                sceneHandler.GoGameOverScene();
            }
            levelNo++; // Next Level
            LevelDTO newLevelDTO = ConfigData.LevelData.Levels.Find(it => it.LevelNo == levelNo);

            CurrentLevel.LevelNo = newLevelDTO.LevelNo;
            CurrentLevel.Difficulty = newLevelDTO.Difficulty.ToDifficulty(); ;
            CurrentLevel.Numbers = newLevelDTO.Numbers;
            CurrentLevel.Operators = newLevelDTO.Operators.Select(it => it.ToOperator()).ToList();
            CurrentLevel.NoNumbers = newLevelDTO.NoNumbers;
            CurrentLevel.NoQuestions = newLevelDTO.NoQuestions;
            CurrentLevel.NoBlanks = newLevelDTO.NoBlanks;
        }
        currentLevel = GenerateLevel();
        SetupLevel(ref currentLevel);
    }

    private void ResetScene()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        foreach (GameObject asteroid in asteroids) Destroy(asteroid);
        QuestionText.Clear();
        spaceship.Reset();
        shield.InitialiseShieldState();
        timer.InitialiseTimer();
    }

    public void SetLevelShouldUpdate()
    {
        shouldUpdate = true;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("MathsAsset")) Destroy(obj);
        QuestionText.Clear();
    }

    void Update()
    {
        if (shouldUpdate)
        {
            shouldUpdate = false;
            UpdateCurrentLevel();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}
