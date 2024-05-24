using UnityEngine;

public class FullScreenButton : MonoBehaviour
{
    public void ToggleFullScreen()
    {
        // V�rifier si le jeu est d�j� en mode plein �cran
        if (Screen.fullScreen)
        {
            // Si oui, quitter le mode plein �cran
            Screen.fullScreen = false;
        }
        else
        {
            // Sinon, activer le mode plein �cran
            Screen.fullScreen = true;
        }
    }
}