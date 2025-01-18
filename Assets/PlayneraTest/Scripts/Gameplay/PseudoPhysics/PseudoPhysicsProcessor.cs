using Assets.PlayneraTest.Scripts.Gameplay.CustomEventSystem;
using PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayneraTest.Scripts.Gameplay.PseudoPhysics
{
    public class PseudoPhysicsProcessor
    {
        private readonly float _verticalAcceleration;
        private readonly IEventSystemAccessor _eventSystemAccessor;
        private readonly IDragDropRegister _dragDropRegister;
        private List<PseudoPhysicsBody> ppbs;

        public PseudoPhysicsProcessor(IDragDropRegister dragDropRegister, IEventSystemAccessor eventSystemAccessor, float verticalAcceleration = -10f)
        {
            _verticalAcceleration = verticalAcceleration;
            _eventSystemAccessor = eventSystemAccessor;
            _dragDropRegister = dragDropRegister;
            dragDropRegister.EndDrag += OnDragEnd;
            ppbs = new List<PseudoPhysicsBody>();
        }

        private void OnDragEnd(IDragDropItem item, PointerEventData data)
        {
            if (_eventSystemAccessor.TryGetObjectUnderMouse(out IDragDropItem surface))
            {
                if(item is MonoBehaviour mono && mono.TryGetComponent<IPseudoPhysicsBody>(out var ppb))
                {
                    switch (surface.ItemType)
                    {
                        case EItemType.Item:
                        case EItemType.Background:
                            ppb.IsTouchedGround = false;
                            ppb.IsGroundPositionDirty = true;
                            ppb.IsActive = true;
                            break;
                        case EItemType.Surface:
                            ppb.IsTouchedGround = true;
                            ppb.IsActive = false;
                            break;
                        default:
                            Debug.LogError("Out of enum range!");
                            break;
                    }
                }
            }
        }

        public void Add(PseudoPhysicsBody ppb)
        {
            ppbs.Add(ppb);
        }

        public void FixedUpdate()
        {
            foreach (var ppb in ppbs)
            {
                if (ppb.IsActive && !ppb.IsTouchedGround)
                {
                    if (ppb.IsGroundPositionDirty)
                    {
                        var result = Physics2D.Raycast(ppb.transform.position, Vector2.down, 100f, LayerMask.GetMask($"Floor"));
                        if (result.transform != null)
                        {
                            ppb.GroundPositionY = result.point.y;
                            ppb.IsGroundPositionDirty = false;
                            ppb.IsTouchedGround = false;
                            ppb.IsActive = true;
                            Debug.Log($"Falling target: {ppb.GroundPositionY}");
                        }
                        else
                        {
                            ppb.IsTouchedGround = true;
                            ppb.IsActive = false;
                            Debug.LogError("Have no ground under item!");
                        }
                    }

                    
                    ppb.VerticalVelocity += _verticalAcceleration * Time.deltaTime;

                    var pos = ppb.transform.position;
                    pos.y += ppb.VerticalVelocity * Time.deltaTime;

                    if (pos.y < ppb.GroundPositionY)
                    {
                        ppb.VerticalVelocity = 0;
                        pos.y = ppb.GroundPositionY;
                        ppb.IsTouchedGround = true;
                        ppb.IsActive = false;
                    }

                    ppb.transform.position = pos;
                }
            }
        }
    }
}