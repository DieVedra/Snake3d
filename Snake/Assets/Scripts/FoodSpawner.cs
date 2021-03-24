using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [Header("Only quadrilaterals")]
    [SerializeField] private GameObject[] _pointsBorderSpawnZone;
    static public bool DoSpawnFood;

    [SerializeField] private float TimeSpawn;
    private float TimeToSpawn;

    private int _countTriangle;

    private int _amountOfFoodPerSpawn;

    [SerializeField] private List<GameObject> FoodsPrefabs;
    [SerializeField] private List<GameObject> ObstaclePrefabs;

    [SerializeField] private DetectorCollision _detectorCollision;
    [SerializeField] private GameData _gameData;

    private void Start()
    {
        _detectorCollision = FindObjectOfType<DetectorCollision>();
        _detectorCollision.OnEat += AmountOfFoodPerSpawnCounter;

        CheckingNumbersPoints();
        _countTriangle = _pointsBorderSpawnZone.Length / 2;
        //_detectorCollision = FindObjectOfType<DetectorCollision>();
        DoSpawnFood = true;
    }
    private void Update()
    {
        if (DoSpawnFood)
        {
            if (TimeToSpawn < 0)
            {
                TimeToSpawn += TimeSpawn;
                SpawnFood();
                //TryObstacleSpawned();
            }
            TimeToSpawn -= Time.deltaTime;
        }
    }

    private void SpawnFood()
    {
        //int indexNumber = CreateRandomIndexTriangle();
        _amountOfFoodPerSpawn = Random.Range(1, 20);
        _amountOfFoodPerSpawn += (int)_gameData.DifficultyLevelOfTheDay;

        for (int i = 0; i < _amountOfFoodPerSpawn; i++)
        {
            int indexNumber = CreateRandomIndexTriangle();

            Instantiate(FoodsPrefabs[Random.Range(0, FoodsPrefabs.Count)],
                        CalculationCoordinatesPoint(_pointsBorderSpawnZone[indexNumber].transform.position,
                                                 _pointsBorderSpawnZone[indexNumber + 1].transform.position,
                                                 _pointsBorderSpawnZone[indexNumber + 2].transform.position),
                        Quaternion.identity);
        }

        if (TryObstacleSpawned())
        {
            int indexNumber = CreateRandomIndexTriangle();

            Instantiate(ObstaclePrefabs[Random.Range(0, ObstaclePrefabs.Count)],
                        CalculationCoordinatesPoint(_pointsBorderSpawnZone[indexNumber].transform.position,
                                                 _pointsBorderSpawnZone[indexNumber + 1].transform.position,
                                                 _pointsBorderSpawnZone[indexNumber + 2].transform.position),
                        Quaternion.Euler(0f, GetRandomValue() * 180f, 0f));
        }


        DoSpawnFood = false;
    }

    private bool TryObstacleSpawned()
    {
        if (GetRandomValue() * _gameData.DifficultyLevelOfTheDay <= 0.8f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private int CreateRandomIndexTriangle()
    {
        int randomValue = Random.Range(0, _countTriangle);
        int selectedTriangletIndex = 0;

        if (randomValue == 1 || randomValue == 0)
        {
            selectedTriangletIndex = randomValue;
        }
        else if (randomValue % 2 == 0)
        {
            selectedTriangletIndex = randomValue + randomValue;
        }
        else if (randomValue % 2 == 1)
        {
            selectedTriangletIndex = randomValue + --randomValue;
        }

        return selectedTriangletIndex;
    }
    private Vector3 CalculationCoordinatesPoint(Vector3 point1, Vector3 point2, Vector3 point3)
    {
        Vector3 point12 = Vector3.Lerp(point1, point2, GetRandomValue());
        Vector3 point23 = Vector3.Lerp(point2, point3, GetRandomValue());
        Vector3 point31 = Vector3.Lerp(point3, point1, GetRandomValue());
        Vector3 point1223 = Vector3.Lerp(point12, point23, GetRandomValue());
        Vector3 point122331 = Vector3.Lerp(point1223, point31, GetRandomValue());

        return point122331;
    }

    //public void DoSpawn()
    //{
    //    _doSpawnFood = true;
    //}

    private float GetRandomValue()
    {
        return Random.Range(0f, 1.01f);
    }

    private void AmountOfFoodPerSpawnCounter(int a)
    {
        _amountOfFoodPerSpawn--;

        if (_amountOfFoodPerSpawn <= 0)
        {
            DoSpawnFood = true;
        }
    }

    private void CheckingNumbersPoints()
    {
        if (_pointsBorderSpawnZone.Length < 4 || _pointsBorderSpawnZone.Length % 4 != 0 )
        {
            Debug.LogError("Points are less than four or their number  is not a multiple of four");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        for (int i = 0; i < _pointsBorderSpawnZone.Length; i+=4)
        {
            DrawQuadrilaterals(i);
        }
    }

    private void DrawQuadrilaterals(int index)
    {
        Gizmos.DrawLine(_pointsBorderSpawnZone[index].transform.position, _pointsBorderSpawnZone[index + 1].transform.position);     // 1 - 2   0, 0+1
        Gizmos.DrawLine(_pointsBorderSpawnZone[index + 1].transform.position, _pointsBorderSpawnZone[index + 3].transform.position); // 2 - 4   0+1, 0+3
        Gizmos.DrawLine(_pointsBorderSpawnZone[index + 3].transform.position, _pointsBorderSpawnZone[index + 2].transform.position); // 4 - 3   0+3, 0+2
        Gizmos.DrawLine(_pointsBorderSpawnZone[index + 2].transform.position, _pointsBorderSpawnZone[index].transform.position);     // 3 - 1   0+2, 0
    }

    //private void OnEnable()
    //{
    //    _detectorCollision = FindObjectOfType<DetectorCollision>();
    //    _detectorCollision.OnEat += AmountOfFoodPerSpawnCounter;
    //}

    private void OnDisable()
    {
        _detectorCollision.OnEat -= AmountOfFoodPerSpawnCounter;
    }
}
