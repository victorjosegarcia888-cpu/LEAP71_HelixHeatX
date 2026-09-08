using System;
using System.Threading;
using PicoGK;

namespace Leap71.RocketMotorExamples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string strOutputFolder = "/workspaces/LEAP71_HelixHeatX/output";

            try
            {
                ThreadStart task = args.Length > 0 ? ResolveTask(args[0]) : null;

                if (task == null)
                {
                    PrintHelp();
                    return;
                }

                Library.Go(
                    0.25f,
                    task,
                    strOutputFolder
                );
            }
            catch (Exception e)
            {
                Library.Log($"Failed to run Task: {e.Message}");
            }
        }

        private static ThreadStart ResolveTask(string strTask)
        {
            return strTask.ToLowerInvariant() switch
            {
                "chamber" => RocketMotorChamber.Task,
                "nozzle" => RocketMotorNozzle.Task,
                "injector" => RocketMotorInjector.Task,
                "turbopump" => RocketMotorTurbopump.Task,
                "preburner" => RocketMotorPreburner.Task,
                "cooling" => RocketMotorCoolingChannels.Task,
                "lattice" => RocketMotorLattice.Task,
                "assembly" => RocketMotorAssembly.Task,
                "assemblywithcooling" => RocketMotorAssembly_WithCooling.Task,
                _ => null
            };
        }

        private static void PrintHelp()
        {
            Console.WriteLine("RocketMotor Examples");
            Console.WriteLine("Usage: dotnet run --project src/RocketMotor/RocketMotor.csproj -- [task]");
            Console.WriteLine();
            Console.WriteLine("Available tasks:");
            Console.WriteLine("  chamber            Rocket motor chamber basic and modulated");
            Console.WriteLine("  nozzle             Rocket motor nozzle profile and wall");
            Console.WriteLine("  injector           Rocket motor injector plate and elements");
            Console.WriteLine("  turbopump          Rocket motor turbopump hub and blades");
            Console.WriteLine("  preburner          Rocket motor preburner toroidal");
            Console.WriteLine("  cooling            Rocket motor cooling channels");
            Console.WriteLine("  lattice            Rocket motor lattice reinforcement");
            Console.WriteLine("  assembly           Rocket motor assembly without cooling");
            Console.WriteLine("  assemblywithcooling Rocket motor assembly with cooling channels subtracted");
            Console.WriteLine();
            Console.WriteLine("Example:");
            Console.WriteLine("  dotnet run --project src/RocketMotor/RocketMotor.csproj -- assemblywithcooling");
        }
    }
}
