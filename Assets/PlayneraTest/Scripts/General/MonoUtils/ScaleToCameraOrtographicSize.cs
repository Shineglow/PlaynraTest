using System;
using UnityEngine;

public class ScaleToCameraOrtographicSize : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private Transform target;

    private void OnValidate()
    {
        SetCamera(camera == null ? Camera.main : camera);
    }

    private void Awake()
    {
        target ??= transform;
        SetCamera(camera == null ? Camera.main : camera);
    }

    public void SetCamera(Camera camera)
    {
        this.camera = camera;

        var width = camera.orthographicSize*2;
        var aspect = camera.aspect;
        var height = aspect * width;
        target.localScale = new Vector3(height, width, 0);
    }
}
