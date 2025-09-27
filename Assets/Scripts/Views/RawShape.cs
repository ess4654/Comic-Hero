using Shapes2D;
using System;
using UnityEngine;

namespace ComicHero.Views.UI
{
    /// <summary>
    ///     Every frame it updates the shape texture to use the raw texture from the camera.
    /// </summary>
    [RequireComponent(typeof(Shape))]
    public class RawShape : MonoBehaviour
    {
        [SerializeField] private Texture rawTexture;
        [SerializeField] private Camera renderCam;

        private Shape shape;

        private void Awake()
        {
            shape = GetComponent<Shape>();
        }

        private void OnValidate()
        {
            shape = GetComponent<Shape>();
            shape.settings.fillType = FillType.Texture;
            UpdateFromRawTexture(null);
        }

        private void LateUpdate() => UpdateFromRawTexture(renderCam);

        private void OnEnable()
        {
            Camera.onPreRender += UpdateFromRawTexture;
        }

        private void OnDisable()
        {
            Camera.onPreRender -= UpdateFromRawTexture;
        }

        private void UpdateFromRawTexture(Camera camera)
        {
            if (rawTexture != null)
            {
                //var texture = rawTexture.ToTexture2D();
                //shape.settings.fillType = FillType.Texture;
                //shape.DrawToTexture2D()

                shape.settings.fillType = FillType.Texture;
                shape.settings.fillTexture = shape.DrawToTexture2D(camera, 1600, 900);
            }
        }
    }
}