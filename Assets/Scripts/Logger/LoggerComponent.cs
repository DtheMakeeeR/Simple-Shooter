using UnityEngine;

public abstract class LoggerComponent: MonoBehaviour
{
    public enum LogColor
    {   
        red,
        green,
        blue, 
        yellow,
        white
    }
    [SerializeField] protected bool needToLog = true;
    [SerializeField] protected string groupName = string.Empty;
    [SerializeField] protected LogColor color = LogColor.white;

    public abstract void Log(string message);
    public abstract void LogWarning(string message);
    public abstract void LogError(string message);
}
