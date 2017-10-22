﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Waf.FileHashGenerator.Applications.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Waf.FileHashGenerator.Applications.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to All files.
        /// </summary>
        internal static string AllFiles {
            get {
                return ResourceManager.GetString("AllFiles", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The following files could not be found:
        ///
        ///{0}.
        /// </summary>
        internal static string FilesNotFoundError {
            get {
                return ResourceManager.GetString("FilesNotFoundError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to MD5.
        /// </summary>
        internal static string MD5 {
            get {
                return ResourceManager.GetString("MD5", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SHA1.
        /// </summary>
        internal static string Sha1 {
            get {
                return ResourceManager.GetString("Sha1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SHA256.
        /// </summary>
        internal static string Sha256 {
            get {
                return ResourceManager.GetString("Sha256", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SHA512.
        /// </summary>
        internal static string Sha512 {
            get {
                return ResourceManager.GetString("Sha512", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An unknown error occurred during calculating the hash value.
        ///
        ///{0}.
        /// </summary>
        internal static string UnknownComputeHashError {
            get {
                return ResourceManager.GetString("UnknownComputeHashError", resourceCulture);
            }
        }
    }
}
