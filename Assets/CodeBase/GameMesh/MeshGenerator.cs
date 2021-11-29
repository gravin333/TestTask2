using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.GameMesh
{
    public class MeshGenerator : MonoBehaviour
    {
        public static MeshGenerator Instanse;
        public HeelChenger heelChanger;
        public float hellScale;
        public Material Material;

        public float scale = 1f;
        public bool showMesh;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private GameObject Example;

        public void Awake()
        {
            Instanse = this;
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.material = Material;
        }

        public void GenerateMesh(Vector3[] toArray)
        {
            var angles = AnglesBetweenPoints(toArray);
            var transformPosition = transform.position;
            Vector3[] vert =
            {
                new Vector3(0, -(scale / 2), scale / 2) - transformPosition,
                new Vector3(0, scale / 2, scale / 2) - transformPosition,

                new Vector3(0, scale / 2, -(scale / 2)) - transformPosition,
                new Vector3(0, -(scale / 2), -(scale / 2)) - transformPosition
            };

            var mesh = new Mesh();
            if (showMesh)
                _meshFilter.mesh = mesh;

            var vertices = GenerateVertices(toArray, vert, angles);
            for (var i = 0; i < angles.Length; i++) Debug.Log(angles[i]);

            int[] triangles =
            {
                0, 1, 2, //face front
                0, 2, 3,

                0, 4, 5, //face right
                0, 5, 1,

                1, 5, 6, //face top
                1, 6, 2,

                2, 6, 7, //face left
                2, 7, 3,

                3, 7, 4, //face bottom
                3, 4, 0,

                7, 6, 5, //face back
                7, 5, 4
            };

            var tr = GenerateTriangles(triangles, vertices);


            mesh.vertices = vertices;
            mesh.triangles = tr;
            mesh.RecalculateNormals();
            mesh.Optimize();

            GenerateObject(mesh, Material);
        }

        private void GenerateObject(Mesh mesh, Material material)
        {
            heelChanger.AddHeels(mesh, material);
        }

        private float[] AnglesBetweenPoints(Vector3[] toArray)
        {
            var rotations = new Quaternion[toArray.Length];
            var angles = new float[toArray.Length];
            for (var i = 0; i < toArray.Length; i++)
                if (i + 1 < toArray.Length)
                {
                    var targetDir = toArray[i] - toArray[i + 1];
                    var angle = Vector3.Angle(targetDir, Vector3.up);
                    angles[i] = angle - 90f;
                }
                else
                {
                    angles[i] = Vector3.Angle(toArray[i], toArray[i]);
                }

            return angles;
        }

        private int[] GenerateTriangles(int[] triangles, Vector3[] vertices)
        {
            var trianglesList = new List<int>();
            for (var i = 0; i < vertices.Length - 4; i += 4)
            for (var j = 0; j < triangles.Length; j++)
                trianglesList.Add(triangles[j] + i);

            return trianglesList.ToArray();
        }

        private Vector3[] GenerateVertices(Vector3[] toArray, Vector3[] vert, float[] angles)
        {
            var list = new List<Vector3>();
            for (var i = 0; i < toArray.Length - 1; i++)
            for (var j = 0; j < vert.Length; j++)
            {
                var point = new Vector3(vert[j].x + toArray[i].x, vert[j].y + toArray[i].y, vert[j].z);
                point = RotatePointAroundPivot(point, toArray[i], new Vector3(0, 0, angles[i]));
                list.Add(point - toArray[0]);
            }

            return list.ToArray();
        }

        private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
        {
            var dir = point - pivot;
            dir = Quaternion.Euler(angles) * dir;
            point = dir + pivot;
            return point;
        }
    }
}