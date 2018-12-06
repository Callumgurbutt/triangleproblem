namespace CSV_file_reader
{
    public interface IMinMax
    {
        int Duration { get; }
        int End { get; }
        int NumberOfProducts { get; }
        int Start { get; }
    }
}