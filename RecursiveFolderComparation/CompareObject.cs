namespace FolderComparer2
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
        public int Number { get; }
    }
}