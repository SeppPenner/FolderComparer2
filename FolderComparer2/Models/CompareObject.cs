using FolderComparer2.Enumerations;

namespace FolderComparer2.Models
{
    public class CompareObject
    {
        public CompareObject(int number)
        {
            Number = number;
        }

        public double SubFolderCount { get; set; }
        public double FileCount { get; set; }
        public double ByteSize { get; set; }
        public double Size { get; set; }
        public Unit Unit { get; set; }
        public int Number { get; }
    }
}