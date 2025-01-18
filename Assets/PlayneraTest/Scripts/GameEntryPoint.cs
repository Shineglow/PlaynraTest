using System.Collections.Generic;
using Assets.PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop;
using Assets.PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop.DragDropActions;
using PlayneraTest.Scripts.Gameplay.CameraScripts;
using PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop;
using PlayneraTest.Scripts.Gameplay.EventSystem.SurfaceSystem;
using PlayneraTest.Scripts.Gameplay.PseudoPhysics;
using UnityEngine;

namespace PlayneraTest.Scripts
{
    public class GameEntryPoint : MonoBehaviour
    {
        private DragDropActionsSystem _dragDropSystem;
        private CameraSlideSystem _cameraSlideSystem;
        private PseudoPhysicsProcessor _pseudoPhysicsProcessor;
        private SurfaceDetectionSystem _surfaceDetectionSystem;
        [SerializeField, Range(0.01f, 0.1f)] private float triggerZone;

        
        [SerializeField] private CameraBase main;
        [SerializeField] private float cameraStartX;
        [SerializeField] private float cameraEndX;
        [SerializeField] private float cameraMovementSpeed;

        [SerializeField] private List<DragDropBase> dragDropItemsStatic = new List<DragDropBase>();
        [SerializeField] private List<DragDropBase> dragDropItemsDynamic = new List<DragDropBase>();
        [SerializeField] private List<PseudoPhysicsBody> pseudoPhysicsBodies = new List<PseudoPhysicsBody>();
        [SerializeField] private List<SurfacePointerListener> surfacePointerListeners = new List<SurfacePointerListener>();

        private void Awake()
        {
            main.Init(cameraStartX, cameraEndX, cameraMovementSpeed);
            _dragDropSystem = new DragDropActionsSystem(
                main,
                main,
                new() { new ScaleUpOnDragStart(this) },
                new() { new MoveOnDrag() },
                new() { new ScaleDownOnDragEnd(this) },
                triggerZone);
            _cameraSlideSystem = new CameraSlideSystem(main, main);
            _pseudoPhysicsProcessor = new PseudoPhysicsProcessor();
            _surfaceDetectionSystem = new();
        }

        private void Start()
        {
            foreach (var item in dragDropItemsStatic)
            {
                item.Init(_cameraSlideSystem);
            }
            
            foreach (var item in dragDropItemsDynamic)
            {
                item.Init(_dragDropSystem);
            }

            foreach (var pseudoPhysicsBody in pseudoPhysicsBodies)
            {
                _pseudoPhysicsProcessor.Add(pseudoPhysicsBody);
            }

            foreach (var surfacePointerListener in surfacePointerListeners)
            {
                surfacePointerListener.Init(_surfaceDetectionSystem);
            }
        }
    }
}
