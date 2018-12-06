namespace CSV_file_reader
{
    public interface IRowData
    {
        int DevelopmentYear { get; }
        double IncrementalValue { get; }
        int OriginYear { get; }
        string Product { get; }
    }
}