﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cave_Adventure.Properties {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Cave_Adventure.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
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
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap andesiteBackground {
            get {
                object obj = ResourceManager.GetObject("andesiteBackground", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на # .# .# .# .# .# .# .
        ///# .  .P .  .  .  .# .
        ///# .# .  .  .# .  .# .
        ///# .  .  .  .  .  .# .
        ///# .  .  .  .  .  .# .
        ///# .Sp.  .Sn.  .  .# .
        ///# .# .# .# .# .# .# ..
        /// </summary>
        internal static string Arena1 {
            get {
                return ResourceManager.GetString("Arena1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на .
        /// </summary>
        internal static string Arena10 {
            get {
                return ResourceManager.GetString("Arena10", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на # .# .# .# .# .# .# .# .# .# .
        ///# .  .P .Sp.  .  .  .  .  .# .
        ///# .# .  .# .  .  .  .  .  .# .
        ///# .  .# .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .# .
        ///# .# .  .# .# .# .  .# .# .# .
        ///# .  .  .  .  .# .  .  .  .# .
        ///# .  .  .  .  .# .  .  .  .# .
        ///# .  .  .  .  .# .  .  .  .# .
        ///# .# .# .# .# .# .# .# .# .# ..
        /// </summary>
        internal static string Arena2 {
            get {
                return ResourceManager.GetString("Arena2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на # .P .# .# .# .# .# .# .# .# .# .# .
        ///# .  .# .  .  .  .  .  .  .  .  .# .
        ///# .  .# .  .# .  .Sp.  .Sp.  .  .# .
        ///# .  .# .  .# .  .  .  .  .  .  .# .
        ///# .  .  .  .# .# .# .# .# .  .# .# .
        ///# .# .# .  .  .  .  .  .  .  .  .# .
        ///# .  .# .  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .# .  .Sn.  .# .  .# .# .
        ///# .  .  .Sn.# .  .  .  .# .  .  .# .
        ///# .Sp.  .  .# .  .# .# .# .  .  .# .
        ///# .  .Sp.  .# .  .  .  .  .  .  .# .
        ///# .# .# .# .# .# .# .# .# .# .# .# ..
        /// </summary>
        internal static string Arena3 {
            get {
                return ResourceManager.GetString("Arena3", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на # .# .# .# .# .# .# .# .# .# .
        ///# .  .  .  .  .  .  .  .  .# .
        ///# .  .  .P .  .  .  .  .  .# .
        ///# .  .  .Sn.  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .# .
        ///# .# .# .# .# .# .# .# .# .# ..
        /// </summary>
        internal static string Arena4 {
            get {
                return ResourceManager.GetString("Arena4", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на # .# .# .# .# .# .# .# .# .# .
        ///# .P .  .Sp.Sp.Sp.Sp.Sp.Sp.# .
        ///# .Sn.Sn.Sn.Sn.Sn.Sn.Sn.Sn.# .
        ///# .Sp.Sp.Sp.Sp.Sp.Sp.Sp.Sp.# .
        ///# .Sn.Sn.Sn.Sn.Sn.Sn.Sn.Sn.# .
        ///# .Sp.Sp.Sp.Sp.Sp.Sp.Sp.Sp.# .
        ///# .Sn.Sn.Sn.Sn.Sn.Sn.Sn.Sn.# .
        ///# .Sp.Sp.Sp.Sp.Sp.Sp.Sp.Sp.# .
        ///# .Sn.Sn.Sn.Sn.Sn.Sn.Sn.Sn.# .
        ///# .# .# .# .# .# .# .# .# .# ..
        /// </summary>
        internal static string Arena5 {
            get {
                return ResourceManager.GetString("Arena5", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на # .# .# .# .# .# .# .# .# .# .# .# .# .
        ///# .P .  .  .  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .  .  .  .# .
        ///# .  .  .Sn.  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .  .  .  .# .
        ///# .# .# .# .# .# .#  [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string Arena6 {
            get {
                return ResourceManager.GetString("Arena6", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на # .# .# .# .# .# .# .# .# .# .
        ///# .P .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .# .
        ///# .  .  .  .  .  .  .  .  .# .
        ///# .Sp.Sn.Sp.Sn.Sp.Sn.Sp.Sn.# .
        ///# .# .# .# .# .# .# .# .# .# ..
        /// </summary>
        internal static string Arena7 {
            get {
                return ResourceManager.GetString("Arena7", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на .
        /// </summary>
        internal static string Arena8 {
            get {
                return ResourceManager.GetString("Arena8", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на .
        /// </summary>
        internal static string Arena9 {
            get {
                return ResourceManager.GetString("Arena9", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Cobra {
            get {
                object obj = ResourceManager.GetObject("Cobra", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap dioriteBackground {
            get {
                object obj = ResourceManager.GetObject("dioriteBackground", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Dwarf {
            get {
                object obj = ResourceManager.GetObject("Dwarf", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap endBackground {
            get {
                object obj = ResourceManager.GetObject("endBackground", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap floorStone1 {
            get {
                object obj = ResourceManager.GetObject("floorStone1", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap floorStone2 {
            get {
                object obj = ResourceManager.GetObject("floorStone2", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap floorStoneBroken {
            get {
                object obj = ResourceManager.GetObject("floorStoneBroken", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Gladiator {
            get {
                object obj = ResourceManager.GetObject("Gladiator", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap grass1 {
            get {
                object obj = ResourceManager.GetObject("grass1", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap grass2 {
            get {
                object obj = ResourceManager.GetObject("grass2", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap gravelBackground {
            get {
                object obj = ResourceManager.GetObject("gravelBackground", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap ironBarsBackground {
            get {
                object obj = ResourceManager.GetObject("ironBarsBackground", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap mazePicMainMenu {
            get {
                object obj = ResourceManager.GetObject("mazePicMainMenu", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap netherBackground {
            get {
                object obj = ResourceManager.GetObject("netherBackground", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap noTexture {
            get {
                object obj = ResourceManager.GetObject("noTexture", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap obsidianBackground {
            get {
                object obj = ResourceManager.GetObject("obsidianBackground", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Spider {
            get {
                object obj = ResourceManager.GetObject("Spider", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap stoneBackground {
            get {
                object obj = ResourceManager.GetObject("stoneBackground", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap wall0 {
            get {
                object obj = ResourceManager.GetObject("wall0", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap wall1 {
            get {
                object obj = ResourceManager.GetObject("wall1", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap wall2 {
            get {
                object obj = ResourceManager.GetObject("wall2", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap wall3 {
            get {
                object obj = ResourceManager.GetObject("wall3", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap wall4 {
            get {
                object obj = ResourceManager.GetObject("wall4", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
    }
}
