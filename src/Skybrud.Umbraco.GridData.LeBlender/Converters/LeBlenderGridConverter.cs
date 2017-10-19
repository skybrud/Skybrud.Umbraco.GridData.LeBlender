using System;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.LeBlender.Config;
using Skybrud.Umbraco.GridData.LeBlender.Values;
using Skybrud.Umbraco.GridData.Rendering;

namespace Skybrud.Umbraco.GridData.LeBlender.Converters {

    /// <summary>
    /// Converter for handling the LeBlender editors.
    /// </summary>
    public class LeBlenderGridConverter : IGridConverter {

        /// <summary>
        /// The function that checks whether the editor is an LeBlender editor.
        /// </summary>
        private readonly Func<GridEditor, bool> _isLeBlenderEditor;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeBlenderGridConverter" /> class.
        /// </summary>
        public LeBlenderGridConverter() : this(IsLeBlenderEditor) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LeBlenderGridConverter" /> class.
        /// </summary>
        /// <param name="isLeBlenderEditor">The function that checks whether the editor is an LeBlender editor.</param>
        /// <exception cref="ArgumentNullException">isLeBlenderEditor</exception>
        public LeBlenderGridConverter(Func<GridEditor, bool> isLeBlenderEditor) {
            _isLeBlenderEditor = isLeBlenderEditor ?? throw new ArgumentNullException(nameof(isLeBlenderEditor));
        }

        /// <summary>
        /// Converts the specified <paramref name="token" /> into an instance of <see cref="IGridControlValue" />.
        /// </summary>
        /// <param name="control">A reference to the parent <see cref="GridControl" />.</param>
        /// <param name="token">The instance of <see cref="JToken" /> representing the control value.</param>
        /// <param name="value">The converted control value.</param>
        /// <returns><code>true</code> if the converter provided a value; otherwise <code>false</code>.</returns>
        public bool ConvertControlValue(GridControl control, JToken token, out IGridControlValue value) {
            value = null;
            if (_isLeBlenderEditor(control.Editor)) {
                value = GridControlLeBlenderValue.Parse(control);
            }
            return value != null;
        }

        /// <summary>
        /// Converts the specified <paramref name="token" /> into an instance of <see cref="IGridEditorConfig" />.
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="token">The instance of <see cref="JToken" /> representing the editor config.</param>
        /// <param name="config">The converted editor config.</param>
        /// <returns><code>true</code> if the converter provided a config value; otherwise <code>false</code>.</returns>
        public bool ConvertEditorConfig(GridEditor editor, JToken token, out IGridEditorConfig config) {
            config = null;
            if (_isLeBlenderEditor(editor)) {
                config = GridEditorLeBlenderConfig.Parse(editor, token as JObject);
            }
            return config != null;
        }

        /// <summary>
        /// Gets an instance <see cref="GridControlWrapper" /> for the specified <paramref name="control" />.
        /// </summary>
        /// <param name="control">The control to be wrapped.</param>
        /// <param name="wrapper">The wrapper.</param>
        /// <returns><code>true</code> if the converter provided a wrapper; otherwise <code>false</code>.</returns>
        public bool GetControlWrapper(GridControl control, out GridControlWrapper wrapper) {
            wrapper = null;
            if (_isLeBlenderEditor(control.Editor)) {
                wrapper = control.GetControlWrapper<GridControlLeBlenderValue, GridEditorLeBlenderConfig>();
            }
            return wrapper != null;
        }

        /// <summary>
        /// Determines whether <paramref name="editor" /> is a LeBlender editor.
        /// </summary>
        /// <param name="editor">The editor.</param>
        /// <returns>
        ///   <code>true</code> if <paramref name="editor" /> is a LeBlender editor; otherwise, <code>false</code>.
        /// </returns>
        /// <remarks>
        /// Checks whether the view starts with <code>/App_Plugins/LeBlender/</code> or the alias starts with <code>LeBlender.</code>.
        /// </remarks>
        private static bool IsLeBlenderEditor(GridEditor editor) {
            return editor.View.StartsWith("/App_Plugins/LeBlender/", StringComparison.OrdinalIgnoreCase) ||
                editor.Alias.StartsWith("LeBlender.", StringComparison.OrdinalIgnoreCase); // Kept for backwards compatibility
        }

    }

}