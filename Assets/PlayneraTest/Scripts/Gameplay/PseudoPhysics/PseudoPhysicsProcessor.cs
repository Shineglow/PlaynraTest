using System.Collections.Generic;
using UnityEngine;

namespace PlayneraTest.Scripts.Gameplay.PseudoPhysics
{
    public class PseudoPhysicsProcessor
    {
        private readonly float _verticalAcceleration;
        
        private List<PseudoPhysicsBody> ppbs;

        public PseudoPhysicsProcessor(float verticalAcceleration = 10f)
        {
            _verticalAcceleration = verticalAcceleration;
            ppbs = new List<PseudoPhysicsBody>();
        }
        
        public void Add(PseudoPhysicsBody ppb)
        {
            ppbs.Add(ppb);
        }

        private void FixedUpdate()
        {
            foreach (var ppb in ppbs)
            {
                ppb.AddVerticalAcceleration(_verticalAcceleration);
                if (ppb.IsActive && ppb.IsGroundPositionDirty && !ppb.IsTouchedGround)
                {
                    Physics2D.Raycast(ppb.transform.position, Vector2.down, 100f, LayerMask.GetMask($"Floor"));
                }
            }
        }
    }
}