using Assets.PlayneraTest.Scripts.Gameplay.CameraScripts;
using PlayneraTest.Scripts.Gameplay.CameraScripts;
using PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop.DragDropActions
{
    public class MoveOnDrag : DragDropAbstractAction
    {
        private const float VERTICAL_OFFSET_IN_UNITS = 0.3f;

        public override void DoAction(IDragDropItem dragDropItem, PointerEventData eventData, AdditionalDragDropInfo additionalDragDropInfo)
        {
            var worlPointerPosition = additionalDragDropInfo.Camera.ScreenToWorldPoint(eventData.position);
            var pointerPositionDelta = additionalDragDropInfo.PointerStartWorldPosition - worlPointerPosition;
            pointerPositionDelta.z = 0;

            var targetPosition = additionalDragDropInfo.ItemStartWorldPosition - pointerPositionDelta;
            targetPosition.y += VERTICAL_OFFSET_IN_UNITS;
            targetPosition.x = worlPointerPosition.x;

            dragDropItem.Position = targetPosition;
        }
    }
}
