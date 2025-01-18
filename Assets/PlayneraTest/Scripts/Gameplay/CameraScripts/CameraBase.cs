using Assets.PlayneraTest.Scripts.Gameplay.CameraScripts;
using Assets.PlayneraTest.Scripts.General;
using UnityEngine;
using UnityEngine.UIElements;

namespace PlayneraTest.Scripts.Gameplay.CameraScripts
{
    public class CameraBase : MonoBehaviour, ICameraMovement, ICamera, ITransformAccessorFull
    {
        [SerializeField] private Camera _camera;

        private float _minPosX;
        private float _maxPosX;
        [SerializeField] private float _movementSpeed;
        private Vector3 _direction;

        public Vector3 Position
        {
            get => _camera.transform.position; 
            set
            {
                value.x = Mathf.Clamp(value.x, _minPosX, _maxPosX);
                _camera.transform.position = value; 
            }
        }
        public Quaternion Rotation
        {
            get => _camera.transform.rotation; set => _camera.transform.rotation = value;
        }
        public Vector3 Scale {
            get => _camera.transform.localScale; set => _camera.transform.localScale = value;
        }

        public Vector3 Forward
        {
            get => _camera.transform.forward;
        }

        public void Init(float minPosX, float maxPosX, float movemntSpeed)
        {
            _minPosX = minPosX;
            _maxPosX = maxPosX;
            _movementSpeed = movemntSpeed;
        }

        private void Update()
        {
            if(_direction.x != 0)
            {
                /* var newPos = Position + _direction * (_movemntSpeed * Time.deltaTime);
                newPos.x = Mathf.Clamp(Position.x, _minPosX, _maxPosX);
                Position = newPos;

                _direction.x = 0f; */
            }
        }

        public void SlowlyMoveToTheLeft()
        {
            if(Position.x > _minPosX)
            {
                _direction.x = -1f;
            }
        }

        public void SlowlyMoveToTheRight()
        {
            if (Position.x < _maxPosX)
            {
                _direction.x = 1f;
            }
        }

        public Ray ViewportPointToRay(Vector3 pos)
        {
            return _camera.ViewportPointToRay(pos);
        }

        public Vector3 ScreenToWorldPoint(Vector3 pos)
        {
            return _camera.ScreenToWorldPoint(pos);
        }

        public Vector3 ScreenToViewportPoint(Vector3 pos)
        {
            return _camera.ScreenToViewportPoint(pos);
        }
    }
}
