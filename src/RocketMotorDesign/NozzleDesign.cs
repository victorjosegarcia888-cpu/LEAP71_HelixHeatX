using System;
using System.Collections.Generic;

namespace RocketMotorDesign
{
    public class NozzleDesign
    {
        public double ThroatRadius { get; set; }
        public double ExitRadius { get; set; }
        public double ExpansionRatio { get; set; }
        public double ThetaInDeg { get; set; }
        public double ThetaExpDeg { get; set; }
        public double ThetaExitDeg { get; set; }
        public double WallThickness { get; set; }
        public double Length { get; set; }

        public NozzleDesign()
        {
            ThroatRadius = 0.099;
            ExitRadius = 0.530;
            ExpansionRatio = 400.0;
            ThetaInDeg = 25.0;
            ThetaExpDeg = 34.0;
            ThetaExitDeg = 10.5;
            WallThickness = 0.003;
            Length = 0.600;
        }

        public double RadiusAt(double z)
        {
            if (z < 0.0) return ThroatRadius;
            if (z > Length) return ExitRadius;

            double t = z / Length;
            if (t < 0.2)
            {
                return ThroatRadius + (ExitRadius - ThroatRadius) * EaseInQuadratic(t / 0.2);
            }
            else if (t < 0.8)
            {
                return ThroatRadius + (ExitRadius - ThroatRadius) * (0.3 + 0.7 * EaseInOutCubic((t - 0.2) / 0.6));
            }
            else
            {
                return ThroatRadius + (ExitRadius - ThroatRadius) * EaseOutQuadratic((t - 0.8) / 0.2);
            }
        }

        public double WallOuterRadiusAt(double z)
        {
            return RadiusAt(z) + WallThickness;
        }

        public List<(double r, double z)> GenerateProfile(int samples = 64)
        {
            var profile = new List<(double, double)>();
            for (int i = 0; i <= samples; i++)
            {
                double z = Length * i / samples;
                profile.Add((RadiusAt(z), z));
            }
            return profile;
        }

        public double EaseInQuadratic(double t)
        {
            return t * t;
        }

        public double EaseOutQuadratic(double t)
        {
            return t * (2.0 - t);
        }

        public double EaseInOutCubic(double t)
        {
            return t < 0.5 ? 4.0 * t * t * t : 1.0 - Math.Pow(-2.0 * t + 2.0, 3.0) / 2.0;
        }
    }
}
