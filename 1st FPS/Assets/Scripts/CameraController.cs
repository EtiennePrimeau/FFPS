using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform m_playerTransform;
    [SerializeField] private float m_lerpF = 0.1f;
    [SerializeField] private float m_horizontalRotationSpeed = 0.1f;
    [SerializeField] private float m_VerticalRotationSpeed = 5.0f;
    private float m_lerpedAngleX;
    private float m_lerpedAngleY;
    [SerializeField] private Vector2 m_clampingXRotationValues;

    private void FixedUpdate()
    {
        RotateAroundObjectHorizontal();
        RotateAroundObjectVertical();
    }

    void RotateAroundObjectHorizontal()
    {
        float currentAngleX = Input.GetAxis("Mouse X") * m_horizontalRotationSpeed;
        m_lerpedAngleX = Mathf.Lerp(m_lerpedAngleX, currentAngleX, m_lerpF);

        transform.RotateAround(m_playerTransform.position, m_playerTransform.up, m_lerpedAngleX);
    }

    void RotateAroundObjectVertical()
    {
        float currentAngleY = Input.GetAxis("Mouse Y") * m_VerticalRotationSpeed;

        var xRotationValue = transform.rotation.eulerAngles.x;
        float comparisonAngle = xRotationValue + currentAngleY;

        if (comparisonAngle > 180)
        {
            comparisonAngle -= 360;
        }

        m_lerpedAngleY = Mathf.Lerp(m_lerpedAngleY, currentAngleY, m_lerpF);

        if (currentAngleY > 0 && comparisonAngle < m_clampingXRotationValues.x)
        {
            m_lerpedAngleY = 0;
        }
        if (currentAngleY < 0 && comparisonAngle > m_clampingXRotationValues.y)
        {
            m_lerpedAngleY = 0;
        }

        transform.RotateAround(m_playerTransform.position, -transform.right, m_lerpedAngleY);
    }
}