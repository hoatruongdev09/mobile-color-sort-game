using System.Collections;
using UnityEngine;

public static class EmptySprite {
    static Sprite instance;

    ///<summary>
    /// Returns the instance of a (1 x 1) white Spprite
    /// </summary>	
    public static Sprite Get () {
        if (instance is null) {
            instance = Resources.Load<Sprite> ("procedural_ui_image_default_sprite");
        }
        return instance;
    }

    public static bool IsEmptySprite (Sprite s) {
        if (Get () == s) {
            return true;
        } else {
            return false;
        }
    }
}