namespace Extensions.FloatExtensions
{
    using UnityEngine;

    static class FloatExtensions
    {
        public static float SmoothDamp(this float value, float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed = Mathf.Infinity)
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

            //value = output;
            return output;
        }
    }
}