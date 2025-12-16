using System;
using UnityEngine;
using UnityEngine.AI;


public class NavMeshAgentController : Controller
{
    private INavMeshMovable _movable;
    private LayerMask _navigationSurface;
    private NavMeshPath _path;
    private NavMeshQueryFilter _filter;

    public Vector3 CurrentTarget {get; private set;}

    public NavMeshAgentController (INavMeshMovable movable, NavMeshQueryFilter filter, LayerMask navigationSurface)
    {
        _movable = movable;
        _filter  = filter;
        _navigationSurface = navigationSurface;
        _path    = new NavMeshPath();
        CurrentTarget = _movable.Position;
    }

    protected override void UpdateInternal ()
    {
        if (_movable.IsActive == false)
        {
            return;
        }

        if (_movable.IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData))
        {
            if (_movable.IsJumping == false)
            {
                _movable.Jump(offMeshLinkData);
            }
        }

        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            if (RayCaster.TryGetRayCastPoint(
                    Camera.main.transform.position,
                    Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition).direction,
                    _navigationSurface,
                    out RaycastHit hit))
            {
                if (NavMeshUtilities.TryGetPath(_movable.Position, hit.point, _filter, _path))
                {
                    CurrentTarget = hit.point;
                }
            }
        }

        _movable.WalkTo(CurrentTarget);
    }
}
