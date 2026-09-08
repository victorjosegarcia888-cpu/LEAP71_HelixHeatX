using System;
using System.Collections.Generic;
using System.Numerics;

namespace Leap71.RocketMotor
{
    public static class MotorParameters
    {
        public static class ASE
        {
            public static readonly float Thrust_lbf = 55000.0f;
            public static readonly float MixtureRatio = 6.0f;
            public static readonly float Ivac = 4623.8f;
            public static readonly float PreburnerTemp_K = 11400.0f;
            public static readonly float ChamberPressure_psia = 923.0f;
            public static readonly float ThroatDiameter_in = 2.57f;
            public static readonly float NozzleExitDiameter_in = 52.2f;
            public static readonly float ExpansionRatio = 400.0f;
            public static readonly float DumpCoolingFlow_lb_sec = 0.0275f;
            public static readonly float CoolantJacketPressureDrop_psid = 75.0f;
            public static readonly float CoolantJacketTempRise_R = 499.0f;

            public static readonly float TotalDryWeight_lb = 456.3f;
            public static readonly float TargetDryWeight_lb = 393.0f;

            public static readonly Vector3 CG_in = new Vector3(1.2f, 0.3f, 18.6f);

            public static readonly float Ixx_in_lb_sec2 = 1155.0f;
            public static readonly float Iyy_in_lb_sec2 = 1140.0f;
            public static readonly float Izz_in_lb_sec2 = 175.0f;

            public static readonly float OxidizerInletID_in = 3.81f;
            public static readonly float FuelInletID_in = 3.36f;

            public static readonly float GimbalingAngle_rad = 0.122f;
            public static readonly float GimbalingAngle_deg = 7.0f;
            public static readonly float GimbalingRate_rad_s2 = 20.0f;
        }

        public static class FuelBoostPump
        {
            public static readonly float Speed_rpm = 26000.0f;
            public static readonly float GearRatio = 2.26f;
            public static readonly float Power_kW = 26.0f;
            public static readonly float PressureRise_psi = 50.8f;
            public static readonly float SuctionSpecificSpeed = 13000.0f;
        }

        public static class OxidizerBoostPump
        {
            public static readonly float PressureRise_psi = 64.3f;
            public static readonly float SuctionSpecificSpeed = 34000.0f;
        }

        public static class HPFuelPump
        {
            public static readonly float Speed_rpm = 93000.0f;
            public static readonly float Head_m = 75200.0f;
            public static readonly float Flow_gpm = 628.0f;
            public static readonly float Efficiency_pct = 58.1f;
            public static readonly float Power_hp = 1480.0f;
            public static readonly float InletTipDiameter_in = 1.77f;
            public static readonly float DischargeTipDiameter_in = 5.276f;
            public static readonly float DischargeBladeHeight_in = 0.1f;
            public static readonly int BladeCount = 12;
        }

        public static class HPOxidizerPump
        {
            public static readonly float InletPressure_psia = 65.0f;
            public static readonly float DischargePressure_psia = 2230.0f;
        }

        public static class GroundRules
        {
            public static readonly float FOS_Yield = 1.1f;
            public static readonly float FOS_Ultimate = 1.4f;
            public static readonly float ProofPressure = 1.2f;
            public static readonly float BurstPressure = 1.5f;
            public static readonly float BurstSpeedMargin = 1.2f;
            public static readonly float CriticalSpeedMargin = 1.25f;
            public static readonly float RigidBodyMargin = 1.2f;
        }

        public static class BearingLimits
        {
            public static readonly float LO2_Roller_DN = 1.5e6f;
            public static readonly float LH2_Roller_DN = 2.0e6f;
            public static readonly float GH2_Roller_DN = 0.5e6f;
            public static readonly float LO2_Ball_DN = 1.5e6f;
            public static readonly float LH2_Ball_DN = 2.0e6f;
            public static readonly float GH2_Ball_DN = 1.2e6f;
            public static readonly float Life_hr = 2100.0f;
        }

        public static class SealLimits
        {
            public static readonly float LO2_PV = 25000.0f;
            public static readonly float LH2_PV = 50000.0f;
            public static readonly float GH2_PV = 20000.0f;
            public static readonly float H2O_PV = 10000.0f;
            public static readonly float LO2_FV = 2000.0f;
            public static readonly float LH2_FV = 4000.0f;
            public static readonly float GH2_FV = 1500.0f;
            public static readonly float H2O_FV = 800.0f;
            public static readonly float LO2_PfV = 60000.0f;
            public static readonly float LH2_PfV = 200000.0f;
            public static readonly float GH2_PfV = 50000.0f;
            public static readonly float H2O_PfV = 20000.0f;
        }

        public static class GearLimits
        {
            public static readonly float PitchLineVelocity_fpm = 20000.0f;
            public static readonly float HertzStress_psi = 60000.0f;
            public static readonly float PressureAngle_rad = 0.436f;
            public static readonly float MaxReduction = 5.0f;
            public static readonly float MinPinionTeeth = 20.0f;
            public static readonly float MaxGearTeeth = 100.0f;
            public static readonly float FaceWidthRatio = 1.0f;
            public static readonly float Overload_pct = 20.0f;
            public static readonly float HeatGen_pct = 1.5f;
        }

        public static class ThrustChamberLimits
        {
            public static readonly float MinSlotWidth_in = 0.030f;
            public static readonly float MinSlotDepthWidthRatio = 4.0f;
            public static readonly float MinWebThickness_in = 0.030f;
            public static readonly float MinWallThickness_in = 0.025f;
        }

        public static class IdleCooldown
        {
            public static readonly float Time_sec = 175.0f;
            public static readonly float FuelBoost_K = 61.0f;
            public static readonly float FuelPump1_K = 21.0f;
            public static readonly float FuelPump2_K = 21.0f;
            public static readonly float OxidizerBoost_K = 106.0f;
            public static readonly float OxidizerPump_K = 94.0f;
            public static readonly float FuelBoostTarget_K = 44.4f;
            public static readonly float OxidizerBoostTarget_K = 100.0f;
            public static readonly float FuelBoostTime_sec = 234.0f;
            public static readonly float OxidizerBoostTime_sec = 198.0f;
        }
    }
}
