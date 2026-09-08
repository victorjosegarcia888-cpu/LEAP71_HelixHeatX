using PicoGK;

namespace Leap71.RocketMotor
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                Library.Go(
                    0.25f,
                    Task);
            }
            catch (Exception e)
            {
                Library.Log($"Failed to run Task: {e.Message}");
            }
        }

        public static void Task()
        {
            try
            {
                MotorAssembly oAssembly = new MotorAssembly();
                oAssembly.PrintSummary();

                Voxels voxMotor = oAssembly.voxAssembledMotor();
                Sh.PreviewVoxels(voxMotor, Cp.clrRock);

                Library.Log("Rocket motor geometry generated successfully.");
            }
            catch (Exception e)
            {
                Library.Log($"Failed run example: {e.Message}");
            }
        }
    }
}
