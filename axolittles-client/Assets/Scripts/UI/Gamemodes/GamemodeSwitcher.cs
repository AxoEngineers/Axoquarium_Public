using UnityEngine;

public class GamemodeSwitcher : MonoBehaviour
{
    public AquariumManager _aquariumManager;
    private static bool isChanged;
    public void SwitchToEditMode()
    {
        if (!isChanged)
        {
            _aquariumManager.EnableEditMode();
        }
        else
        {
            _aquariumManager.OpenModalWindow(true);
        }
    }
    
    public void SwitchToPetMode()
    {
        if (!isChanged)
        {
            _aquariumManager.EnablePetMode();
        }
        else
        {
            _aquariumManager.OpenModalWindow(true);
        }
      
    }

    public static void MakeChanges(bool status)
    {
        isChanged = status;
    }

    public static bool GetChangesStatus()
    {
        return isChanged;
    }
}