using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManager;
public static class Loader 
{

    public enum Scene
    {
        Fathi,
        Sandbox,

    }
   public static void Load(Scene scene)
   {
        SceneManager.LoadScene(scence.ToString());
   }
}
