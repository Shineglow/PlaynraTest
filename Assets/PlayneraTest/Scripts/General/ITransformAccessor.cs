using UnityEngine;

namespace Assets.PlayneraTest.Scripts.General
{
    public interface ITransformAccessor
    {
        Vector3 Position { get; }
        Quaternion Rotation { get; }
        Vector3 Scale { get; }
    }
}
