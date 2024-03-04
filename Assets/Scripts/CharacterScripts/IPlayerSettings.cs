using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace Character
{
    public interface IPlayerSettings
    {
        float TargetDirectionY { get; set; }
        CharacterController CharacterController { get; }
    }
}