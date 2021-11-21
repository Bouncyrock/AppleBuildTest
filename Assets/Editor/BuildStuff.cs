using System.IO;
using UnityEditor;
using UnityEditor.OSXStandalone;

public static class BuildStuff
{
    static string _lastPath = "";

    [MenuItem("BUILD TEST/Build")]
    static void BuildIt()
    {
        UserBuildSettings.architecture = MacOSArchitecture.ARM64;

        var path = EditorUtility.SaveFolderPanel("Build location", _lastPath, "test");

        if (!string.IsNullOrEmpty(path))
        {
            _lastPath = path;
            BuildPipeline.BuildPlayer(new BuildPlayerOptions()
            {
                locationPathName = Path.Combine(path, "test"),
                scenes = new [] { "Assets/Scenes/SampleScene.unity" },
                target = BuildTarget.StandaloneOSX,
                options = BuildOptions.Development,
                extraScriptingDefines = null
            });
        }
    }
}
