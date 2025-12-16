using UnityEngine;
using UnityEngine.AI;


public class Input : MonoBehaviour
{
    [SerializeField] private Character       _character;
    [SerializeField] private LayerMask       _navigationSurface;
    [SerializeField] private DestinationFlag _flag;

    private Controller _controller;

    private void Awake ()
    {
        NavMeshQueryFilter filter = new NavMeshQueryFilter {agentTypeID = 0, areaMask = NavMesh.AllAreas};

        _controller = new NavMeshAgentController(_character, filter, _navigationSurface);
        _controller.Enable();
    }

    private void Update ()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            SetDestinationFlag();
        }

        _controller.Update();
    }

    private void SetDestinationFlag ()
    {
        if (RayCaster.TryGetRayCastPoint(Camera.main.transform.position, Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition).direction, _navigationSurface, out RaycastHit hit))
        {
            _flag.FlagPoint(hit.point);
        }
    }

    private void Destroy ()
    {
        Destroy(gameObject);
    }
}
