﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lecoati.LeBlender.Extension.Models;
using Newtonsoft.Json;
using Skybrud.Umbraco.GridData.Interfaces;

namespace Skybrud.Umbraco.GridData.LeBlender.Values {

    /// <summary>
    /// Class representing the value of a LeBlender control.
    /// </summary>
    public class GridControlLeBlenderValue : IGridControlValue {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent control.
        /// </summary>
        public GridControl Control { get; private set; }

        /// <summary>
        /// Gets a reference to the underlying <see cref="LeBlenderModel"/>.
        /// </summary>
        [JsonIgnore]
        public LeBlenderModel LeBlender { get; private set; }

        /// <summary>
        /// Gets whether the control is valid (alias of <see cref="HasItems"/>).
        /// </summary>
        [JsonIgnore]
        public bool IsValid {
            get { return HasItems; }
        }

        /// <summary>
        /// Gets the items from the LeBlender model.
        /// </summary>
        [JsonIgnore]
        public IEnumerable<LeBlenderValue> Items {
            get { return LeBlender == null ? new LeBlenderValue[0] : LeBlender.Items; }
        }

        /// <summary>
        /// Gets whether the underlying <see cref="LeBlenderModel"/> model has any items.
        /// </summary>
        [JsonIgnore]
        public bool HasItems {
            get { return Items.Any(); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="control"/>.
        /// </summary>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the control.</param>
        protected GridControlLeBlenderValue(GridControl control) {
            Control = control;
            LeBlender = control.JObject.ToObject<LeBlenderModel>();
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets the value of the control as a searchable text - eg. to be used in Examine.
        /// </summary>
        /// <returns>An instance of <see cref="System.String"/> with the value as a searchable text.</returns>
        public string GetSearchableText() {
            if (!IsValid) return "";
            StringBuilder sb = new StringBuilder();
            return sb.ToString().Trim();
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets an instance of <see cref="GridControlLeBlenderValue"/> from the specified <paramref name="control"/>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <returns>An instance of <see cref="GridControlLeBlenderValue"/>.</returns>
        public static GridControlLeBlenderValue Parse(GridControl control) {
            return control == null ? null : new GridControlLeBlenderValue(control);
        }

        #endregion

    }

}