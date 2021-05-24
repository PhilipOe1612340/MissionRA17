using UnityEditor;
using UnityEditor.SceneManagement;


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



}
