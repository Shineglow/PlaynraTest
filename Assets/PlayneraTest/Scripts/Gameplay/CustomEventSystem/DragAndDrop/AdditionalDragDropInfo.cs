using Assets.PlayneraTest.Scripts.Gameplay.CameraScripts;
using Assets.PlayneraTest.Scripts.General;
using PlayneraTest.Scripts.Gameplay.CameraScripts;
using PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop;
using UnityEngine;

namespace Assets.PlayneraTest.Scripts.Gameplay.CustomEventSystem.DragAndDrop
{
    public struct AdditionalDragDropInfo
    {
        public ICamera Camera;
        public ICameraMovement CameraMovement;
        public IDragDropRegister DragDropRegister;
        public Vector3 PointerStartWorldPosition;
        public Vector3 ItemStartWorldPosition;
        public bool IsDragNow;
    }
}
