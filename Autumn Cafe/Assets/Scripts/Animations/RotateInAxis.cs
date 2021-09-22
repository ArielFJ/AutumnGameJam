using DG.Tweening;
using UnityEngine;

public class RotateInAxis : MonoBehaviour
{
    public Vector3 axisRotation;
    public float rotationSpeed;

    void FixedUpdate()
    {
        //transform.DORotate(axisRotation.normalized, rotationSpeed)
        //    .SetLoops(-1)
        //    .SetUpdate(true);
        transform.localEulerAngles += axisRotation.normalized * rotationSpeed * Time.fixedDeltaTime;
    }
}
