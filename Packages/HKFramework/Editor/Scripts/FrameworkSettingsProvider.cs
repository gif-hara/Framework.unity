using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HK.Framework.Editor
{
    public class FrameworkSettingsProvider : SettingsProvider
    {
        public FrameworkSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null)
            : base(path, scopes, keywords)
        {
        }
        
        [SettingsProvider]
        public static SettingsProvider Create()
        {
            var provider = new FrameworkSettingsProvider("Project/HKFramework", SettingsScope.Project)
            {
                label = "HKFramework",
                keywords = new HashSet<string>(new[] { "HKFramework" })
            };
            return provider;
        }
    }
}
