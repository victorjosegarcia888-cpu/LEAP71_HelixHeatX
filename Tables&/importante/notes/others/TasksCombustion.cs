// ============================================================
// TasksCombustion.cs — Cámara, Tobera e Inyección
// ============================================================

using System;
using RocketPropulsion.Data;

namespace RocketPropulsion.Tasks
{
    public static class TasksCombustion
    {
        public static double CalcularPresionCamara(double mdot, double cStar, double At)
        {
            return (mdot * cStar) / At;
        }

        public static double CalcularTemperaturaAdiabatica(double OF)
        {
            return 3300.0 + (OF - 3.6) * 100.0;
        }

        public static double CalcularVelocidadSalida(double Isp)
        {
            return Isp * 9.80665;
        }

        public static double CalcularEmpuje(double mdot, double ve, double pe, double pa, double Ae)
        {
            return mdot * ve + (pe - pa) * Ae;
        }
    }
}
