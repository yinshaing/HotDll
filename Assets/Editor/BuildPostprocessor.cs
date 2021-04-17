using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class BuildPostprocessor  {

    [PostProcessBuildAttribute(100)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        if (target == BuildTarget.Android && (!pathToBuiltProject.EndsWith(".apk")))
        {
            Debug.Log("target: " + target.ToString());
            Debug.Log("pathToBuiltProject: " + pathToBuiltProject);
            Debug.Log("productName: " + PlayerSettings.productName);
            //DLL在android工程中对应的位置   
            string dllPath = pathToBuiltProject + "/" + PlayerSettings.productName + "/src/main/assets/bin/Data/Managed/Assembly-CSharp.dll";

            if (File.Exists(dllPath))
            {
                //先读取没有加密的dll    
                byte[] bytes = File.ReadAllBytes(dllPath);
                //字节偏移 DLL就加密了。    
                bytes[0] += 1;
                //在写到原本的位置上    
                File.WriteAllBytes(dllPath, bytes);
                Debug.Log("Encrypt Assembly-CSharp.dll Success");
            }
            else
            {
                Debug.LogError(dllPath + "  Not Found!!");
            }
        }
    }
}
