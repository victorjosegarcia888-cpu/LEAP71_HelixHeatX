using System;
using System.Numerics;
using PicoGK;
using Leap71.ShapeKernel;
using Leap71.LatticeLibrary;

namespace Leap71.RocketMotor
{
    public static class MotorTasks
    {
        // Task 1: Chamber and Nozzle
        public static void Task_ChamberAndNozzle()
        {
            try
            {
                // Chamber
                LocalFrame oChamberFrame = new LocalFrame(new Vector3(-60, 0, 0));
                BaseCylinder oChamber = new BaseCylinder(oChamberFrame, 200f, 190f);
                oChamber.SetLengthSteps(500);
                oChamber.SetRadius(new SurfaceModulation(fGetChamberRadius));
                Voxels voxChamber = oChamber.voxConstruct();
                Sh.PreviewVoxels(voxChamber, Cp.clrBlue);

                // Nozzle
                LocalFrame oNozzleFrame = new LocalFrame(new Vector3(60, 0, 0));
                BaseCylinder oNozzle = new BaseCylinder(oNozzleFrame, 600f, 99f);
                oNozzle.SetLengthSteps(500);
                oNozzle.SetRadius(new SurfaceModulation(fGetNozzleRadius));
                Voxels voxNozzle = oNozzle.voxConstruct();
                Sh.PreviewVoxels(voxNozzle, Cp.clrGreen);
            }
            catch (Exception e)
            {
                Library.Log($"Failed run Task_ChamberAndNozzle: {e.Message}");
            }
        }

        // Task 2: Injector Plate with Lattice Elements
        public static void Task_InjectorPlate()
        {
            try
            {
                LocalFrame oFrame = new LocalFrame(new Vector3(0, 0, 0));
                BaseCylinder oPlate = new BaseCylinder(oFrame, 5f, 190f);
                Voxels voxPlate = oPlate.voxConstruct();
                Sh.PreviewVoxels(voxPlate, Cp.clrYellow);

                Lattice oLattice = new Lattice();
                int nCount = 24;
                float fRadius = 117.5f;
                float fAngleStep = 2.0f * MathF.PI / nCount;

                for (int i = 0; i < nCount; i++)
                {
                    float fAngle = i * fAngleStep;
                    Vector3 vecPos = new Vector3(fRadius * MathF.Cos(fAngle), fRadius * MathF.Sin(fAngle), 0);
                    oLattice.AddSphere(vecPos, 3f);
                }

                Voxels voxInjectors = new Voxels(oLattice);
                Sh.PreviewVoxels(voxInjectors, Cp.clrRed);
            }
            catch (Exception e)
            {
                Library.Log($"Failed run Task_InjectorPlate: {e.Message}");
            }
        }

        // Task 3: Turbopump Impeller
        public static void Task_TurbopumpImpeller()
        {
            try
            {
                LocalFrame oFrame = new LocalFrame(new Vector3(0, 0, 0));
                BaseCylinder oHub = new BaseCylinder(oFrame, 50f, 26.8f);
                Voxels voxHub = oHub.voxConstruct();
                Sh.PreviewVoxels(voxHub, Cp.clrBlueberry);

                Lattice oLattice = new Lattice();
                int nSteps = 8;
                float fAngleRad = 83.0f * MathF.PI / 180.0f;

                for (int i = 0; i <= nSteps; i++)
                {
                    float fT = (float)i / nSteps;
                    float fR = 26.8f + fT * (67.0f - 26.8f);
                    float fZ = fT * 67.0f;
                    float fAngle = fAngleRad + fT * 0.1f;

                    Vector3 vecStart = new Vector3(0, 0, fZ);
                    Vector3 vecEnd = new Vector3(fR * MathF.Cos(fAngle), fR * MathF.Sin(fAngle), fZ);
                    oLattice.AddBeam(vecStart, 1.52f, vecEnd, 1.52f, false);
                }

                Voxels voxBlades = new Voxels(oLattice);
                Sh.PreviewVoxels(voxBlades, Cp.clrPitaya);
            }
            catch (Exception e)
            {
                Library.Log($"Failed run Task_TurbopumpImpeller: {e.Message}");
            }
        }

        // Task 4: Preburner Toroid
        public static void Task_PreburnerToroid()
        {
            try
            {
                LocalFrame oFrame = new LocalFrame(new Vector3(0, 0, 0));
                BaseRing oTorus = new BaseRing(oFrame, 60f, 10f);
                Voxels voxTorus = oTorus.voxConstruct();
                Sh.PreviewVoxels(voxTorus, Cp.clrFrozen);
            }
            catch (Exception e)
            {
                Library.Log($"Failed run Task_PreburnerToroid: {e.Message}");
            }
        }

        // Task 5: Cooling Channels
        public static void Task_CoolingChannels()
        {
            try
            {
                Lattice oChannels = new Lattice();
                int nChannelCount = 120;
                int nSteps = 100;
                float fHelixAngleDeg = 35.0f;
                float fHelixAngleRad = fHelixAngleDeg * MathF.PI / 180.0f;
                float fChannelRadius = 200.0f;
                float fChannelLength = 880.0f;
                float fChannelWidth = 2.0f;

                for (int i = 0; i < nChannelCount; i++)
                {
                    Vector3 previousPoint = new Vector3();
                    bool hasPreviousPoint = false;
                    float fPhase = 2.0f * MathF.PI * i / nChannelCount;

                    for (int j = 0; j <= nSteps; j++)
                    {
                        float fZ = (float)j / nSteps * fChannelLength;
                        float fPhi = fPhase + fZ * MathF.Tan(fHelixAngleRad) / fChannelRadius;
                        Vector3 point = new Vector3(
                            fChannelRadius * MathF.Cos(fPhi),
                            fChannelRadius * MathF.Sin(fPhi),
                            fZ);

                        if (hasPreviousPoint)
                            oChannels.AddBeam(previousPoint, fChannelWidth, point, fChannelWidth, true);

                        previousPoint = point;
                        hasPreviousPoint = true;
                    }
                }

                Voxels voxChannels = new Voxels(oChannels);
                Sh.PreviewVoxels(voxChannels, Cp.clrToothpaste);
            }
            catch (Exception e)
            {
                Library.Log($"Failed run Task_CoolingChannels: {e.Message}");
            }
        }

        // Task 6: Aperiodic Lattice
        public static void Task_AperiodicLattice()
        {
            try
            {
                LocalFrame oFrame = new LocalFrame(new Vector3(0, 0, 0));
                BaseBox oBounding = new BaseBox(oFrame, 100f, 100f, 100f);
                Voxels voxBounding = oBounding.voxConstruct();
                Sh.PreviewVoxels(voxBounding, Cp.clrWarning);

                ICellArray xCellArray = new RegularCellArray(voxBounding, 20, 20, 20);
                ILatticeType xLatticeType = new BodyCentreLattice();
                IBeamThickness xBeamThickness = new ConstantBeamThickness(1f);
                xBeamThickness.SetBoundingVoxels(voxBounding);

                uint nSubSample = 5;
                Lattice oLattice = new Lattice();
                foreach (IUnitCell xCell in xCellArray.aGetUnitCells())
                {
                    xBeamThickness.UpdateCell(xCell);
                    xLatticeType.AddCell(ref oLattice, xCell, xBeamThickness, nSubSample);
                }

                Voxels voxLattice = new Voxels(oLattice);
                voxLattice = voxLattice.voxBoolIntersect(voxBounding);
                Sh.PreviewVoxels(voxLattice, Cp.clrLemongrass);
            }
            catch (Exception e)
            {
                Library.Log($"Failed run Task_AperiodicLattice: {e.Message}");
            }
        }

        // Task 7: Assembled Motor
        public static void Task_AssembledMotor()
        {
            try
            {
                Voxels voxMotor = new Voxels();

                // Chamber
                LocalFrame oChamberFrame = new LocalFrame(new Vector3(-60, 0, 0));
                BaseCylinder oChamber = new BaseCylinder(oChamberFrame, 200f, 190f);
                oChamber.SetLengthSteps(500);
                oChamber.SetRadius(new SurfaceModulation(fGetChamberRadius));
                Voxels voxChamber = oChamber.voxConstruct();
                Sh.PreviewVoxels(voxChamber, Cp.clrBlue);

                // Nozzle
                LocalFrame oNozzleFrame = new LocalFrame(new Vector3(60, 0, 0));
                BaseCylinder oNozzle = new BaseCylinder(oNozzleFrame, 600f, 99f);
                oNozzle.SetLengthSteps(500);
                oNozzle.SetRadius(new SurfaceModulation(fGetNozzleRadius));
                Voxels voxNozzle = oNozzle.voxConstruct();
                Sh.PreviewVoxels(voxNozzle, Cp.clrGreen);

                voxMotor = voxMotor.voxBoolAdd(voxChamber);
                voxMotor = voxMotor.voxBoolAdd(voxNozzle);

                // Injector
                LocalFrame oInjectorFrame = new LocalFrame(new Vector3(0, 0, -110));
                BaseCylinder oInjector = new BaseCylinder(oInjectorFrame, 5f, 190f);
                Voxels voxInjector = oInjector.voxConstruct();
                Sh.PreviewVoxels(voxInjector, Cp.clrYellow);
                voxMotor = voxMotor.voxBoolAdd(voxInjector);

                // Preburner
                LocalFrame oPreburnerFrame = new LocalFrame(new Vector3(0, 120, 0));
                BaseRing oPreburner = new BaseRing(oPreburnerFrame, 60f, 10f);
                Voxels voxPreburner = oPreburner.voxConstruct();
                Sh.PreviewVoxels(voxPreburner, Cp.clrFrozen);
                voxMotor = voxMotor.voxBoolAdd(voxPreburner);

                // Final preview
                Sh.PreviewVoxels(voxMotor, Cp.clrRock);
            }
            catch (Exception e)
            {
                Library.Log($"Failed run Task_AssembledMotor: {e.Message}");
            }
        }

        // Task 8: Nozzle with Wall
        public static void Task_NozzleWithWall()
        {
            try
            {
                LocalFrame oFrame = new LocalFrame(new Vector3(0, 0, 0));
                BaseCylinder oInner = new BaseCylinder(oFrame, 600f, 99f);
                oInner.SetLengthSteps(500);
                oInner.SetRadius(new SurfaceModulation(fGetNozzleRadius));
                Voxels voxInner = oInner.voxConstruct();

                BaseCylinder oOuter = new BaseCylinder(oFrame, 600f, 532f);
                oOuter.SetLengthSteps(500);
                oOuter.SetRadius(new SurfaceModulation(fGetNozzleOuterRadius));
                Voxels voxOuter = oOuter.voxConstruct();

                Voxels voxNozzleWall = voxOuter.voxBoolSubtract(voxInner);
                Sh.PreviewVoxels(voxNozzleWall, Cp.clrLavender);
            }
            catch (Exception e)
            {
                Library.Log($"Failed run Task_NozzleWithWall: {e.Message}");
            }
        }

        // Task 9: Chamber with Lattice Infill
        public static void Task_ChamberWithLattice()
        {
            try
            {
                LocalFrame oFrame = new LocalFrame(new Vector3(0, 0, 0));
                BaseCylinder oChamber = new BaseCylinder(oFrame, 200f, 190f);
                oChamber.SetLengthSteps(500);
                oChamber.SetRadius(new SurfaceModulation(fGetChamberRadius));
                Voxels voxChamber = oChamber.voxConstruct();
                Sh.PreviewVoxels(voxChamber, Cp.clrBlue);

                BaseBox oBounding = new BaseBox(oFrame, 400f, 400f, 220f);
                Voxels voxBounding = oBounding.voxConstruct();

                ICellArray xCellArray = new RegularCellArray(voxBounding, 20, 20, 20);
                ILatticeType xLatticeType = new BodyCentreLattice();
                IBeamThickness xBeamThickness = new ConstantBeamThickness(1f);
                xBeamThickness.SetBoundingVoxels(voxBounding);

                uint nSubSample = 5;
                Lattice oLattice = new Lattice();
                foreach (IUnitCell xCell in xCellArray.aGetUnitCells())
                {
                    xBeamThickness.UpdateCell(xCell);
                    xLatticeType.AddCell(ref oLattice, xCell, xBeamThickness, nSubSample);
                }

                Voxels voxLattice = new Voxels(oLattice);
                voxLattice = voxLattice.voxBoolIntersect(voxBounding);
                Sh.PreviewVoxels(voxLattice, Cp.clrOrchid);
            }
            catch (Exception e)
            {
                Library.Log($"Failed run Task_ChamberWithLattice: {e.Message}");
            }
        }

        // Task 10: HP Ducts with Spline Paths
        public static void Task_HP_Ducts()
        {
            try
            {
                Lattice oDucts = new Lattice();
                float fDuctRadius = 22f;
                int nSteps = 50;

                // Straight duct
                for (int i = 0; i < nSteps; i++)
                {
                    Vector3 vecStart = new Vector3(0, 0, i * 2f);
                    Vector3 vecEnd = new Vector3(0, 0, (i + 1) * 2f);
                    oDucts.AddBeam(vecStart, fDuctRadius, vecEnd, fDuctRadius, true);
                }

                Voxels voxDucts = new Voxels(oDucts);
                Sh.PreviewVoxels(voxDucts, Cp.clrRacingGreen);
            }
            catch (Exception e)
            {
                Library.Log($"Failed run Task_HP_Ducts: {e.Message}");
            }
        }

        private static float fGetChamberRadius(float fPhi, float fLengthRatio)
        {
            float fZ = fLengthRatio * 200f;
            float fR = 190f;
            return fR * 2.0f;
        }

        private static float fGetNozzleRadius(float fPhi, float fLengthRatio)
        {
            float fZ = fLengthRatio * 600f;
            float fR = 99f + (530f - 99f) * fLengthRatio;
            return fR * 2.0f;
        }

        private static float fGetNozzleOuterRadius(float fPhi, float fLengthRatio)
        {
            float fZ = fLengthRatio * 600f;
            float fR = 99f + (530f - 99f) * fLengthRatio + 3f;
            return fR * 2.0f;
        }
    }
}
