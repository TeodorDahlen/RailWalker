using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f;
    //if any bounciness in deadzone it wont bounce upp and down. 
    [SerializeField] private float deadZone = 0.015f; 

    public UnityEvent OnPressed, OnReleased;
    private bool _isPressed;
    private Vector3 _startPos;
    private ConfigurableJoint _joint;

    void Start()
    {
        _startPos = transform.localPosition;
        _joint = GetComponent<ConfigurableJoint>();

    }

    void Update()
    {
        if (!_isPressed && GetValue() + threshold >= 1)
        {
            Pressed();
        }

        if (_isPressed && GetValue() - threshold <= 0)
        {
            Released();
        }
    }

    private float GetValue()
    {
        //gets the %
        var value = Vector3.Distance(_startPos, transform.localPosition / _joint.linearLimit.limit);

        //if its within the small number in deadzone return to 0
        if (Mathf.Abs (value) < deadZone)
        {
            value = 0;
        }

        // returns a clamped version 
        return Mathf.Clamp(value, -1f, 1f);
    }


    private void Pressed ()
    {
        _isPressed = true;
        OnPressed?.Invoke();
        Debug.Log("Pressed");
    }

    private void Released ()
    {
        _isPressed = false;
        OnReleased?.Invoke();
        Debug.Log("Released");
    }
}
