using System;

namespace RocketMotorDesign
{
    public class Program
    {
        public static void Main()
        {
            var assembly = new MotorAssembly();
            assembly.PrintSummary();

            Console.WriteLine("\n=== Chamber Profile ===");
            Console.WriteLine("z [mm] | rInner [mm] | rOuter [mm]");
            for (double z = 0; z <= assembly.TotalLength; z += 0.05)
            {
                double rInner = assembly.Chamber.InnerRadiusAt(z);
                double rOuter = assembly.Chamber.OuterRadiusAt(z);
                if (z <= 0.01 || z >= assembly.TotalLength - 0.01 || (z / 0.05) % 5 == 0)
                {
                    Console.WriteLine($"{z * 1000.0,7:F1} | {rInner * 1000.0,11:F2} | {rOuter * 1000.0,12:F2}");
                }
            }

            Console.WriteLine("\n=== Nozzle Profile ===");
            var profile = assembly.Nozzle.GenerateProfile();
            foreach (var (r, z) in profile)
            {
                Console.WriteLine($"z={z * 1000.0,6:F1} mm, r={r * 1000.0,7:F2} mm");
            }

            Console.WriteLine("\n=== Injector Positions ===");
            var injectors = assembly.Injector.Positions();
            foreach (var (x, y, angle) in injectors)
            {
                Console.WriteLine($"x={x * 1000.0,6:F2} mm, y={y * 1000.0,6:F2} mm, angle={angle,5:F1} deg");
            }

            Console.WriteLine("\n=== Turbopump Blade Sections ===");
            var blades = assembly.Turbopump.BladeSections();
            foreach (var (r, z, angle) in blades)
            {
                Console.WriteLine($"r={r * 1000.0,6:F2} mm, z={z * 1000.0,6:F2} mm, angle={angle * 180.0 / Math.PI,5:F1} deg");
            }

            Console.WriteLine("\n=== Thermal Segment (1st) ===");
            var thermal = assembly.Thermal.EvaluateSegment(assembly.Chamber, 0, 5);
            foreach (var kv in thermal)
            {
                Console.WriteLine($"{kv.Key}: {kv.Value:F4}");
            }

            Console.WriteLine("\n=== ASE Parameters Reference ===");
            PrintDictionary("Rated Thrust", EngineParameters.ASE_RatedThrust);
            PrintDictionary("Weight", EngineParameters.ASE_Weight);
            PrintDictionary("Fuel Pump Stage 1", EngineParameters.HPFuelPump_Stage1);

            Console.WriteLine("\nRocketMotorDesign demo completed.");
        }

        private static void PrintDictionary(string title, System.Collections.Generic.Dictionary<string, double> dict)
        {
            Console.WriteLine($"-- {title} --");
            foreach (var kv in dict)
            {
                Console.WriteLine($"  {kv.Key}: {kv.Value}");
            }
        }
    }
}
