using System;
using UnityEngine;

namespace WerewolfHunt.Mechanics
{
    [Serializable]
    public struct Checkpoint
    {
        public Vector3 position;
        public string scene;

        public Checkpoint(StartLocation location)
        {
            position = location.warpPos;
            scene = location.sceneName;
        }
    }
}
