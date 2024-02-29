using UnityEngine;

public class SpawnElements : MonoBehaviour
{
    [Header("Spawn Points Players")]
    [SerializeField] private Transform _leftSpawnPoint;
    [SerializeField] private Transform _rightSpawnPoint;
    [SerializeField] private GameObject _playerlPrefab;
    [SerializeField] private GameObject _botPrefab;

    [Header("Spawn Point Ball")]
    [SerializeField] private Transform _ballSpawnPoint;
    [SerializeField] private GameObject _ballPrefab;

    [Header("Spawn Points Objects")]
    [SerializeField] private Transform[] _objectsSpawnPoints;
    [SerializeField] private GameObject _objectPrefab;

    private void Start()
    { 
        SpawnNewPlayers();
        SpawnNewObjects();
        SpawnNewBall();
    }

    public void SpawnNewPlayers()
    {
        Instantiate(_playerlPrefab, _leftSpawnPoint.position, Quaternion.identity);
        Instantiate(_botPrefab, _rightSpawnPoint.position, Quaternion.Euler(0f, 90f, 0f));
    }

    public void SpawnNewObjects()
    {
        foreach (Transform t in _objectsSpawnPoints[Random.Range(0, 4)])
            GameObject.Instantiate(_objectPrefab, t.position, Quaternion.identity);
    }

    public void SpawnNewBall() => Instantiate(_ballPrefab, _ballSpawnPoint.position, Quaternion.identity);
}