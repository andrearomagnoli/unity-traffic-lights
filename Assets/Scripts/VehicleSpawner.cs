using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

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

    [SerializeField]
    [Tooltip("Starting speed.")]
    private float startingSpeed;

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

    // ABSTRACTION
    /// <summary>
    /// Reset timer for reducing generation delay.
    /// </summary>
    private void ResetTimerReduce()
    {
        _timerReduce = timeRangeDecreaseDelay;
    }

    // ABSTRACTION
    /// <summary>
    /// Reset timer for vehicle generation.
    /// </summary>
    private void ResetTimerGenerate()
    {
        _timerGenerate = Mathf.Clamp(Random.Range(timeRange.x, timeRange.y) - timeRangeDecreaseAmount * _timeRangeDecreaseAmountCounter, timeRangeMinimum, Mathf.Infinity);
    }

    // ABSTRACTION
    // POLYMORPHISM
    /// <summary>
    /// Generate a random vehicle.
    /// </summary>
    private void GenerateVehicle()
    {
        int randVehicle = Random.Range(0, vehicles.Length);
        int randTransform = Random.Range(0, _spawnPoints.Count);

        GameObject newVehicle = Instantiate(vehicles[randVehicle], _spawnPoints[randTransform]);
        newVehicle.GetComponent<Rigidbody>().velocity = new Vector3(startingSpeed, 0f, 0f);
    }

    // ABSTRACTION
    // POLYMORPHISM
    /// <summary>
    /// Generate a specific vehicle.
    /// </summary>
    private void GenerateVehicle(int vehicleNumber)
    {
        if (vehicleNumber < 0 || vehicleNumber > vehicles.Length)
        {
            throw new Exception("Vehicle number is out of range");
        }
        int randTransform = Random.Range(0, _spawnPoints.Count);

        GameObject newVehicle = Instantiate(vehicles[vehicleNumber], _spawnPoints[randTransform]);
        newVehicle.GetComponent<Rigidbody>().velocity = new Vector3(startingSpeed, 0f, 0f);
    }

    #endregion
}
