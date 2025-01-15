using Assets.PlayneraTest.Scripts.Gameplay.CameraScripts;
using Assets.PlayneraTest.Scripts.Gameplay.DragAndDrop;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.PlayneraTest.Scripts
{
    public class GameEntryPoint : MonoBehaviour
    {
        private DragDropSystem _dragDropSystem;
        [SerializeField, Range(0.01f, 0.1f)] private float triggerZone;

        [SerializeField] CameraBase main;
        [SerializeField] private float cameraStartX;
        [SerializeField] private float cameraEndX;
        [SerializeField] private float cameraMovementSpeed;

        [SerializeField] private List<DragDropBase> _dragDropItems = new List<DragDropBase>();

        private void Awake()
        {
            main.Init(cameraStartX, cameraEndX, cameraMovementSpeed);
            _dragDropSystem = new DragDropSystem(main, main, triggerZone);    
        }

        private void Start()
        {
            foreach (var item in _dragDropItems)
            {
                item.Init(_dragDropSystem);
            }
        }
    }
}
