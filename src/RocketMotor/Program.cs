using PicoGK;

namespace Leap71.RocketMotorExamples
{
    public class Program
    {
        public static void Main()
        {
            string strOutputFolder = "/workspaces/LEAP71_HelixHeatX/output";

            try
            {
                Library.Go(
                    0.25f,
                    RocketMotorAssembly.Task,
                    strOutputFolder
                );
            }
            catch (Exception e)
            {
                Library.Log($"Failed to run Task: {e.Message}");
            }
        }
    }
}
