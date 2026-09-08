using System.Collections.Generic;

namespace RocketMotorDesign
{
    public static class EngineParameters
    {
        public static readonly Dictionary<string, double> ASE_RatedThrust = new()
        {
            ["MixtureRatio"] = 6.0,
            ["Ivac"] = 4623.8,
            ["Thrust_lbf"] = 55000.0,
            ["PreburnerTemp_K"] = 11400.0,
            ["ChamberPressure_psia"] = 923.0,
            ["ThroatDiameter_in"] = 2.57,
            ["NozzleExitDiameter_in"] = 52.2,
            ["ExpansionRatio"] = 400.0,
            ["DumpCoolingFlow_lb_sec"] = 0.0275,
            ["CoolantJacketPressureDrop_psid"] = 75.0,
            ["CoolantJacketTempRise_R"] = 499.0
        };

        public static readonly Dictionary<string, double> ASE_PumpedIdle = new()
        {
            ["MixtureRatio"] = 3.5,
            ["Ivac"] = 4415.0,
            ["Thrust_pct"] = 0.04,
            ["ChamberPressure_psia"] = 79.1,
            ["ThroatDiameter_in"] = 2.57,
            ["NozzleDumpCooling_lb_sec"] = 0.0275
        };

        public static readonly Dictionary<string, double> ASE_TankHeadIdle = new()
        {
            ["MixtureRatio"] = 1.5,
            ["Ivac"] = 3939.3,
            ["Thrust_pct"] = 0.003
        };

        public static readonly Dictionary<string, double> ASE_Weight = new()
        {
            ["TotalDry_lb"] = 456.3,
            ["TargetDry_lb"] = 393.0,
            ["HPFuelTurbopump_lb"] = 42.1,
            ["HPOxidizerTurbopump_lb"] = 49.9,
            ["LPFuelPump_lb"] = 5.8,
            ["LPOxidizerPump_lb"] = 8.3,
            ["Preburner_lb"] = 8.5,
            ["MainCase_lb"] = 23.5,
            ["ThrustChamber_lb"] = 49.6,
            ["DumpCooledNozzle_lb"] = 71.6,
            ["Controls_lb"] = 164.9,
            ["Plumbing_lb"] = 32.1
        };

        public static readonly Dictionary<string, double> ASE_CG_in = new()
        {
            ["X"] = 1.2,
            ["Y"] = 0.3,
            ["Z"] = 18.6
        };

        public static readonly Dictionary<string, double> ASE_GimbalInertia = new()
        {
            ["Ixx_in_lb_sec2"] = 1155.0,
            ["Iyy_in_lb_sec2"] = 1140.0,
            ["Izz_in_lb_sec2"] = 175.0
        };

        public static readonly Dictionary<string, double> ASE_InletIDs = new()
        {
            ["Oxidizer_in"] = 3.81,
            ["Fuel_in"] = 3.36
        };

        public static readonly Dictionary<string, double> FuelBoostPump = new()
        {
            ["Speed_rpm"] = 26000.0,
            ["GearRatio"] = 2.26,
            ["Power_kW"] = 26.0,
            ["PressureRise_psi"] = 50.8,
            ["SuctionSpecificSpeed"] = 13000.0
        };

        public static readonly Dictionary<string, double> OxidizerBoostPump = new()
        {
            ["PressureRise_psi"] = 64.3,
            ["SuctionSpecificSpeed"] = 34000.0
        };

        public static readonly Dictionary<string, double> HPFuelPump_Stage1 = new()
        {
            ["Speed_rpm"] = 93000.0,
            ["Head_m"] = 75200.0,
            ["Flow_gpm"] = 628.0,
            ["Efficiency_pct"] = 58.1,
            ["Power_hp"] = 1480.0,
            ["InletTipDiameter_in"] = 1.77,
            ["DischargeTipDiameter_in"] = 5.276,
            ["DischargeBladeHeight_in"] = 0.1,
            ["BladeCount"] = 12
        };

        public static readonly Dictionary<string, double> HPFuelPump_Stage2 = new()
        {
            ["Speed_rpm"] = 93000.0,
            ["Head_m"] = 78215.0,
            ["Flow_gpm"] = 669.0,
            ["Efficiency_pct"] = 58.4,
            ["Power_hp"] = 1530.0,
            ["InletTipDiameter_in"] = 2.35,
            ["DischargeTipDiameter_in"] = 5.37,
            ["DischargeBladeHeight_in"] = 0.1
        };

        public static readonly Dictionary<string, double> HPOxidizerPump = new()
        {
            ["InletPressure_psia"] = 65.0,
            ["DischargePressure_psia"] = 2230.0
        };

        public static readonly Dictionary<string, double> ASE_GroundRules = new()
        {
            ["FOS_Yield"] = 1.1,
            ["FOS_Ultimate"] = 1.4,
            ["ProofPressure"] = 1.2,
            ["BurstPressure"] = 1.5,
            ["BurstSpeedMargin"] = 1.2,
            ["CriticalSpeedMargin"] = 1.25,
            ["RigidBodyMargin"] = 1.2
        };

        public static readonly Dictionary<string, double> BearingLimits = new()
        {
            ["LO2_Roller_DN"] = 1.5e6,
            ["LH2_Roller_DN"] = 2.0e6,
            ["GH2_Roller_DN"] = 0.5e6,
            ["LO2_Ball_DN"] = 1.5e6,
            ["LH2_Ball_DN"] = 2.0e6,
            ["GH2_Ball_DN"] = 1.2e6,
            ["Life_hr"] = 2100.0
        };

        public static readonly Dictionary<string, double> SealLimits = new()
        {
            ["LO2_PV"] = 25000.0,
            ["LH2_PV"] = 50000.0,
            ["GH2_PV"] = 20000.0,
            ["H2O_PV"] = 10000.0,
            ["LO2_FV"] = 2000.0,
            ["LH2_FV"] = 4000.0,
            ["GH2_FV"] = 1500.0,
            ["H2O_FV"] = 800.0,
            ["LO2_PfV"] = 60000.0,
            ["LH2_PfV"] = 200000.0,
            ["GH2_PfV"] = 50000.0,
            ["H2O_PfV"] = 20000.0
        };

        public static readonly Dictionary<string, double> GearLimits = new()
        {
            ["PitchLineVelocity_fpm"] = 20000.0,
            ["HertzStress_psi"] = 60000.0,
            ["PressureAngle_rad"] = 0.436,
            ["MaxReduction"] = 5.0,
            ["MinPinionTeeth"] = 20.0,
            ["MaxGearTeeth"] = 100.0,
            ["FaceWidthRatio"] = 1.0,
            ["Overload_pct"] = 20.0,
            ["HeatGen_pct"] = 1.5
        };

        public static readonly Dictionary<string, double> ThrustChamberLimits = new()
        {
            ["MinSlotWidth_in"] = 0.030,
            ["MinSlotDepthWidthRatio"] = 4.0,
            ["MinWebThickness_in"] = 0.030,
            ["MinWallThickness_in"] = 0.025
        };

        public static readonly Dictionary<string, double> IdleCooldown = new()
        {
            ["Time_sec"] = 175.0,
            ["FuelBoost_K"] = 61.0,
            ["FuelPump1_K"] = 21.0,
            ["FuelPump2_K"] = 21.0,
            ["OxidizerBoost_K"] = 106.0,
            ["OxidizerPump_K"] = 94.0,
            ["FuelBoostTarget_K"] = 44.4,
            ["OxidizerBoostTarget_K"] = 100.0,
            ["FuelBoostTime_sec"] = 234.0,
            ["OxidizerBoostTime_sec"] = 198.0
        };

        public static readonly Dictionary<string, double> Gimbaling = new()
        {
            ["Angle_rad"] = 0.122,
            ["Angle_deg"] = 7.0,
            ["Rate_rad_s2"] = 20.0
        };
    }
}
