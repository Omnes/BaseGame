using UnityEngine;

namespace BaseGame.Assets
{
    public interface IMotor
    {
         Vector2 InputVector {get; set;}
         float Velocity {get;}
    }
}