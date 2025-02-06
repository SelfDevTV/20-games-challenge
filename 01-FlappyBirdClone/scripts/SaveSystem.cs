

using Godot;


public static class SaveSystem
{
    public static string SaveFilePath = "user://savegame.save";
    public static void Save(Variant variable)
    {
        FileAccess file = FileAccess.Open(SaveFilePath, FileAccess.ModeFlags.Write);
        file.StoreVar(variable);
        file.Close();
    }

    public static int Load()
    {
        if (FileAccess.FileExists(SaveFilePath))
        {
            FileAccess file = FileAccess.Open(SaveFilePath, FileAccess.ModeFlags.Read);
            Variant variable = file.GetVar();
            file.Close();

            return (int)variable;
        }
        else
        {
            return 0;
        }
    }
}