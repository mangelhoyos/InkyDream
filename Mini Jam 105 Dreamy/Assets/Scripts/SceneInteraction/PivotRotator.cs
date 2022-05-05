using UnityEngine;

public class PivotRotator : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform pivotPoint;
    void Update()
    {
        transform.RotateAround(pivotPoint.position, pivotPoint.forward, speed * Time.deltaTime);
    }
}
