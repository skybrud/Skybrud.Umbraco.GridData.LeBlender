using System;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Config;

namespace Skybrud.Umbraco.GridData.LeBlender.Config {

    /// <summary>
    /// Class representing the configuration of a link picker.
    /// </summary>
    public class GridEditorLeBlenderConfig : GridEditorConfigBase {

        #region Properties

        /// <summary>
        /// Gets the minimum amount of allowed items.
        /// </summary>
        public int Min { get; private set; }

        /// <summary>
        /// Gets the maximium amount of allowed items.
        /// </summary>
        public int Max { get; private set; }

        /// <summary>
        /// Gets whether the control should be rendred in the grid.
        /// </summary>
        public bool RenderInGrid { get; private set; }

        /// <summary>
        /// Gets the cache period of the control. If this property equals <see cref="TimeSpan.Zero"/>, caching is disabled.
        /// </summary>
        public TimeSpan CachePeriod { get; private set; }

        /// <summary>
        /// Gets whether caching has been enabled for controls of this type.
        /// </summary>
        public bool HasCachePeriod => CachePeriod != TimeSpan.Zero;

        #endregion

        #region Constructors

        private GridEditorLeBlenderConfig(GridEditor editor, JObject obj) : base(editor, obj) {
            Min = obj.GetInt32("min");
            Max = obj.GetInt32("max");
            RenderInGrid = obj.GetBoolean("renderInGrid");
            CachePeriod = obj.GetDouble("expiration", TimeSpan.FromSeconds);
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="obj"/> into an instance of <see cref="GridEditorLeBlenderConfig"/>.
        /// </summary>
        /// <param name="editor">The parent editor.</param>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        /// <returns>An instance of <see cref="GridEditorLeBlenderConfig"/>.</returns>
        public static GridEditorLeBlenderConfig Parse(GridEditor editor, JObject obj) {
            return obj == null ? null : new GridEditorLeBlenderConfig(editor, obj);
        }

        #endregion

    }

}