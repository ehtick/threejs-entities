using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CodeCave.Threejs.Entities
{
    /// <summary>
    /// Geometry is a user-friendly alternative to BufferGeometry.
    /// Geometries store attributes (vertex positions, faces, colors, etc.)
    /// using objects like Vector3 or Color that are easier to read and edit,
    /// but less efficient than typed arrays.
    ///
    /// Prefer <see cref="BufferGeometry"/> for large or serious projects.
    /// </summary>
    [DataContract]
    public class Geometry
    {
        /// <summary>Gets the type of this object, it always equals 'Geometry'.</summary>
        /// <value>The 'Geometry' type.</value>
        [DataMember(Name = "type")]
        [JsonProperty("type")]
        public const string Type = nameof(Geometry);

        /// <summary>
        /// Gets or sets the UUID of this object instance.
        /// </summary>
        /// <value>
        /// The UUID. This gets automatically assigned and shouldn't be edited.
        /// </value>
        [DataMember(Name = "uuid")]
        [JsonProperty("uuid")]
        public string Uuid { get; set; } = Guid.NewGuid().ToString();

        [DataMember(Name = "data")]
        [JsonProperty("data")]
        private GeometryData Data { get; } = new GeometryData();

        // TODO implement morphTargets[]
        // TODO implement morphNormals[]
        [DataContract]
        private class GeometryData
        {
            /// <summary>
            /// Gets or sets a value indicating whether [casts shadow].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [casts shadow]; otherwise, <c>false</c>.
            /// </value>
            [DataMember(Name = "castShadow")]
            [JsonProperty("castShadow")]
            public bool CastShadow { get; set; } = true;

            /// <summary>
            /// Gets or sets the array of vertex colors, matching number and order of vertices.
            /// </summary>
            /// <value>
            /// The array of vertex colors, matching number and order of vertices.
            /// </value>
            [DataMember(Name = "colors")]
            [JsonProperty("colors")]
            public ICollection<int> Colors { get; set; } = new List<int>();

            /// <summary>
            /// Gets or sets a value indicating whether [is double sided].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [is double sided]; otherwise, <c>false</c>.
            /// </value>
            [DataMember(Name = "doubleSided")]
            [JsonProperty("doubleSided")]
            public bool DoubleSided { get; set; } = true;

            /// <summary>
            /// Gets or sets the array of faces.
            /// The array of faces describe how each vertex in the model is connected to form faces.
            /// Additionally it holds information about face and vertex normals and colors.
            /// </summary>
            /// <value>
            /// The array of faces.
            /// </value>
            [DataMember(Name = "faces")]
            [JsonProperty("faces")]
            public List<int> Faces { get; set; } = new List<int>();

            /// <summary>
            /// Gets or sets the normals.
            /// </summary>
            /// <value>
            /// The normals.
            /// </value>
            [DataMember(Name = "normals")]
            [JsonProperty("normals")]
            public ICollection<double> Normals { get; set; } = new List<double>();

            /// <summary>
            /// Gets or sets a value indicating whether [receives shadow].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [receives shadow]; otherwise, <c>false</c>.
            /// </value>
            /// ReSharper disable once RedundantDefaultMemberInitializer
            [DataMember(Name = "receiveShadow")]
            [JsonProperty("receiveShadow")]
            public bool ReceiveShadow { get; set; } = false;

            /// <summary>
            /// Gets or sets the scale of the geometry data.
            /// Scaling is typically done as a one time operation but not during the render loop.
            /// </summary>
            /// <value>
            /// The scale of the geometry data.
            /// </value>
            [DataMember(Name = "scale")]
            [JsonProperty("scale")]
            public double Scale { get; set; } = 1.0D;

            /// <summary>
            /// Gets or sets the array of face UV layers, used for mapping textures onto the geometry.
            /// Each UV layer is an array of UVs matching the order and number of vertices in faces.
            /// </summary>
            /// <value>
            /// The array of face UV layers, used for mapping textures onto the geometry.
            /// </value>
            [DataMember(Name = "uvs")]
            [JsonProperty("uvs")]
            public ICollection<double> UVs { get; set; } = new List<double>();

            /// <summary>
            /// Gets or sets the array of vertices (in millimeters).
            /// </summary>
            /// <value>
            /// The array of vertices holds the position of every vertex in the model.
            /// </value>
            [DataMember(Name = "vertices")]
            [JsonProperty("vertices")]
            public ICollection<double> Vertices { get; set; } = new List<double>();

            /// <summary>
            /// Gets or sets a value indicating whether this <see cref="GeometryData"/> is visible.
            /// </summary>
            /// <value>
            ///   <c>true</c> if visible; otherwise, <c>false</c>.
            /// </value>
            [DataMember(Name = "visible")]
            [JsonProperty("visible")]
            public bool Visible { get; set; } = true;
        }

        /// <summary>Gets the materials of this <see cref="Geometry"/> instance.</summary>
        /// <value>The materials.</value>
        [DataMember(Name = "materials")]
        [JsonProperty("materials")]
        [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:Elements should appear in the correct order", Justification = "The class above is private.")]
        public ICollection<Material> Materials { get; internal set; } = new List<Material>();

        /// <summary>Adds the vertice.</summary>
        /// <param name="vertice">The vertice.</param>
        public void AddVertice(long vertice)
        {
            Data.Vertices.Add(vertice);
        }

        /// <summary>Adds the faces.</summary>
        /// <param name="faces">The faces.</param>
        public void AddFaces(params int[] faces)
        {
            Data.Faces.AddRange(faces);
        }
    }
}