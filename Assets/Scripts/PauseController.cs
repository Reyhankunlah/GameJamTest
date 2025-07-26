using UnityEngine;

public static class PauseController
{
    private static bool isGamePaused = false;

    public static bool IsGamePaused => isGamePaused;

    public static void setPause(bool pause)
    {
        isGamePaused = pause;

        Time.timeScale = pause ? 0f : 1f; // Pause semua animasi dan physics
        // Bisa juga tambahkan: AudioListener.pause = pause;
    }
}
