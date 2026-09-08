using System;
using System.Collections.Generic;

namespace RocketMotorDesign
{
    public class ThermalEstimate
    {
        public double CoolantMassFlow { get; set; }
        public double CoolantInletTemp { get; set; }
        public double CoolantPressure { get; set; }
        public double HotGasTemp { get; set; }
        public double WallThickness { get; set; }
        public double WallConductivity { get; set; }

        public ThermalEstimate()
        {
            CoolantMassFlow = 0.1;
            CoolantInletTemp = 20.0;
            CoolantPressure = 40e5;
            HotGasTemp = 3500.0;
            WallThickness = 0.002;
            WallConductivity = 25.0;
        }

        public double CoolantOutletTemp(double heatFlux, double length, double channelCount, double channelWidth, double channelHeight)
        {
            double area = channelWidth * channelHeight * channelCount;
            double cp = 4180.0;
            double qTotal = heatFlux * length * area;
            double deltaT = qTotal / (CoolantMassFlow * cp);
            return CoolantInletTemp + deltaT;
        }

        public double HydraulicDiameter(double channelWidth, double channelHeight)
        {
            double perimeter = 2.0 * (channelWidth + channelHeight);
            double area = channelWidth * channelHeight;
            return 4.0 * area / perimeter;
        }

        public double ReynoldsNumber(double density, double velocity, double hydraulicDiameter, double viscosity)
        {
            return density * velocity * hydraulicDiameter / viscosity;
        }

        public double PressureDrop(double frictionFactor, double length, double hydraulicDiameter, double density, double velocity, double minorLossCoeff = 2.0)
        {
            double major = frictionFactor * (length / hydraulicDiameter) * (density * velocity * velocity / 2.0);
            double minor = minorLossCoeff * (density * velocity * velocity / 2.0);
            return major + minor;
        }

        public double WallTemperature(double heatFlux, double hGas, double hCoolant)
        {
            double rWall = WallThickness / WallConductivity;
            return HotGasTemp - heatFlux * (1.0 / hGas + rWall + 1.0 / hCoolant);
        }

        public Dictionary<string, double> EvaluateSegment(ChamberDesign chamber, int segmentIndex, int totalSegments)
        {
            double zStart = chamber.ChamberLength * segmentIndex / totalSegments;
            double zEnd = chamber.ChamberLength * (segmentIndex + 1) / totalSegments;
            double zMid = (zStart + zEnd) / 2.0;

            double rInner = chamber.InnerRadiusAt(zMid);
            double area = 2.0 * Math.PI * rInner * (zEnd - zStart);
            double heatFlux = 1.0e6;
            double channelArea = chamber.ChannelWidth * chamber.ChannelHeight * chamber.ChannelCount;
            double velocity = CoolantMassFlow / (1000.0 * channelArea);
            double hydraulicDiameter = HydraulicDiameter(chamber.ChannelWidth, chamber.ChannelHeight);
            double reynolds = ReynoldsNumber(1000.0, velocity, hydraulicDiameter, 0.001);
            double frictionFactor = reynolds < 2300.0 ? 64.0 / reynolds : 0.079 * Math.Pow(reynolds, -0.25);
            double deltaP = PressureDrop(frictionFactor, zEnd - zStart, hydraulicDiameter, 1000.0, velocity);
            double tCoolant = CoolantOutletTemp(heatFlux, zEnd - zStart, chamber.ChannelCount, chamber.ChannelWidth, chamber.ChannelHeight);
            double tWall = WallTemperature(heatFlux, 5000.0, 2000.0);

            return new Dictionary<string, double>
            {
                ["z_m"] = zMid,
                ["rInner_m"] = rInner,
                ["area_m2"] = area,
                ["velocity_m_s"] = velocity,
                ["reynolds"] = reynolds,
                ["frictionFactor"] = frictionFactor,
                ["deltaP_Pa"] = deltaP,
                ["tCoolant_K"] = tCoolant,
                ["tWall_K"] = tWall
            };
        }
    }
}
