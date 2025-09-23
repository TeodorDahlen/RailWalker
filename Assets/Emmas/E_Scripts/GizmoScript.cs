using UnityEngine;

public class GizmoScript : MonoBehaviour
{
    public float radius = 1f;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
