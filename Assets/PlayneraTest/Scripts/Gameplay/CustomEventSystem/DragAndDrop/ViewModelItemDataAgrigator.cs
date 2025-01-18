using PlayneraTest.Scripts.Gameplay.EventSystem.DragAndDrop;
using PlayneraTest.Scripts.Gameplay.PseudoPhysics;
using UnityEngine;

namespace Assets.PlayneraTest.Scripts.Gameplay.CustomEventSystem.DragAndDrop
{
    public class ViewModelItemDataAgrigator : MonoBehaviour
    {
        [field: SerializeField] public DragDropBase DragDropBase { get; private set; }
        [field: SerializeField] public PseudoPhysicsBody PseudoPhysicsBody { get; private set; }
    }
}
