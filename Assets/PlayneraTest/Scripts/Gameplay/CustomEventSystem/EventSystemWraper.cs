using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.PlayneraTest.Scripts.Gameplay.CustomEventSystem
{
    public class EventSystemWraper : IEventSystemAccessor
    {
        private EventSystem _eventSystem;

        public EventSystemWraper(EventSystem eventSystem)
        {
            _eventSystem = eventSystem;
        }

        public bool TryGetObjectUnderMouse<T>(out T obj)
        {
            if (!_eventSystem.IsPointerOverGameObject())
            {
                obj = default(T);
                return false;
            }

            PointerEventData pointerData = new PointerEventData(_eventSystem)
            {
                position = Input.mousePosition
            };

            var raycastResults = new List<RaycastResult>();
            _eventSystem.RaycastAll(pointerData, raycastResults);

            Debug.Log(raycastResults.Count);

            foreach(var raycastResult in raycastResults)
            {
                if (raycastResult.gameObject.TryGetComponent<T>(out obj))
                    return true;
            }

            obj = default(T);
            return false;
        }
    }
}
