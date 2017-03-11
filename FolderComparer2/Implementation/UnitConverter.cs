using FolderComparer2.Enumerations;
using FolderComparer2.Interfaces;
using FolderComparer2.Models;

namespace FolderComparer2.Implementation
{
    public class UnitConverter : IUnitConverter
    {
        public void EvaluateByteSize(CompareObject compare)
        {
            if (!EvaluatekB(compare))
                return;
            if (!EvaluateMb(compare))
                return;
            if (!EvaluateGb(compare))
                return;
            if (!EvaluateTb(compare))
                return;
            if (!EvaluatePb(compare))
                return;
            EvaluateEb(compare);
        }

        private static bool EvaluatekB(CompareObject compare)
        {
            if (compare.Size < 1024) return false;
            compare.Size /= 1024;
            compare.Unit = Unit.KB;
            return true;
        }

        private static bool EvaluateMb(CompareObject compare)
        {
            if (compare.Size < 1024) return false;
            compare.Size /= 1024;
            compare.Unit = Unit.Mb;
            return true;
        }

        private static bool EvaluateGb(CompareObject compare)
        {
            if (compare.Size < 1024) return false;
            compare.Size /= 1024;
            compare.Unit = Unit.Gb;
            return true;
        }

        private static bool EvaluateTb(CompareObject compare)
        {
            if (compare.Size < 1024) return false;
            compare.Size /= 1024;
            compare.Unit = Unit.Tb;
            return true;
        }

        private static bool EvaluatePb(CompareObject compare)
        {
            if (compare.Size < 1024) return false;
            compare.Size /= 1024;
            compare.Unit = Unit.Pb;
            return true;
        }

        private static bool EvaluateEb(CompareObject compare)
        {
            if (compare.Size < 1024) return false;
            compare.Size /= 1024;
            compare.Unit = Unit.Eb;
            return true;
        }
    }
}