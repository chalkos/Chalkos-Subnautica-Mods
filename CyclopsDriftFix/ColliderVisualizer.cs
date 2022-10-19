using System;

#if BZ
using TMPro;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace CyclopsDriftFix
{
    public class ColliderVisualizer : MonoBehaviour
    {
        private static readonly Vector2 ReferenceResolution = new Vector2(800f, 600f);
        private static readonly Color Color = new Color32(255, 0, 0, 200);

        private GameObject _visualizer;
#if SN1
        private Text _label;
#elif BZ
        private TextMeshProUGUI _label;
#endif
        private static GameObject _colliderVisualizerCanvas;

        private static GameObject ColliderVisualizerCanvas
        {
            get
            {
                if (_colliderVisualizerCanvas == null)
                {
                    _colliderVisualizerCanvas = new GameObject("ColliderVisualizerCanvas");
                    var canvas = _colliderVisualizerCanvas.AddComponent<Canvas>();
                    canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                    var canvasScaler = _colliderVisualizerCanvas.AddComponent<CanvasScaler>();
                    canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                    canvasScaler.referenceResolution = ReferenceResolution;
                    canvasScaler.matchWidthOrHeight = 1f;
                    _colliderVisualizerCanvas.AddComponent<GraphicRaycaster>();
                }

                return _colliderVisualizerCanvas;
            }
        }

        private void LateUpdate()
        {
            if (_visualizer == null || _label == null) return;

            _label.rectTransform.position =
                RectTransformUtility.WorldToScreenPoint(Camera.main, _visualizer.transform.position);
        }

        private void OnDestroy()
        {
            if (_label == null) return;

            Destroy(_label.gameObject);
            Destroy(_visualizer);
        }

        private void OnEnable()
        {
            if (_label && _label.gameObject)
                _label.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            if (_label && _label.gameObject)
                _label.gameObject.SetActive(false);
        }

        public void Initialize(Collider targetCollider, string labelMessage, int fontSize)
        {
            switch (targetCollider)
            {
                case BoxCollider c:
                    _visualizer = CreateVisualizer(PrimitiveType.Cube);
                    SetVisualizerTransform(c);
                    break;
                case CapsuleCollider c:
                    _visualizer = CreateVisualizer(PrimitiveType.Sphere);
                    SetVisualizerTransform(c);
                    break;
                case SphereCollider c:
                    _visualizer = CreateVisualizer(PrimitiveType.Capsule);
                    SetVisualizerTransform(c);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(targetCollider), targetCollider,
                        $"Unsupported collider type: {targetCollider.GetType().FullName}");
            }

            var material = _visualizer.GetComponent<Renderer>().material;
            // material.shader = Shader.Find("Sprites/Default");
            material.color = Color;

            CreateLabel(labelMessage, fontSize);
        }

        private GameObject CreateVisualizer(PrimitiveType primitiveType)
        {
            GameObject visualizer = GameObject.CreatePrimitive(primitiveType);
            visualizer.name = "ColliderDebug";
            visualizer.transform.SetParent(transform, worldPositionStays: false);

            // remove since it's not needed, we already have the mesh
            var visibleCollider = visualizer.GetComponent<Collider>();
            visibleCollider.enabled = false;
            Destroy(visibleCollider);

            return visualizer;
        }

        private void SetVisualizerTransform(BoxCollider boxCollider)
        {
            var visualizerTransform = _visualizer.transform;
            visualizerTransform.localPosition += boxCollider.center;
            visualizerTransform.localScale = Vector3.Scale(visualizerTransform.localScale, boxCollider.size);
        }

        private void SetVisualizerTransform(SphereCollider sphereCollider)
        {
            Transform visualizerTransform = _visualizer.transform;
            visualizerTransform.localPosition += sphereCollider.center;
            visualizerTransform.localScale *= sphereCollider.radius * 2f;
        }

        private void SetVisualizerTransform(CapsuleCollider capsuleCollider)
        {
            Transform visualizerTransform = _visualizer.transform;
            visualizerTransform.localPosition += capsuleCollider.center;

            switch (capsuleCollider.direction)
            {
                // X-Axis
                case 0:
                    visualizerTransform.Rotate(Vector3.forward * 90f);
                    break;

                // Y-Axis
                case 1:
                    break;

                // Z-Axis
                case 2:
                    visualizerTransform.Rotate(Vector3.right * 90f);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            Vector3 capsuleLocalScale = visualizerTransform.localScale;
            float radius = capsuleCollider.radius;
            float newCapsuleLocalScaleX = capsuleLocalScale.x * radius * 2f;
            float newCapsuleLocalScaleY = capsuleLocalScale.y * capsuleCollider.height * 0.5f;
            float newCapsuleLocalScaleZ = capsuleLocalScale.z * radius * 2f;
            visualizerTransform.localScale =
                new Vector3(newCapsuleLocalScaleX, newCapsuleLocalScaleY, newCapsuleLocalScaleZ);
        }

        private void CreateLabel(string message, int fontSize)
        {
            var label = new GameObject("Label");
            label.transform.SetParent(ColliderVisualizerCanvas.transform, worldPositionStays: false);
#if SN1
            _label = label.AddComponent<Text>();
            _label.font = HandReticle.main.useSecondaryText.font;
            _label.alignment = TextAnchor.MiddleCenter;
#elif BZ
            _label = label.AddComponent<TextMeshProUGUI>();
            _label.font = HandReticle.main.compTextUseSubscript.font;
            _label.alignment = TextAlignmentOptions.Center;
#endif
            _label.fontSize = fontSize;
            _label.raycastTarget = false;
            _label.text = message;

            var contentSizeFitter = label.AddComponent<ContentSizeFitter>();
            contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            label.SetActive(gameObject.activeSelf);
        }
    }
}