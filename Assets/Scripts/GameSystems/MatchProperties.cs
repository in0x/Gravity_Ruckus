using UnityEngine;

public static class MatchProperties
{
    // Set true when not started through QuickStart, development flag
    public static bool customGame = false;
    //public static int[] playerControllerIDs = new int[4];
    public static int[] playerControllerIDs = { 1, 2, 3, 4 };
    public static PlayerColor[] playerColors = { PlayerColor.blue, PlayerColor.green, PlayerColor.hotpink, PlayerColor.red };

    // Use PlayerColor enums casted to int for indexing (red, green, blue, pink)
    public static Color[] colorValues = { new Color(1, 0, 0, 0.7843137254901f),
                                          new Color(0, 1, 0, 0.7843137254901f),
                                          new Color(0, 0, 1, 0.7843137254901f),
                                          new Color(1, 0.0784f, 0.576f, 0.7843137254901f) };
}
