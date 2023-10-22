using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Frame
{
    public enum LogDomain
    {
        None, All,
        Driver,
        Manager,
        Collider,
        Quadtree
    }

    public class Debugger : MonoSingleton<Debugger>
    {
        [ShowInInspector]
        [ValueDropdown(nameof(GetEnumValues), IsUniqueList = true)]
        public static HashSet<LogDomain> logDict = new(new LogDomain[] { LogDomain.All });
        private IEnumerable<LogDomain> GetEnumValues()
        {
            // 生成所有可选项
            return System.Enum.GetValues(typeof(LogDomain)) as LogDomain[];
        }

        public static void Log(string msg, LogDomain domain = LogDomain.All)
        {
            if (logDict.Count == 0)
            {
                return;
            }

            if (logDict.Contains(LogDomain.All))
            {
                Debug.Log($"<b><color=#008AFF>[{domain}]  </color></b>{msg}");
            }
            else if (logDict.Contains(domain))
            {
                Debug.Log($"<b><color=#45FFE0>[{domain}]  </color></b>{msg}");
            }
        }

        /// <summary>
        /// Log Warnings
        /// </summary>
        /// <param name="level"></param>
        /// <param name="msg"></param>
        public static void LogWarning(string msg, LogDomain domain = LogDomain.All)
        {
            if (logDict.Count == 0)
            {
                return;
            }

            if (logDict.Contains(LogDomain.All))
            {
                Debug.LogWarning($"<b><color=#008AFF>[{domain}]  </color></b>{msg}");
            }
            else if (logDict.Contains(domain))
            {
                Debug.LogWarning($"<b><color=#45FFE0>[{domain}]  </color></b>{msg}");
            }
        }

        /// <summary>
        /// Log Errors
        /// </summary>
        /// <param name="level"></param>
        /// <param name="msg"></param>
        public static void LogError(string msg, LogDomain domain = LogDomain.All)
        {
            if (logDict.Count == 0)
            {
                return;
            }
            if (logDict.Contains(LogDomain.All))
            {
                Debug.LogError($"<b><color=#008AFF>[{domain}]  </color></b>{msg}");
            }
            else if (logDict.Contains(domain))
            {
                Debug.LogError($"<b><color=#45FFE0>[{domain}]  </color></b>{msg}");
            }
        }
    }

}
