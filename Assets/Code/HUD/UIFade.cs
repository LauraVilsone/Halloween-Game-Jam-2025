using System;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIFade : MonoBehaviour
{
    private CanvasGroup _group;

    private float _targetOpacity;
    private float _currentOpacity;

    private float _smoothTime = 0.1f;
    private float _smoothVelocity;

    public float Opacity => _currentOpacity;
    public Action OnTransitionClear;

    private void Awake()
    {
        TryGetComponent(out _group);
        _currentOpacity = _targetOpacity = _group.alpha;
        SetInteractability();
    }

    private void Update()
    {
        if (_currentOpacity != _targetOpacity)
        {
            _currentOpacity = SmoothDamp(_currentOpacity, _targetOpacity, ref _smoothVelocity, _smoothTime);
            if (Mathf.Abs(_targetOpacity - _currentOpacity) < .001f)
            {
                _currentOpacity = _targetOpacity;
                OnTransitionClear?.Invoke();
                SetInteractability();
            }
            _group.alpha = _currentOpacity;
        }
    }

    public void Show()
    {
        _targetOpacity = 1;
    }

    public void Hide()
    {
        _targetOpacity = 0;
    }

    public void SetOpacity(float opacity)
    {
        _currentOpacity = opacity;
        _targetOpacity = opacity;
        _group.alpha = opacity;
        OnTransitionClear?.Invoke();
        SetInteractability();
    }

    public float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed = Mathf.Infinity)
    {
        // Based on Game Programming Gems 4 Chapter 1.10
        smoothTime = Mathf.Max(0.0001F, smoothTime);
        float omega = 2F / smoothTime;

        float x = omega * Time.unscaledDeltaTime;
        float exp = 1F / (1F + x + 0.48F * x * x + 0.235F * x * x * x);
        float change = current - target;
        float originalTo = target;

        // Clamp maximum speed
        float maxChange = maxSpeed * smoothTime;
        change = Mathf.Clamp(change, -maxChange, maxChange);
        target = current - change;

        float temp = (currentVelocity + omega * change) * Time.unscaledDeltaTime;
        currentVelocity = (currentVelocity - omega * temp) * exp;
        float output = target + (change + temp) * exp;

        // Prevent overshooting
        if (originalTo - current > 0.0F == output > originalTo)
        {
            output = originalTo;
            currentVelocity = (output - originalTo) / Time.unscaledDeltaTime;
        }

        return output;
    }

    private void SetInteractability()
    {
        _group.interactable = _currentOpacity == 1;
    }
}
