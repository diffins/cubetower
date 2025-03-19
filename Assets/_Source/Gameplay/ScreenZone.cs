using _Source.Enums;
using UnityEngine;

public static class ScreenZone
{
    public static ScreenZoneType CheckPosition(Vector3 position)
    {
        var screenPosition = Camera.main.WorldToScreenPoint(position);

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        if (screenPosition.x < 0 || screenPosition.x > screenWidth ||
            screenPosition.y < 0 || screenPosition.y > screenHeight)
        {
            return ScreenZoneType.OutOfScreen;
        }
        else
        {
            if (screenPosition.x < screenWidth / 2)
            {
                return ScreenZoneType.DestroyZone;
            }
            else
            {
                return ScreenZoneType.TowerZone;
            }
        }
    }
}
