using UnityEngine;
using UnityEngine.AI;


public class Input : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private LayerMask _navigationSurface;
    [SerializeField] private DestinationFlag _flag;
    [SerializeField] private GameObject _medkitPrefab;

    private Controller _controller;
    private SurroundingSpawner _medkitSpawner;

    private void Awake ()
    {
        NavMeshQueryFilter filter = new NavMeshQueryFilter {agentTypeID = 0, areaMask = NavMesh.AllAreas};

        _controller = new NavMeshAgentController(_character, filter, _navigationSurface);
        _controller.Enable();

        _medkitSpawner = new SurroundingSpawner(_medkitPrefab, _character.transform, 5f, this, 1f,3f);
    }

    private void Update ()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            SetDestinationFlag();
        }

        _controller.Update();

        if (UnityEngine.Input.GetKeyDown(KeyCode.F))
        {
            _medkitSpawner.Switch();
        }
    }

    private void SetDestinationFlag ()
    {
        if (RayCaster.TryGetRayCastPoint(Camera.main.transform.position, Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition).direction, _navigationSurface, out RaycastHit hit))
        {
            _flag.FlagPoint(hit.point);
        }
    }

    public void Destroy ()
    {
        Destroy(gameObject);
    }
}
