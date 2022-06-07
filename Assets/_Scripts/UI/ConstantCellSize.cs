using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class ConstantCellSize : MonoBehaviour
{

    GridLayoutGroup group;
    RectTransform rectTransform;
    public float percentCellSize = 0.1f;
    public float percentSpacing=0.05f;

    // Use this for initialization
    void Start()
    {
        group = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (Screen.width > Screen.height)
        {
            group.cellSize = Vector2.one * (percentCellSize * Screen.height);
        }
        else
        {
            group.cellSize = Vector2.one*(percentCellSize * Screen.width);
        }
        group.spacing = percentSpacing * group.cellSize;
        //if (group.constraint != GridLayoutGroup.Constraint.Flexible) UpdateCells();
    }
    /*void UpdateCells()
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
        float cellsWidth = columns * group.cellSize.x + (columns - 1) * group.spacing.x;
        float horizontalSpace = rectTransform.rect.width - cellsWidth;
        group.padding.left = group.padding.right = (int)(horizontalSpace / 2);
        float cellsHeight = rows * group.cellSize.y + (rows - 1) * group.spacing.y;
        float verticalSpace = rectTransform.rect.height - cellsHeight;
        group.padding.top = group.padding.bottom = (int)(verticalSpace / 2);
    }*/
}
