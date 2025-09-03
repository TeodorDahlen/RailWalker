using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Grab;

public class TwoHandedShovel : MonoBehaviour, ITransformer
{
    private Grabbable _grabbable;
    private Pose _primaryHandPose;
    private Pose _secondaryHandPose;

    public void Initialize(IGrabbable grabbable)
    {
        _grabbable = grabbable as Grabbable;
    }

    public void BeginTransform()
    {
        if (_grabbable.SelectingPointsCount == 2)
        {
            // Cache initial hand poses
            _primaryHandPose = _grabbable.GrabPoints[0];
            _secondaryHandPose = _grabbable.GrabPoints[1];
        }
    }

    public void UpdateTransform()
    {
        if (_grabbable.SelectingPointsCount == 1)
        {
            // One hand: follow directly
            _grabbable.Transform.SetPose(_grabbable.GrabPoints[0]);
        }
        else if (_grabbable.SelectingPointsCount == 2)
        {
            Pose primary = _grabbable.GrabPoints[0];
            Pose secondary = _grabbable.GrabPoints[1];

            // Position stays at the primary hand
            _grabbable.Transform.position = primary.position;

            // Get forward direction from primary → secondary
            Vector3 dir = (secondary.position - primary.position).normalized;

            // Rotation anchored to primary hand, twisted toward secondary
            Quaternion lookRot = Quaternion.LookRotation(dir, Vector3.up);

            // Blend primary’s rotation with two-hand direction
            _grabbable.Transform.rotation = Quaternion.Slerp(primary.rotation, lookRot, 0.8f);
        }
    }

    public void EndTransform() { }
}
