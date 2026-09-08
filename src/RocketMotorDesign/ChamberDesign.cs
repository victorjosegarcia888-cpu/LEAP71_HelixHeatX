using System;

namespace RocketMotorDesign
{
    public class ChamberDesign
    {
        public double ChamberRadius { get; set; }
        public double ChamberLength { get; set; }
        public double ThroatRadius { get; set; }
        public double ConvergentLength { get; set; }
        public double NozzleExitRadius { get; set; }
        public double NozzleLength { get; set; }
        public double LinerThickness { get; set; }
        public double OuterWallThickness { get; set; }
        public int ChannelCount { get; set; }
        public double ChannelWidth { get; set; }
        public double ChannelHeight { get; set; }
        public double HelixAngleDeg { get; set; }

        public ChamberDesign()
        {
            ChamberRadius = 0.190;
            ChamberLength = 0.200;
            ThroatRadius = 0.099;
            ConvergentLength = 0.080;
            NozzleExitRadius = 0.530;
            NozzleLength = 0.600;
            LinerThickness = 0.002;
            OuterWallThickness = 0.003;
            ChannelCount = 120;
            ChannelWidth = 0.002;
            ChannelHeight = 0.003;
            HelixAngleDeg = 35.0;
        }

        public double InnerRadiusAt(double z)
        {
            double lc = ChamberLength;
            double lconv = ConvergentLength;
            double ltotal = ChamberLength + ConvergentLength + NozzleLength;

            if (z < 0.0) return ChamberRadius;
            if (z <= lc) return ChamberRadius;

            double t = (z - lc) / lconv;
            if (t > 1.0) t = 1.0;
            double rConv = Lerp(ChamberRadius, ThroatRadius, t);

            if (z <= lc + lconv) return rConv;

            double tNoz = (z - lc - lconv) / NozzleLength;
            if (tNoz > 1.0) tNoz = 1.0;
            return Lerp(ThroatRadius, NozzleExitRadius, tNoz);
        }

        public double OuterRadiusAt(double z)
        {
            return InnerRadiusAt(z) + LinerThickness + ChannelHeight + OuterWallThickness;
        }

        public double ChannelRadiusAt(double z)
        {
            return InnerRadiusAt(z) + LinerThickness + 0.5 * ChannelHeight;
        }

        public double HelixAngleRad => HelixAngleDeg * Math.PI / 180.0;

        public (double r, double theta, double z) ChannelPosition(int channelIndex, double z)
        {
            double phi = 2.0 * Math.PI * channelIndex / ChannelCount + z * Math.Tan(HelixAngleRad) / ChannelRadiusAt(z);
            return (ChannelRadiusAt(z), phi, z);
        }

        private double Lerp(double a, double b, double t)
        {
            return a + (b - a) * t;
        }
    }
}
