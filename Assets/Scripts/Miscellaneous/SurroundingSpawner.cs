using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public class SurroundingSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private Transform  _anchor;
    [SerializeField] private float      _rate;

    private Coroutine _spawnCoroutine;

    private void Update ()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.F))
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }
            else
            {
                _spawnCoroutine = StartCoroutine(SpawnObject());
            }
        }
    }

    private IEnumerator SpawnObject ()
    {
        while (true)
        {
            float   distance  = Random.Range(1f, 3f);
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));

            Vector3 relativePosition = direction.normalized * distance;
            relativePosition.y = 0.5f;

            Instantiate(_objectPrefab, _anchor.position + relativePosition, Quaternion.identity);

            yield return new WaitForSeconds(_rate);
        }
    }
}
