using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayneraTest.Scripts.Gameplay.PseudoPhysics
{
    public class PseudoPhysicsBody : MonoBehaviour, IPseudoPhysicsBody, IEndDragHandler, IBeginDragHandler
    {
        public float GroundPositionY { get; private set; }
        public float VerticalVelocity { get; private set; }
        public float VerticalAcceleration { get; private set; }

        public bool IsTouchedGround { get; set; } = false;
        public bool IsGroundPositionDirty { get; set; } = true;
        public bool IsActive { get; set; }

        public void AddVerticalAcceleration(float acceleration)
        {
            VerticalAcceleration += acceleration;
        }

        public void SetGroundPoint(float groundPosY)
        {
            GroundPositionY = groundPosY;
            IsGroundPositionDirty = false;
        }

        private void Update()
        {
            if (!IsActive) return;
            
            VerticalVelocity += VerticalAcceleration * Time.deltaTime;
            
            var pos = transform.position;
            pos.y += VerticalVelocity * Time.deltaTime;

            if (pos.y < GroundPositionY)
            {
                VerticalAcceleration = 0;
                VerticalVelocity = 0;
                pos.y = GroundPositionY;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            IsGroundPositionDirty = true;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            IsGroundPositionDirty = false;
        }
    }
}