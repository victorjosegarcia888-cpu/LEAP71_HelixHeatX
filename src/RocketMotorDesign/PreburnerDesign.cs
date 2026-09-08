using System;

namespace RocketMotorDesign
{
    public class PreburnerDesign
    {
        public double MajorRadius { get; set; }
        public double PipeRadius { get; set; }
        public double WallThickness { get; set; }
        public double Length { get; set; }
        public int FeedPorts { get; set; }

        public PreburnerDesign()
        {
            MajorRadius = 0.060;
            PipeRadius = 0.010;
            WallThickness = 0.002;
            Length = 0.120;
            FeedPorts = 2;
        }

        public double OuterRadius => MajorRadius + PipeRadius + WallThickness;

        public double TorusCrossSectionRadius => PipeRadius;

        public (double x, double y, double z) CenterLineAt(double angle)
        {
            return (MajorRadius * Math.Cos(angle), MajorRadius * Math.Sin(angle), 0.0);
        }
    }
}
