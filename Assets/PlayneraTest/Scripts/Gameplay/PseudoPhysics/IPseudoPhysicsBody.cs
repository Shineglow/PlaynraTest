using UnityEngine;

namespace PlayneraTest.Scripts.Gameplay.PseudoPhysics
{
    public interface IPseudoPhysicsBody
    {
        bool IsTouchedGround { get; set; }
    }
}