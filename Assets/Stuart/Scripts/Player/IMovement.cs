
using System;

internal interface IMovement
{
    public event Action<float> MoveAmount;
    public event Action<float> RotateAmount;
}