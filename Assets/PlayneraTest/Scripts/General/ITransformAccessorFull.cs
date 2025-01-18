using UnityEngine;

namespace Assets.PlayneraTest.Scripts.General
{
    public interface ITransformAccessorFull
    {
        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }
        Vector3 Scale { get; set; }
    }
}
