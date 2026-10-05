using System.IO;
using UnityEditor;
using UnityEngine;

public class NamespaceBatchAdder : EditorWindow
{
    private string targetNamespace = "YourProjectNamespace";

    [MenuItem("Tools/Add Namespace to All Scripts")]
    public static void ShowWindow()
    {
        GetWindow<NamespaceBatchAdder>("Batch Namespace Adder");
    }

    private void OnGUI()
    {
        targetNamespace = EditorGUILayout.TextField("Namespace Name", targetNamespace);

        if (GUILayout.Button("Apply to All Scripts"))
        {
            if (EditorUtility.DisplayDialog("Confirm", "This will modify all C# scripts in your Assets folder. Proceed?", "Yes", "No"))
            {
                AddNamespaceToAll();
            }
        }
    }

    private void AddNamespaceToAll()
    {
        // Find all C# script files in Assets, excluding Editor scripts
        string[] filePaths = Directory.GetFiles(Application.dataPath + "/Scripts" + "/PlayerSystem", "*.cs", SearchOption.AllDirectories);

        foreach (string path in filePaths)
        {
            if (path.Contains("/Editor/")) continue; // Skip editor scripts

            string fileContent = File.ReadAllText(path);

            // Skip if the file already contains a namespace definition
            if (fileContent.Contains("namespace ")) continue;

            // Simple injection: Wrap the class/interfaces in a namespace block
            // Note: This assumes standard formatting where code starts directly with usable components
            string newContent = $"namespace {targetNamespace}\n{{\n{fileContent}\n}}";

            File.WriteAllText(path, newContent);
        }

        AssetDatabase.Refresh();
        Debug.Log("Namespaces added successfully!");
    }
}
