using UnityEngine;

public class BorderController : MonoBehaviour
{
    void Start()
    {
        AdjustBorders();
    }

    void AdjustBorders()
    {
        Camera camera = Camera.main;
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        float height = camera.orthographicSize * 2;
        float width = height * aspectRatio;

        // Top and Bottom Borders
        Transform topBorder = transform.Find("TopBorder");
        Transform bottomBorder = transform.Find("BottomBorder");
        topBorder.localScale = new Vector3(width, 1, 1);
        bottomBorder.localScale = new Vector3(width, 1, 1);
        topBorder.localPosition = new Vector3(0, height / 2, 0);
        bottomBorder.localPosition = new Vector3(0, -height / 2, 0);

        // Left and Right Borders
        Transform leftBorder = transform.Find("LeftBorder");
        Transform rightBorder = transform.Find("RightBorder");
        leftBorder.localScale = new Vector3(1, height, 1);
        rightBorder.localScale = new Vector3(1, height, 1);
        leftBorder.localPosition = new Vector3(-width / 2, 0, 0);
        rightBorder.localPosition = new Vector3(width / 2, 0, 0);
    }
}
