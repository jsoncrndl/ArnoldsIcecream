#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;

namespace WerewolfHunt.Mechanics
{
    [CreateAssetMenu(fileName ="startlocation", menuName = "Custom/Start Location")]
    public class StartLocation : ScriptableObject
    {
        public string sceneName;
        [HideInInspector] public Vector3 warpPos;

        public static StartLocation FromCheckpoint(Checkpoint checkpoint)
        {
            StartLocation startLocation = CreateInstance<StartLocation>();
            startLocation.warpPos = checkpoint.position;
            startLocation.sceneName = checkpoint.scene;
            return startLocation;
        }

        public void SetLocation(string scene, Vector3 pos)
        {
#if UNITY_EDITOR
            SerializedObject serializedObject = new SerializedObject(this);
            serializedObject.FindProperty("sceneName").stringValue = scene;
            serializedObject.FindProperty("warpPos").vector3Value = pos;
#else
            sceneName = scene;
            warpPos = pos;
#endif
        }


#if UNITY_EDITOR
        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            StartLocation location = EditorUtility.InstanceIDToObject(instanceID) as StartLocation;
            
            if (location == null || string.IsNullOrEmpty(location.sceneName)) return false;
            
            EditorSceneManager.OpenScene("Assets/_Scenes/" + location.sceneName + ".unity");
            return true;
            

            
        }
#endif
    }
}