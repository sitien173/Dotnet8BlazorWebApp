﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlazorWebApp.Resources {
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
    internal class SharedResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SharedResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BlazorWebApp.Resources.SharedResource", typeof(SharedResource).Assembly);
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
        ///   Looks up a localized string similar to Cancel.
        /// </summary>
        internal static string Cancel_Btn {
            get {
                return ResourceManager.GetString("Cancel_Btn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Add Detail.
        /// </summary>
        internal static string CreatePost_Add_Detail_Btn {
            get {
                return ResourceManager.GetString("CreatePost_Add_Detail_Btn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Detail.
        /// </summary>
        internal static string CreatePost_Detail_Title {
            get {
                return ResourceManager.GetString("CreatePost_Detail_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Post is created successfully.
        /// </summary>
        internal static string CreatePost_Successfully {
            get {
                return ResourceManager.GetString("CreatePost_Successfully", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred while processing your request..
        /// </summary>
        internal static string DefaultTitleException {
            get {
                return ResourceManager.GetString("DefaultTitleException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} existed..
        /// </summary>
        internal static string FieldIsExisted {
            get {
                return ResourceManager.GetString("FieldIsExisted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is not existed in database..
        /// </summary>
        internal static string FieldIsNotExistedInDatabase {
            get {
                return ResourceManager.GetString("FieldIsNotExistedInDatabase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Save.
        /// </summary>
        internal static string Save_Btn {
            get {
                return ResourceManager.GetString("Save_Btn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to One or more validation errors occurred..
        /// </summary>
        internal static string ValidationFailedException {
            get {
                return ResourceManager.GetString("ValidationFailedException", resourceCulture);
            }
        }
    }
}
