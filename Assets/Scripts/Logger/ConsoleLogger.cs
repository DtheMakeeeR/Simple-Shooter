using Unity.VisualScripting;
using UnityEngine;

public class ConsoleLogger : LoggerComponent
{
    public override void Log(string message)
    {
        if (!needToLog) return;
        Debug.Log($"<color={color.ToString()}>{groupName}</color>:{message}");
    }
    public override void LogWarning(string message)
    {
        if (!needToLog) return;
        Debug.LogWarning($"<color={color.ToString()}>{groupName}</color>:{message}");

    }
    public override void LogError(string message)
    {
        if (!needToLog) return;
        Debug.LogError($"<color={color.ToString()}>{groupName}</color>:{message}");
    }
}
