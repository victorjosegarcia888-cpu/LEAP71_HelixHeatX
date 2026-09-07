using System;
using Leap71.LatticeLibrary;
using PicoGK;

namespace Leap71.RocketCooling
{
    /// <summary>
    /// Generates a structural lattice for the material outside the coolant channels.
    /// It must be clipped to a solid shell and kept out of the fluid domain.
    /// </summary>
    public sealed class RocketCoolingLattice
    {
        /// <summary>
        /// Builds a body-centred lattice inside the supplied boundary volume.
        /// </summary>
        public Voxels VoxGetStructuralLattice(
            Voxels voxBoundary,
            float cellSizeX,
            float cellSizeY,
            float cellSizeZ,
            float beamThickness)
        {
            if (voxBoundary is null)
                throw new ArgumentNullException(nameof(voxBoundary));
            if (cellSizeX <= 0f || cellSizeY <= 0f || cellSizeZ <= 0f)
                throw new ArgumentOutOfRangeException(nameof(cellSizeX));
            if (beamThickness <= 0f)
                throw new ArgumentOutOfRangeException(nameof(beamThickness));

            RegularCellArray cellArray = new RegularCellArray(
                voxBoundary,
                cellSizeX,
                cellSizeY,
                cellSizeZ);
            BodyCentreLattice latticeType = new BodyCentreLattice();
            ConstantBeamThickness thickness = new ConstantBeamThickness(beamThickness);
            Lattice lattice = new Lattice();

            foreach (IUnitCell cell in cellArray.aGetUnitCells())
                latticeType.AddCell(ref lattice, cell, thickness);

            Voxels voxLattice = new Voxels(lattice);
            voxLattice &= voxBoundary;
            return voxLattice;
        }
    }
}
