using System.Collections.Generic;
using CodeBase.GameMesh;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.DrawImage
{
    public class DrawLine : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerExitHandler, IPointerEnterHandler
    {
        public LineRenderer LineRenderer;
        public Camera cm;
        public float UpdateDistance = 0.2f;
        private readonly List<Vector3> fingerPoints = new List<Vector3>();

        private void Update()
        {
        }

        public void OnDrag(PointerEventData eventData)
        {
            UpdateLine();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Pane");
            Debug.Log($"fingerPoints {string.Join(" ", fingerPoints.ToArray())}");
            //MeshGenerator.
            MeshGenerator.Instanse.GenerateMesh(fingerPoints.ToArray());

            ResetLine();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetFirstPosition();
        }


        public void OnPointerExit(PointerEventData eventData)
        {
        }

        private void UpdateLine()
        {
            var newPosition = cm.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            if (Vector3.Distance(newPosition, fingerPoints[fingerPoints.Count - 1]) > UpdateDistance)
            {
                fingerPoints.Add(newPosition);
                LineRenderer.positionCount++;
                LineRenderer.SetPosition(LineRenderer.positionCount - 1, newPosition);
            }
        }

        private void SetFirstPosition()
        {
            ResetLine();
            fingerPoints.Add(cm.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1)));
            LineRenderer.SetPosition(0, fingerPoints[0]);
            LineRenderer.SetPosition(1, fingerPoints[0]);
        }

        private void ResetLine()
        {
            fingerPoints.Clear();
            LineRenderer.positionCount = 2;
            LineRenderer.SetPosition(0, new Vector3(-10f, -10f, -10f));
            LineRenderer.SetPosition(1, new Vector3(-10f, -10f, -10f));
        }
    }
}