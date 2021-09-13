namespace developer0223.Tools.Demo
{
    // Unity
    using UnityEngine;

    public class FpsDisplayerSample : MonoBehaviour
    {
        private void Start()
        {
            CreateFpsDisplayer();
            ModifyFpsDisplayer();
        }

        public void CreateFpsDisplayer()
        {
            // You can create and destroy FpsDisplayer like below.

            // Default value of fontSize, DisplayPosition is 30 and upper left.

            //FpsDisplayer sample_01 = FpsDisplayer.GetOrCreate();
            //FpsDisplayer sample_02 = FpsDisplayer.GetOrCreate(30);
            FpsDisplayer sample_03 = FpsDisplayer.GetOrCreate(75, DisplayPosition.UpperRight);
        }

        public void ModifyFpsDisplayer()
        {
            FpsDisplayer fpsDisplayer = FpsDisplayer.GetOrCreate();

            // Text Size
            fpsDisplayer.SetFontSize(50);

            // Text Color
            //fpsDisplayer.SetTextColor(Color.red);
            //fpsDisplayer.SetTextColor(new Color(1, 1, 1));
            //fpsDisplayer.SetTextColor(new Color(1, 1, 1, 0.5f));
            //fpsDisplayer.SetTextColor(new Vector4(1, 1, 1, 0.5f));
            fpsDisplayer.SetTextColor(new Color32(0, 255, 0, 255));

            // DisplayPosition
            fpsDisplayer.SetDisplayPosition(DisplayPosition.MiddleCenter);
        }

        public void DestroyFpsDisplayer()
        {
            FpsDisplayer.Destroy();
        }
    }
}