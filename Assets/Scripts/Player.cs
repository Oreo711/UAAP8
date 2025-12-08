using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Character       _character;
    [SerializeField] private LayerMask       _navigationSurface;
    [SerializeField] private DestinationFlag _flag;

    private void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetCharacterDestination();
        }
    }

    private void SetCharacterDestination ()
    {
        if (RayCaster.TryGetRayCastPoint(
                Camera.main.transform.position,
                Camera.main.ScreenPointToRay(Input.mousePosition).direction,
                _navigationSurface,
                out RaycastHit hit))
        {
            _character.WalkTo(hit.point);
            _flag.FlagPoint(hit.point);
        }
    }

    private void Destroy ()
    {
        Destroy(gameObject);
    }
}
