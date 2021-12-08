using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    #region Parameters

    [SerializeField]
    [Tooltip("Vehicle that are generated randomly.")]
    private GameObject[] vehicles;

    [SerializeField]
    [Tooltip("Time range between are generated random cars.")]
    private Vector2 timeRange;

    [SerializeField]
    [Tooltip("Seconds after which the ratio of vehicles generated is decreased.")]
    private float timeRangeDecreaseDelay;

    [SerializeField]
    [Tooltip("Amount of time that the time range is reduced when the decrese delay expires.")]
    private float timeRangeDecreaseAmount;

    [SerializeField]
    [Tooltip("Minimum time delta allowed for vehicle generation.")]
    private float timeRangeMinimum;

    private List<Transform> _spawnPoints;

    private float _timerReduce;
    private float _timerGenerate;
    private int _timeRangeDecreaseAmountCounter = 0;

    #endregion

    #region UnityEvents

    private void Awake()
    {
        _spawnPoints = new List<Transform>();
        foreach(Transform spawnPoint in transform)
        {
            _spawnPoints.Add(spawnPoint);
        }

        ResetTimerReduce();
        ResetTimerGenerate();
    }

    private void Update()
    {
        _timerReduce -= Time.deltaTime;
        _timerGenerate -= Time.deltaTime;

        if (_timerReduce <= 0f)
        {
            _timeRangeDecreaseAmountCounter++;
            ResetTimerReduce();
        }
        if (_timerGenerate <= 0f)
        {
            GenerateVehicle();
            ResetTimerGenerate();
        }
    }

    #endregion

    #region Methods

    private void ResetTimerReduce()
    {
        _timerReduce = timeRangeDecreaseDelay;
    }
    
    private void ResetTimerGenerate()
    {
        _timerGenerate = Mathf.Clamp(Random.Range(timeRange.x, timeRange.y) - timeRangeDecreaseAmount * _timeRangeDecreaseAmountCounter, timeRangeMinimum, Mathf.Infinity);
    }

    private void GenerateVehicle()
    {
        int randVehicle = Random.Range(0, vehicles.Length);
        int randTransform = Random.Range(0, _spawnPoints.Count);

        Instantiate(vehicles[randVehicle], _spawnPoints[randTransform]);
    }

    #endregion
}
