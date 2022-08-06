using UnityEngine;
using System.Collections;

public class RotatableCube : MonoBehaviour
{
    public Direction direction;
    public float rotationSpeed;

    public enum Direction
    {
        Right,
        Left
    }

    private void FixedUpdate()
    {
        ControlRotation();
    }

    private void ControlRotation()
    {
        float rotationDirection = 0;

        if (direction == Direction.Right) rotationDirection = -1;
        if (direction == Direction.Left) rotationDirection = 1;

        transform.Rotate(Vector3.forward * rotationDirection, rotationSpeed * Time.deltaTime);
    }
}