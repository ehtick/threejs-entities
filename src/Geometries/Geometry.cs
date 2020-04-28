using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JsonSubTypes;
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
    [Serializable]
    [DataContract]
    [Newtonsoft.Json.JsonConverter(typeof(JsonSubtypes), nameof(Type))]
    [JsonSubtypes.KnownSubType(typeof(BoxGeometry), nameof(BoxGeometry))]
    public partial class Geometry : IEquatable<Geometry>
    {
        /// <summary>Initializes a new instance of the <see cref="Geometry"/> class.</summary>
        /// <param name="uuid">The UUID of this object instance.</param>
        [JsonConstructor]
        public Geometry(string uuid)
        {
            Uuid = uuid ?? Guid.NewGuid().ToString();
            Data = new GeometryData();
        }

        /// <summary>Gets the type of this object, it always equals 'Geometry'.</summary>
        /// <value>The 'Geometry' type.</value>
        [DataMember(Name = "type")]
        [JsonProperty("type")]
        [JsonPropertyName("type")]
        public virtual string Type => nameof(Geometry);

        /// <summary>
        /// Gets the UUID of this object instance.
        /// </summary>
        /// <value>
        /// The UUID. This gets automatically assigned and shouldn't be edited.
        /// </value>
        [DataMember(Name = "uuid")]
        [JsonProperty("uuid")]
        [JsonPropertyName("uuid")]
        public string Uuid { get; private set; }

        [DataMember(Name = "data")]
        [JsonProperty("data")]
        [JsonPropertyName("data")]
        public virtual GeometryData Data { get; private set; }

        /// <summary>Adds the point to the vertices.</summary>
        /// <param name="vertex">The vertex.</param>
        [SuppressMessage(
            "StyleCop.CSharp.OrderingRules",
            "SA1201:Elements should appear in the correct order",
            Justification = "The class below is private, so it will never be OK.")]
        public void AddPoint(Vector3 vertex)
        {
            if (vertex is null)
                throw new ArgumentNullException(nameof(vertex));

            Data.Vertices.Add(vertex.X);
            Data.Vertices.Add(vertex.Y);
            Data.Vertices.Add(vertex.Z);
        }

        /// <summary>Adds the point to the vertices.</summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        public void AddPoint(double x, double y, double z)
        {
            Data.Vertices.Add(x);
            Data.Vertices.Add(y);
            Data.Vertices.Add(z);
        }

        /// <summary>Adds a faces from its vertices.</summary>
        /// <param name="vertices">The vertices of a face.</param>
        public void AddFace(params int[] vertices)
        {
            Data.Faces.AddRange(vertices);
        }

        public override bool Equals(object obj) => obj is Geometry geometry && Equals(geometry);

        public bool Equals(Geometry other) => Uuid.Equals(other?.Uuid, StringComparison.OrdinalIgnoreCase);

        public override int GetHashCode()
        {
            return 2083305506 + EqualityComparer<string>.Default.GetHashCode(Uuid);
        }
    }
}
