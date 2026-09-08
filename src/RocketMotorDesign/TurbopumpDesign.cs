using System;
using System.Collections.Generic;

namespace RocketMotorDesign
{
    public class TurbopumpDesign
    {
        public double InletDiameter { get; set; }
        public double OutletDiameter { get; set; }
        public int BladeCount { get; set; }
        public double HelixAngleDeg { get; set; }
        public double BladeThickness { get; set; }
        public double HubRatio { get; set; }
        public double WallThickness { get; set; }
        public double SpeedRpm { get; set; }

        public TurbopumpDesign()
        {
            InletDiameter = 0.045;
            OutletDiameter = 0.134;
            BladeCount = 12;
            HelixAngleDeg = 83.0;
            BladeThickness = 0.00152;
            HubRatio = 0.4;
            WallThickness = 0.002;
            SpeedRpm = 35000.0;
        }

        public double HubDiameter => HubRatio * OutletDiameter;

        public double ShroudDiameter => OutletDiameter;

        public List<(double r, double z, double angle)> BladeSections(int axialSteps = 8)
        {
            var sections = new List<(double, double, double)>();
            double angleRad = HelixAngleDeg * Math.PI / 180.0;

            for (int i = 0; i <= axialSteps; i++)
            {
                double t = (double)i / axialSteps;
                double r = Lerp(HubDiameter / 2.0, ShroudDiameter / 2.0, t);
                double z = t * OutletDiameter * 0.5;
                double angle = angleRad + t * 0.1;
                sections.Add((r, z, angle));
            }

            return sections;
        }

        private double Lerp(double a, double b, double t)
        {
            return a + (b - a) * t;
        }
    }
}
