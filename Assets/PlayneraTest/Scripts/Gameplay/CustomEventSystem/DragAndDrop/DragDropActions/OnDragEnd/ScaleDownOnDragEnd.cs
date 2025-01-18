using Assets.PlayneraTest.Scripts.General;
using PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.PlayneraTest.Scripts.Gameplay.CustomEventSystem.DragAndDrop.DragDropActions
{
    public class ScaleDownOnDragEnd : DragDropAbstractAction
    {
        private const float SPEED = 0.1f;
        private Vector3 TargetScale { get; } = Vector3.one * 0.1f;

        private MonoBehaviour _coroutineHolder;
        private ITransformAccessorFull _transformAccessorFull;
        private IDragDropRegister _dragDropRegister;
        private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

        public ScaleDownOnDragEnd(MonoBehaviour coroutineHolder)
        {
            _coroutineHolder = coroutineHolder;
        }

        public override void DoAction(IDragDropItem dragDropItem, PointerEventData eventData, AdditionalDragDropInfo additionalDragDropInfo)
        {
            _transformAccessorFull = dragDropItem;
            _dragDropRegister = additionalDragDropInfo.DragDropRegister;
            _coroutineHolder.StartCoroutine(ScaleOverTime());
        }

        private IEnumerator ScaleOverTime() 
        {
            while(_transformAccessorFull.Scale != TargetScale && !_dragDropRegister.IsHoldNow)
            {
                _transformAccessorFull.Scale -= Vector3.one * SPEED * Time.deltaTime;

                if (_transformAccessorFull.Scale.x < TargetScale.x)
                {
                    var tmpScale = _transformAccessorFull.Scale;
                    tmpScale.x = TargetScale.x;
                    _transformAccessorFull.Scale = tmpScale;
                }
                if (_transformAccessorFull.Scale.y < TargetScale.y)
                {
                    var tmpScale = _transformAccessorFull.Scale;
                    tmpScale.y = TargetScale.y;
                    _transformAccessorFull.Scale = tmpScale;
                }
                if (_transformAccessorFull.Scale.z < TargetScale.z)
                {
                    var tmpScale = _transformAccessorFull.Scale;
                    tmpScale.z = TargetScale.z;
                    _transformAccessorFull.Scale = tmpScale;
                }

                yield return waitForEndOfFrame;
            }
        }
    }
}
