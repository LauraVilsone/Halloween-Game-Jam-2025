using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions.FloatExtensions;

namespace Tools.Fade
{
    public class SpriteFade : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        private SpriteRenderer[] _renderers;

        private float _targetOpacity;
        private float _currentOpacity;

        [SerializeField] private float _smoothTime = 0.1f;
        private float _smoothVelocity;

        private void Awake()
        {
            TryGetComponent(out _renderer);
            _renderers = GetComponentsInChildren<SpriteRenderer>(true);
        }

        private void Update()
        {
            if (_currentOpacity != _targetOpacity)
            {
                _currentOpacity = _currentOpacity.SmoothDamp(_currentOpacity, _targetOpacity, ref _smoothVelocity, _smoothTime);
                if (Mathf.Abs(_targetOpacity - _currentOpacity) < .001f)
                {
                    _currentOpacity = _targetOpacity;
                    SetInteractability();
                }
                //_renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, _currentOpacity);
                UpdateRenderers(_currentOpacity);
            }
        }

        public void Show()
        {
            _targetOpacity = 1;
            SetInteractability();
        }

        public void Hide()
        {
            _targetOpacity = 0;
        }

        public void SetOpacity(float opacity)
        {
            _currentOpacity = opacity;
            _targetOpacity = opacity;
            //_renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, opacity);
            UpdateRenderers(_currentOpacity);
            SetInteractability();
        }
        private void UpdateRenderers(float opacity) => UpdateRenderers(opacity, _renderers);
        private void UpdateRenderers(float opacity, SpriteRenderer[] renderers)
        {
            if (renderers == null || renderers.Length == 0) return;
            foreach (var renderer in renderers)
            {
                if (renderer == null) continue;
                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, opacity);
            }
        }
        private void SetInteractability()
        {
            if (_targetOpacity == 0)
                gameObject.SetActive(false);
            else if (_targetOpacity == 1)
                gameObject.SetActive(true);
        }
    }
}