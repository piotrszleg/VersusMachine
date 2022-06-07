using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class GridLayoutScript : MonoBehaviour
{

    GridLayoutGroup group;
    RectTransform rectTransform;
    public Rect percentPadding;
    public Vector2 percentSpacing;

    // Use this for initialization
    void Start()
    {
        group = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (rectTransform.rect.width > rectTransform.rect.height)
        {
            group.spacing = percentSpacing * rectTransform.rect.width;
            group.padding = new RectOffset(
                (int)(percentPadding.x * rectTransform.rect.width),
                (int)(percentPadding.width * rectTransform.rect.width),
                (int)(percentPadding.y * rectTransform.rect.width),
                (int)(percentPadding.height * rectTransform.rect.width)
                );
        }
        if (rectTransform.rect.height > rectTransform.rect.width)
        {
            group.spacing = percentSpacing * rectTransform.rect.width;
            //group.padding = percentPadding * rectTransform.rect.x;
        }
        if (group.constraint != GridLayoutGroup.Constraint.Flexible) UpdateCells();
    }
    void UpdateCells()
    {
        int columns = 0;
        int rows = 0;
        if (group.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
        {
            columns = group.constraintCount;
            rows = transform.childCount / group.constraintCount;
        }
        if (group.constraint == GridLayoutGroup.Constraint.FixedRowCount)
        {
            columns = transform.childCount / group.constraintCount;
            rows = group.constraintCount;
        }
        float innerWidth = rectTransform.rect.size.x - group.padding.horizontal;
        float innerHeight = rectTransform.rect.size.y - group.padding.vertical;
        Vector2 cellSize = new Vector2((innerWidth - group.spacing.x * (columns - 1)) / columns, (innerHeight - group.spacing.y * (rows - 1)) / rows);
        if (cellSize.x > cellSize.y)
        {
            cellSize.x = cellSize.y;
        }
        if (cellSize.y > cellSize.x)
        {
            cellSize.y = cellSize.x;
        }
        group.cellSize = cellSize;
    }
}
