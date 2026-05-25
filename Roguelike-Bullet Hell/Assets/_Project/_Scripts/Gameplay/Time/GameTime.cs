using UnityEngine;

public static class GameTime
{
    public static float TimeScale { get; set; } = 1f;

    public static float DeltaTime => Time.unscaledDeltaTime * TimeScale;

    public static float UnscaledDeltaTime => Time.unscaledDeltaTime;
}