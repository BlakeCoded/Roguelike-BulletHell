using UnityEngine;

public static class CursorManager
{
    public static void Lock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Lock Cursor");
    }

    public static void Unlock()
    {
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;

        Debug.Log("Unlock Cursor");
    }
}
