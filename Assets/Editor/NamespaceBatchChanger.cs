using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class NamespaceBatchChanger : EditorWindow
{
    private string oldNamespace = "OldNamespaceName";
    private string targetNamespace = "NewNamespaceName";
    private string selectedFolder = "Assets/Scripts/PlayerSystem"; // Default starting path

    [MenuItem("Tools/Batch Namespace Changer")]
    public static void ShowWindow()
    {
        GetWindow<NamespaceBatchChanger>("Batch Namespace Changer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Namespace Settings", EditorStyles.boldLabel);

        // 1. Where you type the namespace you want to REMOVE/REPLACE
        oldNamespace = EditorGUILayout.TextField("Old Namespace (To Replace)", oldNamespace);

        // 2. Where you type your NEW namespace
        targetNamespace = EditorGUILayout.TextField("New Namespace (Target)", targetNamespace);

        GUILayout.Space(10);
        GUILayout.Label("Target Folder Settings", EditorStyles.boldLabel);

        // Display current target path
        EditorGUILayout.LabelField("Target Folder:", selectedFolder, EditorStyles.wordWrappedLabel);

        // 3. The button to change the directory/folder interactively
        if (GUILayout.Button("Select Target Folder"))
        {
            // Opens a native folder selection window starting inside your project's Assets folder
            string absolutePath = EditorUtility.OpenFolderPanel("Select Folder to Update Scripts", Application.dataPath, "");

            if (!string.IsNullOrEmpty(absolutePath))
            {
                // Make the path relative to the project so it looks clean (e.g., Assets/Scripts/...)
                if (absolutePath.StartsWith(Application.dataPath))
                {
                    selectedFolder = "Assets" + absolutePath.Substring(Application.dataPath.Length);
                }
                else
                {
                    // Fallback to absolute path if a folder outside Assets is chosen
                    selectedFolder = absolutePath;
                }
            }
        }

        GUILayout.Space(20);

        if (GUILayout.Button("Update Namespaces Now", GUILayout.Height(30)))
        {
            // Convert back to full path for system file operations
            string fullPath = Application.dataPath + selectedFolder.Substring(6);
            if (selectedFolder.StartsWith("Assets"))
            {
                fullPath = Path.Combine(Application.dataPath, selectedFolder.Substring(7));
            }
            else
            {
                fullPath = selectedFolder;
            }

            if (!Directory.Exists(fullPath))
            {
                EditorUtility.DisplayDialog("Error", "The selected directory does not exist!", "OK");
                return;
            }

            if (EditorUtility.DisplayDialog("Confirm Change", $"This will replace 'namespace {oldNamespace}' with 'namespace {targetNamespace}' inside:\n\n{selectedFolder}\n\nProceed?", "Yes", "No"))
            {
                ChangeSpecificNamespace(fullPath);
            }
        }
    }

    private void ChangeSpecificNamespace(string folderPath)
    {
        string[] filePaths = Directory.GetFiles(folderPath, "*.cs", SearchOption.AllDirectories);
        int changedCount = 0;

        // Matches exactly the old namespace string layout (handles varied spacing)
        // Escapes any dots in the namespace name automatically
        string escapedOldNamespace = Regex.Escape(oldNamespace);
        string pattern = $@"namespace\s+{escapedOldNamespace}\b";

        foreach (string path in filePaths)
        {
            if (path.Contains("/Editor/")) continue; // Skip editor utilities

            string fileContent = File.ReadAllText(path);

            if (Regex.IsMatch(fileContent, pattern))
            {
                // Replaces the specific targeted old namespace with the new one
                string newContent = Regex.Replace(fileContent, pattern, $"namespace {targetNamespace}");

                File.WriteAllText(path, newContent);
                changedCount++;
            }
        }

        AssetDatabase.Refresh();
        Debug.Log($"Successfully updated {changedCount} scripts from '{oldNamespace}' to '{targetNamespace}'!");
    }
}
