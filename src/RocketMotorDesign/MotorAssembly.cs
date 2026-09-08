using System;

namespace RocketMotorDesign
{
    public class MotorAssembly
    {
        public ChamberDesign Chamber { get; set; }
        public NozzleDesign Nozzle { get; set; }
        public InjectorDesign Injector { get; set; }
        public TurbopumpDesign Turbopump { get; set; }
        public PreburnerDesign Preburner { get; set; }
        public ThermalEstimate Thermal { get; set; }

        public MotorAssembly()
        {
            Chamber = new ChamberDesign();
            Nozzle = new NozzleDesign();
            Injector = new InjectorDesign();
            Turbopump = new TurbopumpDesign();
            Preburner = new PreburnerDesign();
            Thermal = new ThermalEstimate();
        }

        public double TotalLength
        {
            get
            {
                double chamberLen = Chamber.ChamberLength + Chamber.ConvergentLength;
                double nozzleLen = Nozzle.Length;
                return chamberLen + nozzleLen;
            }
        }

        public double MaxOuterRadius
        {
            get
            {
                double maxR = 0.0;
                for (double z = 0; z <= TotalLength; z += 0.01)
                {
                    maxR = Math.Max(maxR, Chamber.OuterRadiusAt(z));
                }
                return maxR;
            }
        }

        public void PrintSummary()
        {
            Console.WriteLine("=== Motor Assembly Summary ===");
            Console.WriteLine($"Total length: {TotalLength * 1000.0:F1} mm");
            Console.WriteLine($"Max outer radius: {MaxOuterRadius * 1000.0:F1} mm");
            Console.WriteLine($"Chamber radius: {Chamber.ChamberRadius * 1000.0:F1} mm");
            Console.WriteLine($"Throat radius: {Chamber.ThroatRadius * 1000.0:F1} mm");
            Console.WriteLine($"Nozzle exit radius: {Chamber.NozzleExitRadius * 1000.0:F1} mm");
            Console.WriteLine($"Channel count: {Chamber.ChannelCount}");
            Console.WriteLine($"Helix angle: {Chamber.HelixAngleDeg} deg");
            Console.WriteLine($"Injector count: {Injector.Count}");
            Console.WriteLine($"Turbopump speed: {Turbopump.SpeedRpm} rpm");
            Console.WriteLine($"Preburner major radius: {Preburner.MajorRadius * 1000.0:F1} mm");
        }
    }
}
