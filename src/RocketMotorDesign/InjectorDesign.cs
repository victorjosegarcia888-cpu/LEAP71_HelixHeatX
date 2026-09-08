using System;
using System.Collections.Generic;

namespace RocketMotorDesign
{
    public class InjectorDesign
    {
        public int Count { get; set; }
        public double SwirlAngleDeg { get; set; }
        public double InnerRadius { get; set; }
        public double OuterRadius { get; set; }
        public double ChannelWidth { get; set; }
        public double ChannelDepth { get; set; }

        public InjectorDesign()
        {
            Count = 24;
            SwirlAngleDeg = 45.0;
            InnerRadius = 0.045;
            OuterRadius = 0.190;
            ChannelWidth = 0.0015;
            ChannelDepth = 0.0025;
        }

        public List<(double x, double y, double angle)> Positions()
        {
            var positions = new List<(double, double, double)>();
            double angleStep = 2.0 * Math.PI / Count;
            double radius = (InnerRadius + OuterRadius) / 2.0;

            for (int i = 0; i < Count; i++)
            {
                double angle = i * angleStep;
                double x = radius * Math.Cos(angle);
                double y = radius * Math.Sin(angle);
                positions.Add((x, y, SwirlAngleDeg));
            }

            return positions;
        }

        public double SwirlAngleRad => SwirlAngleDeg * Math.PI / 180.0;
    }
}
