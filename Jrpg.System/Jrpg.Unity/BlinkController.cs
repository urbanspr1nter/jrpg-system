using UnityEngine;
using System;

namespace Jrpg.Unity
{
    public class BlinkController
    {
        private static int CountLimit = 5;
        private static float Period = 0.0625f;

        public bool InBlink = false;

        private float FrameTime = 0;
        private int Count = 0;
        private SpriteRenderer Renderer;
        private Color OriginalColor;

        public BlinkController(SpriteRenderer spriteRenderer)
        {
            Renderer = spriteRenderer;
            OriginalColor = Renderer.color;
        }

        public BlinkController(SpriteRenderer spriteRenderer, int numberOfBlinks, float period)
        {
            Renderer = spriteRenderer;
            OriginalColor = Renderer.color;
            CountLimit = numberOfBlinks;
            Period = period;
        }

        public void Tick()
        {
            Blink();
            FrameTime += Time.fixedDeltaTime;
        }

        public void Reset()
        {
            Renderer.forceRenderingOff = false;
            Renderer.color = OriginalColor;
            InBlink = false;
            FrameTime = 0;
            Count = 0;
        }

        private bool ShouldBlink()
        {
            return FrameTime >= Period && Count < CountLimit;
        }

        private bool ShouldReset()
        {
            return Count >= CountLimit;
        }

        private void Blink()
        {
            // We should not blink if the current total frame time has not
            // reached the time specified in Period
            if (!ShouldBlink())
            {
                return;
            }

            // Set the color of the sprite to be a red tint
            Renderer.color = new Color(1.0f, 0, 0, 0.9f);

            // We have reached the period, so now we simulate the flicker.
            if (Count % 2 == 0)
            {
                // disappear!
                Renderer.forceRenderingOff = true;
            }
            else
            {
                // reappear!
                Renderer.forceRenderingOff = false;
            }

            Count++;

            // Reset the FrameTime to begin tracking of the next blink for the
            // new period.
            FrameTime = 0;

            if (ShouldReset())
                Reset();
        }
    }
}
