﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChatManager.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ChatManager.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Die angegebene Farbe in der Textbox entspricht nicht dem HEX-Standard..
        /// </summary>
        internal static string Err_ColorNotHex {
            get {
                return ResourceManager.GetString("Err_ColorNotHex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fehler.
        /// </summary>
        internal static string MessageBoxError {
            get {
                return ResourceManager.GetString("MessageBoxError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Information.
        /// </summary>
        internal static string MessageBoxInfo {
            get {
                return ResourceManager.GetString("MessageBoxInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Warnung.
        /// </summary>
        internal static string MessageBoxWarn {
            get {
                return ResourceManager.GetString("MessageBoxWarn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Der Pfad für die lokalen Dateien von SWTOR wurde nicht gefunden!
        ///
        ///Ist das Spiel installiert?.
        /// </summary>
        internal static string Warn_SWTORpathNotFound {
            get {
                return ResourceManager.GetString("Warn_SWTORpathNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Es wurde eine aktive SWTOR Instanz gefunden, bitte schließe das Spiel zuerst!
        ///
        ///Andernfalls kann die korrekte Funktion der Änderungen nicht garantiert werden..
        /// </summary>
        internal static string Warn_SWTORrunning {
            get {
                return ResourceManager.GetString("Warn_SWTORrunning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Es muss zuerst eine Datei importiert werden!.
        /// </summary>
        internal static string Warn_TextBoxEmpty {
            get {
                return ResourceManager.GetString("Warn_TextBoxEmpty", resourceCulture);
            }
        }
    }
}
