using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


public class SurroundingSpawner
{

    private GameObject    _objectPrefab;
    private Transform     _anchor;
    private float         _rate;
    private MonoBehaviour _coroutineRunner;
    private Coroutine     _spawnCoroutine;
    private float         _minDistance;
    private float         _maxDistance;

    public SurroundingSpawner (GameObject objectPrefab, Transform anchor, float rate, MonoBehaviour coroutineRunner, float minDistance, float maxDistance)
    {
        _objectPrefab = objectPrefab;
        _anchor = anchor;
        _rate = rate;
        _coroutineRunner = coroutineRunner;
        _minDistance = minDistance;
        _maxDistance = maxDistance;
    }

    public void Switch ()
    {
        if (_spawnCoroutine != null)
        {
            _coroutineRunner.StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
        else
        {
            _spawnCoroutine = _coroutineRunner.StartCoroutine(SpawnObject());
        }
    }

    private IEnumerator SpawnObject ()
    {
        while (true)
        {
            float   distance  = Random.Range(_minDistance, _maxDistance);
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));

            Vector3 relativePosition = direction.normalized * distance;
            relativePosition.y = 0.5f;

            Object.Instantiate(_objectPrefab, _anchor.position + relativePosition, Quaternion.identity);

            yield return new WaitForSeconds(_rate);
        }
    }
}
