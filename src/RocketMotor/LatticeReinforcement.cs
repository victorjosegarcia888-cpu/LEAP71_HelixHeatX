using System;
using System.Numerics;
using PicoGK;

namespace Leap71.RocketMotor
{
    public class LatticeReinforcement
    {
        public float fCellSize = 0.020f;
        public float fBeamRadius = 0.002f;
        public int nOrder = 10;
        public float fAmplitude = 0.015f;
        public float fFrequency = 8.0f;

        public Voxels voxAperiodicLattice(float fVoxelSize, Voxels voxBounding)
        {
            ICellArray xCellArray = new RegularCellArray(voxBounding, (int)(fCellSize / fVoxelSize), (int)(fCellSize / fVoxelSize), (int)(fCellSize / fVoxelSize));
            ILattice xLatticeType = new BodyCenteredLattice();
            IBeamThickness xBeamThickness = new ConstantBeamThickness(fBeamRadius);
            xBeamThickness.SetBoundingVoxels(voxBounding);

            uint nSubSample = 5;
            Voxels voxLattice = voxGetFinalLatticeGeometry(xCellArray, xLatticeType, xBeamThickness, nSubSample);
            voxLattice = Sh.voxIntersect(voxLattice, voxBounding);
            return voxLattice;
        }

        public Voxels voxQuasicrystalInfill(float fVoxelSize, Voxels voxBounding)
        {
            IImplicit sdfPattern = new ImplicitGyroid(fFrequency, fAmplitude);
            Voxels voxInfill = Sh.voxIntersectImplicit(voxBounding, sdfPattern);
            return voxInfill;
        }

        private Voxels voxGetFinalLatticeGeometry(ICellArray xCellArray, ILattice xLatticeType, IBeamThickness xBeamThickness, uint nSubSample)
        {
            Lattice oLattice = new Lattice();
            foreach (IUnitCell xCell in xCellArray.aGetUnitCells())
            {
                xBeamThickness.UpdateCell(xCell);
                xLatticeType.AddCell(ref oLattice, xCell, xBeamThickness, nSubSample);
            }
            Voxels voxLattice = new Voxels(oLattice, fBeamRadius * 2.0f);
            return voxLattice;
        }
    }
}
