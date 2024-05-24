using UnityEngine;

public class FullScreenButton : MonoBehaviour
{
    public void ToggleFullScreen()
    {
        // Vérifier si le jeu est déjà en mode plein écran
        if (Screen.fullScreen)
        {
            // Si oui, quitter le mode plein écran
            Screen.fullScreen = false;
        }
        else
        {
            // Sinon, activer le mode plein écran
            Screen.fullScreen = true;
        }
    }
}