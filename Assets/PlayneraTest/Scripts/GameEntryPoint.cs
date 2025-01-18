using System.Collections.Generic;
using Assets.PlayneraTest.Scripts.Gameplay.CustomEventSystem;
using Assets.PlayneraTest.Scripts.Gameplay.CustomEventSystem.DragAndDrop;
using Assets.PlayneraTest.Scripts.Gameplay.CustomEventSystem.DragAndDrop.DragDropActions;
using PlayneraTest.Scripts.Gameplay.CameraScripts;
using PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop;
using PlayneraTest.Scripts.Gameplay.PseudoPhysics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayneraTest.Scripts
{
    public class GameEntryPoint : MonoBehaviour
    {
        private DragDropActionsSystem _dragDropSystem;
        private CameraSlideSystem _cameraSlideSystem;
        private PseudoPhysicsProcessor _pseudoPhysicsProcessor;
        private IEventSystemAccessor _eventSytemAccesor;
        [SerializeField, Range(0.01f, 0.1f)] private float triggerZone;

        
        [SerializeField] private CameraBase main;
        [SerializeField] private float cameraStartX;
        [SerializeField] private float cameraEndX;
        [SerializeField] private float cameraMovementSpeed;

        [SerializeField] private List<DragDropBase> dragDropItemsStatic = new List<DragDropBase>();
        [SerializeField] private List<DragDropBase> dragDropItemsDynamic = new List<DragDropBase>();
        [SerializeField] private List<PseudoPhysicsBody> pseudoPhysicsBodies = new List<PseudoPhysicsBody>();

        [SerializeField] private EventSystem eventSystem;

        private void Awake()
        {
            main.Init(cameraStartX, cameraEndX, cameraMovementSpeed);
            _eventSytemAccesor = new EventSystemWraper(eventSystem);
            _dragDropSystem = new DragDropActionsSystem(
                main,
                main,
                _eventSytemAccesor,
                new() { new ScaleUpOnDragStart(this) },
                new() { new MoveOnDrag() },
                new() { new ScaleDownOnDragEnd(this) },
                triggerZone);
            _cameraSlideSystem = new CameraSlideSystem( main, main );
            _pseudoPhysicsProcessor = new PseudoPhysicsProcessor(_dragDropSystem, _eventSytemAccesor);
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
        }

        private void FixedUpdate()
        {
            _pseudoPhysicsProcessor.FixedUpdate();
        }
    }
}
