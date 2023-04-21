/* v1.0 170603
* For exporting packages.
* For any scene in the editor window, rightclick and choose "select dependencies"
* Now, with all dependent objects selected, go to Main Menu > Edit > Bulk Copy...
* Items to copy will be listed. Hit the "Copy" button and choose target folder.
* All items will be copied to that folder.
*/
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Text;

public class BulkCopyAssets : ScriptableWizard
{

    static private string targetPath = @"C:\Temp";
    static private List<string> objectPaths;

    [MenuItem("Edit/Bulk Copy...")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard("Bulk Copy Assets", typeof(BulkCopyAssets), "Copy");
    }

    void OnEnable()
    {
        UpdateSelectionHelper();
    }

    void OnSelectionChange()
    {
        UpdateSelectionHelper();
    }

    void UpdateSelectionHelper()
    {
        helpString = (Selection.objects != null) ?
            string.Concat(
                "Number of objects selected: " + Selection.objects.Length + System.Environment.NewLine
                , GetObjNamesString(Selection.objects)
            )
            : "";
        objectPaths = GetObjNamesArray(Selection.objects);
    }

    List<string> GetObjNamesArray(Object[] selectionObjects)
    {
        string assetBasePath = Application.dataPath;
        string fixedPath, assetPath;
        List<string> result = new List<string>();

        for (int i = 0; i < selectionObjects.Length; i++)
        {
            assetPath = AssetDatabase.GetAssetPath(selectionObjects[i]);
            fixedPath = string.Concat(
                    assetBasePath
                    , @"\"
                    , assetPath.Remove(
                        0
                        , 7
                    )
                ).Replace(@"/", @"\");

            result.Add(fixedPath);
            Debug.Log(result[i]);
        }
        return result;
    }

    string GetObjNamesString(Object[] selectionObjects)
    {
        List<string> result = new List<string>();
        //for (int i = 0; i < selectionObjects.Length; i++) result.Add(selectionObjects[i].name);
        return string.Join(System.Environment.NewLine, result.ToArray());
    }

    void OnWizardCreate()
    {

        if (Selection.objects == null)
            return;

        targetPath = EditorUtility.OpenFolderPanel(
            "Target Path"
            , ""
            , ""
        ).Replace(@"/", @"\");

        if (string.IsNullOrEmpty(targetPath) || !Directory.Exists(targetPath)) return;
        Debug.Log("Targetpath = " + targetPath);

        for (int i = 0; i < objectPaths.Count; i++)
        {
            if ((File.GetAttributes(objectPaths[i]) & FileAttributes.Directory) != FileAttributes.Directory)
            {
                string sourceFile, targetFile;

                sourceFile = objectPaths[i];
                targetFile = Path.Combine(
                    targetPath
                    , Path.GetFileName(objectPaths[i])
                );

                // copy main file
                File.Copy(
                    sourceFile
                    , targetFile
                    , true
                );

                // and the .meta file
                File.Copy(
                    string.Concat(sourceFile, ".meta")
                    , string.Concat(targetFile, ".meta")
                    , true
                );
            }
        }
    }
}
