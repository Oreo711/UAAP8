using System;
using Unity.VisualScripting;
using UnityEngine;

public class DestinationFlag : MonoBehaviour
{
    [SerializeField] private GameObject _flagPrefab;
    [SerializeField] private Character  _character;

    private GameObject _activeFlag;

    public void FlagPoint (Vector3 point)
    {
        Destroy(_activeFlag);
        GameObject flag = Instantiate(_flagPrefab, point, Quaternion.identity);
        _activeFlag = flag;
    }

    private void Update ()
    {
        if (_activeFlag)
        {
            if ((_character.transform.position - _activeFlag.transform.position).magnitude < 0.1f)
            {
                Destroy(_activeFlag);
            }
        }
    }
}
