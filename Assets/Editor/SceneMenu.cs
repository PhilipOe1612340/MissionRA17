using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SceneMenu 
{
   [MenuItem("Scenes/Capsule")]
   public static void OpenCapsule(){
       OpenScene("Capsule");
    }
   
   [MenuItem("Scenes/Navigation")]
   public static void OpenNavigation(){
       OpenScene("Navigation");
    }
   
   [MenuItem("Scenes/Engine")]
   public static void OpenEngine(){
       OpenScene("Engine");
    }

   [MenuItem("Scenes/Storage")]
   public static void OpenStorage(){
       OpenScene("Storage");
    }

   private static void OpenScene(string scene){
       EditorSceneManager.OpenScene("Assets/Scenes/Always_Active.unity", OpenSceneMode.Single);
       EditorSceneManager.OpenScene("Assets/Scenes/" + scene + ".unity", OpenSceneMode.Additive);
    }

    static string GetScenePathOrThrow (Object obj){
		string path = AssetDatabase.GetAssetPath(obj);
		if (!path.EndsWith(".unity"))
			throw new System.Exception("You must select a set of scenes to multi bake them");
		return path;
	}
	
	[MenuItem ("Scenes/Bake Selected Scenes")]
	public static void Bake(){
		var paths = new List<string>();
		paths.Add(GetScenePathOrThrow(Selection.activeObject));
		foreach(var obj in Selection.objects)
		{
			string path = GetScenePathOrThrow(obj);
			if (path != paths[0])
				paths.Add(path);
		}

        Debug.Log("selected: " + paths);
		
        foreach(string scene in paths){
            EditorSceneManager.OpenScene("Assets/Scenes/Always_Active.unity", OpenSceneMode.Single);
            var main = EditorSceneManager.OpenScene(scene, OpenSceneMode.Additive);
            EditorSceneManager.SetActiveScene(main);
    		Lightmapping.Bake();
            Debug.Log("backed " + scene);
        }
	}






}
