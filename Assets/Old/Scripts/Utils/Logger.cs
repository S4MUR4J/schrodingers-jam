using System.Runtime.CompilerServices;

namespace Utils
{
    public static class Logger
    {
        public static void Log(
            object message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0
        )
        {
            UnityEngine.Debug.Log(
                FormatMessage(
                    message: message,
                    memberName: memberName,
                    filePath: filePath,
                    lineNumber: lineNumber
                )
            );
        }

        public static void Warning(
            object message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0
        )
        {
            UnityEngine.Debug.LogWarning(
                FormatMessage(
                    message: message,
                    memberName: memberName,
                    filePath: filePath,
                    lineNumber: lineNumber
                )
            );
        }

        public static void Error(
            object message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0
        )
        {
            UnityEngine.Debug.LogError(
                FormatMessage(
                    message: message,
                    memberName: memberName,
                    filePath: filePath,
                    lineNumber: lineNumber
                )
            );
        }

        private static object FormatMessage(
            object message,
            string memberName,
            string filePath,
            int lineNumber
        )
        {
            return $"{GetClassName(filePath)}::{memberName}::{lineNumber} - {message}";
        }

        private static string GetClassName(string filePath)
        {
            return System.IO.Path.GetFileNameWithoutExtension(filePath);
        }
    }
}
