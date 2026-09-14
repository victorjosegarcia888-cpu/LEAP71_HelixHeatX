// ============================================================
// TasksRaptor.cs — Cálculos termoquímicos y estructurales
// ============================================================

using System;
using RocketPropulsion.Data;

namespace RocketPropulsion.Tasks
{
    public static class TasksRaptor
    {
        // --- CÁMARA DE COMBUSTIÓN ---
        public static double CalcularVolumenCamara()
        {
            double Vc = Math.PI * Math.Pow(DatosMotor.Dt_Raptor / 2.0, 2.0) * DatosMotor.L_Raptor;
            return Vc;
        }

        // --- TOBERA ---
        public static double CalcularRelacionExpansion()
        {
            double Ae_At = Math.Pow(DatosMotor.De_Raptor / DatosMotor.Dt_Raptor, 2.0);
            return Ae_At;
        }

        // --- INYECTOR PINTLE ---
        public static double CalcularAnguloSpray(double TMR)
        {
            double cosTheta = 1.0 / (1.0 + TMR);
            return Math.Acos(cosTheta) * (180.0 / Math.PI);
        }

        // --- CICLO FFSCC ---
        public static double CalcularPotenciaTurbina(double mdot, double Cp, double Tpb, double PR, double eta)
        {
            double gamma = 1.3;
            double tempDropFactor = 1.0 - Math.Pow(1.0 / PR, (gamma - 1.0) / gamma);
            double W = (mdot * Cp * Tpb * tempDropFactor * eta) / 1e6; // MW
            return W;
        }

        // --- DISOCIACIÓN ---
        public static double CalcularDisociacion(double T, double P)
        {
            // Basado en UC3M Fig. 2.5
            double factorTemp = (T - 1500.0) / 2000.0;
            double factorPresion = Math.Log10(P / 1e5) * 0.05;
            return Math.Clamp(0.15 + factorTemp + factorPresion, 0.0, 1.0);
        }

        // --- PERFIL TÉRMICO OTDF ---
        public static double CalcularOTDF(double Tmax, double Tavg, double Tinlet)
        {
            return (Tmax - Tavg) / (Tavg - Tinlet);
        }

        // --- RIESGO DE COKING ---
        public static double CalcularRiesgoCoking(double T)
        {
            if (T < 850) return 0.1;
            if (T <= 1200) return 0.85;
            return 0.3;
        }

        // --- EFICIENCIA GLOBAL ---
        public static double CalcularEficienciaTermica(double Isp, double LHV)
        {
            double ve = Isp * 9.80665;
            return (0.5 * Math.Pow(ve, 2.0)) / LHV;
        }

        // --- ÍNDICE DE OXIDACIÓN ---
        public static double CalcularIndiceOxidacion(double OF, double Tad)
        {
            // Relación empírica NASA: índice = (O/F)/((Tad/1000)*4)
            return Math.Clamp((OF / ((Tad / 1000.0) * 4.0)), 0.5, 1.0);
        }
    }
}
