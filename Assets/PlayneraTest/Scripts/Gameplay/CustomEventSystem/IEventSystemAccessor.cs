using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.PlayneraTest.Scripts.Gameplay.CustomEventSystem
{
    public interface IEventSystemAccessor
    {
        bool TryGetObjectUnderMouse<T>(out T obj);
    }
}
