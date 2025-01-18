using PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop;
using PlayneraTest.Scripts.Gameplay.PseudoPhysics;
using UnityEngine.EventSystems;

namespace Assets.PlayneraTest.Scripts.Gameplay.General
{
    public class DragableItemViewModel
    {
        private IPseudoPhysicsBody _ppb;
        private IDragDropItem _ddi;

        public DragableItemViewModel(IPseudoPhysicsBody ppb, IDragDropItem ddi)
        {
            _ppb = ppb;
            _ddi = ddi;

            ddi.PointerDown += OnPointerDown;
        }

        private void OnPointerDown(IDragDropItem item, PointerEventData data)
        {
            _ppb.IsActive = false;
            _ppb.IsGroundPositionDirty = true;
        }
    }
}
