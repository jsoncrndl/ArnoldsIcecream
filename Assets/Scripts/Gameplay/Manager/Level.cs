using UnityEngine;

namespace WerewolfHunt.Manager
{
    public static class Level 
    {
        public static int getNextLevelXP(int currentLevel)
        {
            float x = ((currentLevel + 81) - 92) * 0.0002f;
            return Mathf.FloorToInt(((x < 0 ? 0 : x + 0.001f) * Mathf.Pow(currentLevel + 81, 2)) + 0.01f);
        }
    }
}
