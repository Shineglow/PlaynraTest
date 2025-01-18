using UnityEngine.EventSystems;

namespace Assets.PlayneraTest.Scripts.Gameplay.CustomEventSystem
{
    public interface IDragDropComplex : IBeginDragHandler, IDragHandler, IEndDragHandler
    {
    }
}
