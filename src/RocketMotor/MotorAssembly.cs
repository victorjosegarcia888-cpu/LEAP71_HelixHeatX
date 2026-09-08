using System;
using System.Numerics;
using PicoGK;

namespace Leap71.RocketMotor
{
    public class MotorAssembly
    {
        public ChamberGeometry oChamber = new ChamberGeometry();
        public NozzleGeometry oNozzle = new NozzleGeometry();
        public InjectorGeometry oInjector = new InjectorGeometry();
        public TurbopumpGeometry oTurbopump = new TurbopumpGeometry();
        public PreburnerGeometry oPreburner = new PreburnerGeometry();
        public CoolingChannels oCooling = new CoolingChannels();
        public LatticeReinforcement oLattice = new LatticeReinforcement();

        public float fVoxelSize = 0.25f;

        public Voxels voxAssembledMotor()
        {
            Voxels voxMotor = new Voxels(fVoxelSize);

            Voxels voxChamber = oChamber.voxCombinedChamberNozzle(fVoxelSize);
            Sh.PreviewVoxels(voxChamber, Cp.clrBlue);
            voxMotor = Sh.voxUnion(voxMotor, voxChamber);

            Voxels voxInjector = oInjector.voxInjectorAssembly(fVoxelSize);
            Sh.PreviewVoxels(voxInjector, Cp.clrGreen);
            voxMotor = Sh.voxUnion(voxMotor, voxInjector);

            Voxels voxTurbopump = oTurbopump.voxImpeller(fVoxelSize);
            Sh.PreviewVoxels(voxTurbopump, Cp.clrRed);
            voxMotor = Sh.voxUnion(voxMotor, voxTurbopump);

            Voxels voxPreburner = oPreburner.voxPreburnerBody(fVoxelSize);
            Sh.PreviewVoxels(voxPreburner, Cp.clrYellow);
            voxMotor = Sh.voxUnion(voxMotor, voxPreburner);

            Voxels voxCooling = oCooling.voxAllCoolingChannels(fVoxelSize);
            Sh.PreviewVoxels(voxCooling, Cp.clrFrozen);
            voxMotor = Sh.voxSubtract(voxMotor, voxCooling);

            Voxels voxLattice = oLattice.voxAperiodicLattice(fVoxelSize, voxChamber);
            Sh.PreviewVoxels(voxLattice, Cp.clrPitaya);
            voxMotor = Sh.voxUnion(voxMotor, voxLattice);

            return voxMotor;
        }

        public void PrintSummary()
        {
            Library.Log("=== Rocket Motor Assembly Summary ===");
            Library.Log($"Chamber radius: {oChamber.fChamberRadius * 1000.0f:F1} mm");
            Library.Log($"Throat radius: {oChamber.fThroatRadius * 1000.0f:F1} mm");
            Library.Log($"Nozzle exit radius: {oChamber.fNozzleExitRadius * 1000.0f:F1} mm");
            Library.Log($"Channel count: {oChamber.nChannelCount}");
            Library.Log($"Helix angle: {oChamber.fHelixAngleDeg:F1} deg");
            Library.Log($"Injector count: {oInjector.nCount}");
            Library.Log($"Turbopump speed: {oTurbopump.fSpeedRpm:F0} rpm");
            Library.Log($"Preburner major radius: {oPreburner.fMajorRadius * 1000.0f:F1} mm");
        }
    }
}
